using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public class Zadatak
    {
        public int Id { get; set; }
        public string ZadatakNaziv { get; set; }
        public GrupaZadataka GrupaZadataka { get; set; }
        public int GrupaZadatakaId { get; set; }
    }
}