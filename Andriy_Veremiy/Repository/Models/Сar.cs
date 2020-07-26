using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq.Mapping;

namespace Repository.Models
{
    [Table(Name = "Cars")]
    public class Car
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string CarName { get; set; }

        [Column]
        public string CarModel { get; set; }

        [Column]
        public float CarMileage { get; set; }

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
