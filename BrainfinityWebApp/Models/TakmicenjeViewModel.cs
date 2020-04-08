using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public class TakmicenjeViewModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime datumOd { get; set; }

        public DateTime DatumDo { get; set; }
    }
}