using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class Formation
    {
        // Clé primaire
        public int ID { get; set; }
        [Required]
        public string IntituleDiplome { get; set; }
        [Required]
        public int AnneeDiplome { get; set; }

        // Données calculées non persistante
        [Display(Name = "Nom Formation")]
        public string NomComplet
        {
            get
            {
                return IntituleDiplome + " - Année " + AnneeDiplome;
            }
        }

        // Liens de navigation
        public ICollection<UE> LesUE { get; set; }
    }
}
