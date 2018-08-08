using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models.ViewModels
{
    public class SaisieNotesData
    {
        [Display(Name = "Etudiant")]
        public Etudiant LEtudiant { get; set; }
        public UE LUE { get; set; }
        public Note LaNote { get; set; }
        // NoteString permet d'afficher soit une note soit une chaine vide
        [Display(Name = "Note /20")]
        public String NoteString { get; set; }

    }
}
