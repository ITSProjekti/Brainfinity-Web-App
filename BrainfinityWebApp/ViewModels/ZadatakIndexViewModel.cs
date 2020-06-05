using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;

namespace BrainfinityWebApp.ViewModels
{
    public class ZadatakIndexViewModel
    {
        public IEnumerable<Zadatak> SviZadaci { get; set; }
        public Zadatak Zadatak { get; set; }
        public int GrupaId { get; set; }
    }
}