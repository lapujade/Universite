using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models.ViewModels
{
    public class EnseignantIndexData
    {
        public IEnumerable<Enseignant> LesEnseignant { get; set; }
        public IEnumerable<UE> LesUE { get; set; }
        public IEnumerable<Enseigne> LesEnseigne { get; set; }
    }
}
