using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;

namespace BrainfinityWebApp.ViewModels
{
    public class NovoTakmicenjeSvaTakmicenja
    {
        public IEnumerable<TakmicenjeViewModel> SvaTakmicenja { get; set; }
        public TakmicenjeViewModel Takmicenje { get; set; }
    }
}