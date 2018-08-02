using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class Enseignant
    {
        // Clé primaire
        public int ID { get; set; }
        [Required]
        public String Nom { get; set; }
        [Required]
        public String Prenom { get; set; }

        // Données calculée non persistante
        [Display(Name = "Nom Enseignant")]
        public string NomComplet
        {
            get
            {
                return Nom + " " + Prenom;
            }
        }

        // Lien de navigation
        [Display(Name = "UEs enseignées")]
        public ICollection<Enseigne> LesEnseigne{ get; set; }
    }
}
