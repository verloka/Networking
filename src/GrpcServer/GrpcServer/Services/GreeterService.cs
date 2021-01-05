using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        readonly ILogger<GreeterService> _logger;
        readonly Random Random = new Random((int)DateTime.Now.Ticks);
        readonly string letters = "qwertyuiopasdfghjklzxcvbnm ";

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello, " + request.Name
            });
        }

        public override Task<EchoData> Echo(EchoData request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            return Task.FromResult(new EchoData
            {
                Message = "Echo: " + request.Message
            });
        }

        public override Task<TypesData> TestTypes(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            return Task.FromResult(new TypesData
            {
                BoolType = GetBool(),
                DoubleType = Random.NextDouble(),
                Fixed32Type = (uint)Random.Next(100),
                Fixed64Type = (ulong)Random.Next(100),
                FloatType = (float)Random.NextDouble(),
                Int32Type = Random.Next(100),
                Int64Type = Random.Next(100),
                Sfixed32Type = Random.Next(100),
                Sfixed64Type = Random.Next(100),
                Sint32Type = Random.Next(100),
                Sint64Type = Random.Next(100),
                Uint32Type = (uint)Random.Next(100),
                Uint64Type = (ulong)Random.Next(100),
                StringType = GetString(),
                DurationType = Duration.FromTimeSpan(TimeSpan.FromDays(Random.Next(12))),
                TimestampType = Timestamp.FromDateTime(DateTime.UtcNow),
                BytesType = ByteString.CopyFromUtf8(GetString())
            });
        }

        public override Task<NullableTypesData> TestNullableTypes(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            return Task.FromResult(new NullableTypesData
            {
                BoolType = GetBool() ? GetBool() : null,
                BytesType = GetBool() ? ByteString.CopyFromUtf8(GetString()) : null,
                DoubleType = GetBool() ? Random.NextDouble() : null,
                FloatType = GetBool() ? (float?)Random.NextDouble() : null,
                IntType = GetBool() ? Random.Next(100) : null,
                LongType = GetBool() ? Random.Next(100) : null,
                StringType = GetBool() ? GetString() : string.Empty,
                UintType = GetBool() ? (uint)Random.Next(100) : null,
                UlongType = GetBool() ? (ulong)Random.Next(100) : null
            });
        }

        public override Task<CollectionsData> TestCollection(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            CollectionsData result = new CollectionsData();

            for (int i = 0; i < Random.Next(10); i++)
                result.Names.Add(GetString());

            for (int i = 0; i < Random.Next(10); i++)
                result.Numbers.Add(Random.Next(100));

            for (int i = 0; i < Random.Next(10); i++)
                result.IdName.Add(i, GetString());

            for (int i = 0; i < Random.Next(10); i++)
                result.GuidValue.Add(Guid.NewGuid().ToString(), (float)Random.NextDouble());

            return Task.FromResult(result);
        }

        public override Task<Status> TestAny(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            return GetBool() ? Task.FromResult(new Status
            {
                Detail = Any.Pack(new HelloRequest { Name = GetString() }),
                Message = "HelloRequest"
            }) : Task.FromResult(new Status
            {
                Detail = Any.Pack(new HelloReply { Message = GetString() }),
                Message = "HelloReply"
            });
        }

        public override Task<PersonResponse> GetPerson(PersonRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            if (request.Id == 0)
                return Task.FromResult(new PersonResponse());

            return request.Id > 0 ? Task.FromResult(new PersonResponse { Person = new Person { Address = GetString(), Name = GetString(), Age = Random.Next(2, 100) } }) :
                                    Task.FromResult(new PersonResponse { Error = new Error { Code = Random.Next(10), Message = "Person not found!" } });
        }

        public override async Task ServerTimeStream(Empty request, IServerStreamWriter<StreamData> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new StreamData { Tikcs = DateTime.Now.Ticks });
            }
        }

        public override async Task<HelloReply> ClientTimeStream(IAsyncStreamReader<StreamData> requestStream, ServerCallContext context)
        {
            _logger.LogInformation("Host: {0}, Peer: {1}, Method: {2}", context.Host, context.Peer, context.Method);

            List<StreamData> streams = new List<StreamData>();

            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
                streams.Add(requestStream.Current);

            return new HelloReply { Message = DateTime.FromBinary(streams.Last().Tikcs).ToString("o") };
        }

        public override async Task Chat(IAsyncStreamReader<HelloReply> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
                await responseStream.WriteAsync(new HelloReply { Message = string.Join("", requestStream.Current.Message.Reverse()) });
        }

        string GetString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Random.Next(20); i++)
                sb.Append(letters[Random.Next(letters.Length)]);

            return sb.ToString();
        }

        bool GetBool() => Random.Next(100) > 50;
    }
}
