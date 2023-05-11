using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Bruger
    {

        [Key]
        public int Id { get; set; } = 0;
        public string Fornavn { get; set; } = " ";
        public string Efternavn { get; set; } = " ";
        public int Telefonnummer { get; set; } = 0;
        public string Adresse { get; set; } = " ";
        public string Email { get; set; } = " ";
        [Required]
        [DataType(DataType.Date)]
        public DateTime Fødselsdag { get; set; }
        public string Password { get; set; } = " ";
        public bool IsKoordinator { get; set; }

        public static List<Bruger> ToList()
        {
            throw new NotImplementedException();
        }
    }
}

 