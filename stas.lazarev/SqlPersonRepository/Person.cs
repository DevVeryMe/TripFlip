using System;

namespace SqlPersonRepository
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Age { get; set; }
        public bool? IsAdult { get; set; }
        public decimal? Balance { get; set; }
    }
}
