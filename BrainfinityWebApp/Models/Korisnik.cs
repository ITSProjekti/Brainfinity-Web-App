using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrainfinityWebApp.Models
{
    public class Korisnik
    {
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [EmailAddress(ErrorMessage = "E-mail adresa nije validna")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        [RegularExpression("^(?:(?=.*[a-z])(?:(?=.*[A-Z])(?=.*[\\d\\W])|(?=.*\\W)(?=.*\\d))|(?=.*\\W)(?=.*[A-Z])(?=.*\\d)).{8,}$",
            ErrorMessage = "Lozinka mora sadržati minimum 8 karaktera, veliko i malo slovo i broj")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        public string PotvrdiPassword { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public byte[] Slika { get; set; }
        public byte[] Logo { get; set; }
        public string KorisnickiTip { get; set; }
    }
}