using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public class GrupaZadataka
    {
        public int Id { get; set; }
        public Razred Razred { get; set; }

        [RegularExpression(@"\d{1,3}", ErrorMessage = "Ovo polje je obavezno")]
        public int RazredId { get; set; }

        public Takmicenje Takmicenje { get; set; }
        public int TakmicenjeId { get; set; }
    }
}