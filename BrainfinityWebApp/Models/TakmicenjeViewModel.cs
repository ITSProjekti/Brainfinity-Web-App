using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public enum Status
    {
        Arhivirano,
        Aktivno,
        Nastupajuce
    }

    public class TakmicenjeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Uneti datum nije odgovarajućeg formata")]
        [DataType(DataType.Date)]
        public DateTime datumOd { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Uneti datum nije odgovarajućeg formata")]
        [DataType(DataType.Date)]
        public DateTime DatumDo { get; set; }

        public string Slika { get; set; }
        public Status Status { get; set; }
    }
}