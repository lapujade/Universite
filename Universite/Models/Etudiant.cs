using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class Etudiant
    {
        public enum EnumGenre
        {
            Feminin, Masculin
        }
    
        // Clé primaire
        public int ID { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime Naissance { get; set; }
        public EnumGenre Genre { get; set; }
        [Display(Name = "Formation en cours")]
        public int FormationID { get; set; }

        // Données calculée non persistante
        [Display(Name = "Nom étudiant")]
        public string NomComplet
        {
            get
            {
                return Nom + " " + Prenom;
            }
        }
        // Lien de navigation vers les notes de l'étudiant
        public ICollection<Note> LesNotes { get; set; }
        [Display(Name = "Formation en cours")]
        public Formation LaFormation { get; set; }
    }
}
