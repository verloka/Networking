using System;

namespace Caching.Models
{
    public class RecordModel
    {
        public int Id { get; set; }

        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int Num3 { get; set; }

        public RecordModel(int Id)
        {
            this.Id = Id;

            var rnd = new Random();

            Num1 = rnd.Next(0, 100);
            Num2 = rnd.Next(101, 200);
            Num3 = rnd.Next(201, 300);
        }
    }
}
