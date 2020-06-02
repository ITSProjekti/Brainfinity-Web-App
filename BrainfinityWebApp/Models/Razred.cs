using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public class Razred
    {
        public int Id { get; set; }
        public string RazredNaziv { get; set; }

        [RegularExpression(@"\d{1,3}", ErrorMessage = "Ovo polje je obavezno")]
        public int NivoSkoleId { get; set; }

        public virtual NivoSkole NivoSkole { get; set; }
    }
}