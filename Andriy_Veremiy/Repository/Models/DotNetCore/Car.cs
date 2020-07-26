using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.DotNetCore
{
    public class Car
    {
        public int ID { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public float CarMileage { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int Horsepower { get; set; }
        public string EngineType { get; set; }
        public decimal Price { get; set; }
    }
}
