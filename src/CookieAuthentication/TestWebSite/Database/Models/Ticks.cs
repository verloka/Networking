using System;

namespace TestWebSite.Database.Models
{
    public class Ticks
    {
        public int ID { get; set; }
        public DateTime DT { get; set; }

        public Ticks()
        {
            DT = DateTime.Now;
        }
    }
}
