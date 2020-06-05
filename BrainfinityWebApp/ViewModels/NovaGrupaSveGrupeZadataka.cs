using BrainfinityWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.ViewModels
{
    public class NovaGrupaSveGrupeZadataka
    {
        public IEnumerable<GrupaZadataka> SveGrupe { get; set; }
        public GrupaZadataka GrupaZadataka { get; set; }
        public Zadatak Zadatak { get; set; }

        [RegularExpression(@"\d{1,3}", ErrorMessage = "Ovo polje je obavezno")]
        public int RazredId { get; set; }

        public IEnumerable<Razred> Razredi { get; set; }

        [RegularExpression(@"\d{1,3}", ErrorMessage = "Ovo polje je obavezno")]
        public int NivoSkoleId { get; set; }

        public IEnumerable<NivoSkole> NivoiSkole { get; set; }

        public string NazivTakmicenja { get; set; }
        public int TakmicenjeId { get; set; }
    }
}