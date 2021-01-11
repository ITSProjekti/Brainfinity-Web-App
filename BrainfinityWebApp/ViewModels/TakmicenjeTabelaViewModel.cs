using BrainfinityWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.ViewModels
{
    public class TakmicenjeTabelaViewModel
    {
        public IEnumerable<GrupaZadataka> GrupeZadataka { get; set; }
        public int OdabranaGurpaZadatakaId { get; set; }
    }
}