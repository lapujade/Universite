using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Models
{
    public class UniversiteContext : DbContext
    {
        public UniversiteContext (DbContextOptions<UniversiteContext> options)
            : base(options)
        {
        }

        public DbSet<Universite.Models.Enseignant> Enseignant { get; set; }

        public DbSet<Universite.Models.Etudiant> Etudiant { get; set; }

        public DbSet<Universite.Models.Formation> Formation { get; set; }

        public DbSet<Universite.Models.UE> UE { get; set; }

        public DbSet<Universite.Models.Enseigne> Enseigne { get; set; }
    }
}
