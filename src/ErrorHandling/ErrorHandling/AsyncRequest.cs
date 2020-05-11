using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public static class AsyncRequest
    {
        public static async Task<Result> Make(string Host)
        {
            Result result = new Result();

            WebRequest request = WebRequest.Create(new UriBuilder { Scheme = "http", Host = Host }.Uri);
            request.Method = "POST";

            using StreamWriter sw = new StreamWriter(request.GetRequestStream());

            var task = request.GetResponseAsync();

            result.Local = 1;

            try
            {
                var response = await task;

                using StreamReader sr = new StreamReader(response.GetResponseStream());
                result.Remote = await sr.ReadToEndAsync();

            }
            catch (WebException we)
            {
                ProcessException(we);
            }


            return result;
        }

        private static void ProcessException(WebException ex)
        {
            switch (ex.Status)
            {
                case WebExceptionStatus.ConnectFailure:
                case WebExceptionStatus.ConnectionClosed:
                case WebExceptionStatus.RequestCanceled:
                case WebExceptionStatus.PipelineFailure:
                case WebExceptionStatus.SendFailure:
                case WebExceptionStatus.KeepAliveFailure:
                case WebExceptionStatus.Timeout:
                    Console.WriteLine("We should retry connection attempts");
                    break;
                case WebExceptionStatus.NameResolutionFailure:
                case WebExceptionStatus.ProxyNameResolutionFailure:
                case WebExceptionStatus.ServerProtocolViolation:
                    Console.WriteLine("Prevent further attempts and notify consumers to check URL configurations");
                    break;
                case WebExceptionStatus.SecureChannelFailure:
                case WebExceptionStatus.TrustFailure:
                    Console.WriteLine("Authentication or security issue. Prompt for credentials and perhaps try again");
                    break;

            }
        }
    }
}
