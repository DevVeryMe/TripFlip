using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq.Mapping;

namespace RepositoryLibrary.Models
{
    [Table(Name = "Cars")]
    public class Car
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string CarName { get; set; }

        [Column]
        public string CarModel { get; set; }

        [Column]
        public double CarMileage { get; set; }

        [Column]
        public DateTime ManufactureDate { get; set; }

        [Column]
        public int Horsepower { get; set; }

        [Column]
        public string EngineType { get; set; }

        [Column]
        public decimal Price { get; set; }
    }
}
