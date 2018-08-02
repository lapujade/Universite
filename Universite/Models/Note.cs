using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class Note
    {
        // Clé primaire
        public int ID { get; set; }
        [Required]
        [Range(0, 20)]
        public float Valeur { get; set; }
        // Clé étrangère vers l'UE
        public int UEID { get; set; }
        // Clé étrangère vers l'étudiant
        public int EtudiantID { get; set; }

        // Liens de navigation
        [Display(Name = "Nom UE")]
        public UE LUe { get; set; }
        [Display(Name = "Nom Etudiant")]
        public Etudiant LEtudiant { get; set; }
    }
}
