﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Vagt
    {

        [Key]
        public int ID { get; set; } = 0;
        public string Navn { get; set; } = " ";
        public string Kategori { get; set; } = " ";
        public int Point { get; set; } = 0;
        public DateTime Start { get; set; }
        public DateTime Slut { get; set; }
        public int Antal { get; set; } = 1;
        public string Beskrivelse { get; set; } = " ";
        

        public static List<Vagt> ToList()
        {
            throw new NotImplementedException();
        }
    }
}