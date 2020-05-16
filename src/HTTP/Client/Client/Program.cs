using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using DataClient dc = new DataClient("localhost", 6000);

            Console.WriteLine("Records on the server:");
            foreach (var record in await dc.GetAll())
                OutputRecord(record);

            Console.WriteLine("\nAdd the new records:");
            for (int i = 0; i < 5; i++)
                await dc.Add(new RecordModel
                {
                    Title = i.ToString(),
                    Type = i % 2 == 0 ? "even" : "odd",
                    Comment = $"comment #{i}"
                });

            Console.WriteLine("\nRecords on the server:");
            foreach (var record in await dc.GetAll())
                OutputRecord(record);

            Console.WriteLine("\nGet record with the title - 1:");
            OutputRecord(await dc.GetByTitle("1"));

            Console.WriteLine("\nRecords on the server with the type 'odd':");
            foreach (var record in await dc.GetByType("odd"))
                OutputRecord(record);

            Console.WriteLine("\nDelete the record with Title=4");
            await dc.Delete("4");

            Console.WriteLine("\nRecords on the server:");
            foreach (var record in await dc.GetAll())
                OutputRecord(record);

            Console.WriteLine("\nUpdate the comment for the record with Title=0");
            await dc.UpdateComment("4", "A new comment");

            Console.WriteLine("\nRecords on the server:");
            foreach (var record in await dc.GetAll())
                OutputRecord(record);


            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        static void OutputRecord(RecordModel model)
        {
            Console.WriteLine($"{"{"} Title: {model.Title}; Type: {model.Type}; Comment: {model.Comment}; {"}"}");
        }
    }
}
