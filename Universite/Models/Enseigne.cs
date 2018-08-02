using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Models
{
    public class Enseigne
    {
        public int ID { get; set; }
        public int EnseignantID { get; set; }
        public int UEID { get; set; }
        public Enseignant LEnseignant { get; set; }
        public UE LUE { get; set; }
    }
}
