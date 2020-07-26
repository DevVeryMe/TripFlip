using System;
using System.ComponentModel.DataAnnotations;

namespace SqlPersonRepository
{
    public class Person
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [StringLength(1)]
        public string Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Age { get; set; }
        public bool? IsAdult { get; set; }
        public decimal? Balance { get; set; }
    }
}
