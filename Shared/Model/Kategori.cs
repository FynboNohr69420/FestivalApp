using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Kategori
    {
        [Key]
        public int ID { get; set; } = 0;
        public string Navn { get; set; } = "";
    }
}
