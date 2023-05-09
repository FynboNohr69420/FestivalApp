using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Frivillig
    {

        [Key]
        public int Id { get; set; } = 0;
        public string Navn { get; set; } = " ";
        public string Email { get; set; } = " ";
        [DataType(DataType.Date)]
        public DateTime Fødselsdagsdato { get; set; }
        public string Adresse { get; set; } = " ";
        public int Telefonnummer { get; set; } = 0;

        internal static List<Frivillig> ToList()
        {
            throw new NotImplementedException();
        }
    }
}

