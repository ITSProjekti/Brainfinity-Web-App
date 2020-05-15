using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public class GrupaZadatakaViewModel
    {
        public int Id { get; set; }
        public string Razred { get; set; }
        public string NivoSkole { get; set; }
        public TakmicenjeViewModel Takmicenje { get; set; }
        public int TakmicenjeId { get; set; }
    }
}