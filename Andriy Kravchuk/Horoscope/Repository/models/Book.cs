using System;

namespace Horoscope.models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public DateTime ReleaseDate { get; set; }

        public double Rating { get; set; }

        public string Publisher { get; set; }
    }
}