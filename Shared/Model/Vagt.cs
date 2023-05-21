using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Vagt
    {

        [Key]
        [Required]
        public int ID { get; set; } = 0;

        [Required]
        public string Navn { get; set; }

        [Required]
        public int Kategori { get; set; } = 0;

        
        public int Point { get; set; } = 0;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime Slut { get; set; }

        [Required]
        public int Antal { get; set; } = 1;

        [Required]
        public string? Beskrivelse { get; set; }
        public bool isLocked { get; set; } = false;
        public int? Pladser_Tilbage { get; set; }


        public static List<Vagt> ToList()
        {
            throw new NotImplementedException();
        }

       
    }
}