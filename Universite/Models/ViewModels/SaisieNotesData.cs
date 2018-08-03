using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models.ViewModels
{
    public class SaisieNotesData
    {
        public Etudiant LEtudiant { get; set; }
        public UE LUE { get; set; }
        public Note LaNote { get; set; }

    }
}
