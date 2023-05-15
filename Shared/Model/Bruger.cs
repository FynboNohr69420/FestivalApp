using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Bruger
    {

        [Key]
        public int ID { get; set; } = 0;
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public int Telefonnummer { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime Fødselsdag { get; set; }
        public string Password { get; set; }
        public bool IsKoordinator { get; set; }
    }
}

 