using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;

namespace BrainfinityWebApp.ViewModels
{
    public class NovoTakmicenjeSvaTakmicenja
    {
        public IEnumerable<Takmicenje> SvaTakmicenja { get; set; }
        public Takmicenje Takmicenje { get; set; }
    }
}