using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class UE
    {
        // Clé primaire
        public int ID { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Intitule { get; set; }

        // Clé étrangère vers la formation
        public int FormationID { get; set; }

        // Données calculées non persistante
        [Display(Name = "Nom UE")]
        public string NomComplet
        {
            get
            {
                return Numero + " - " + Intitule;
            }
        }

        // Liens de navigation
        [Display(Name = "Dans la formation")]
        public Formation LaFormation { get; set; }
        [Display(Name = "Les notes obtenues")]
        public ICollection<Note> LesNotes { get; set; }
        [Display(Name = "Les enseignants")]
        public ICollection<Enseigne> LesEnseigne { get; set; }
    }
}
