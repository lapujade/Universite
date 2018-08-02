using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Pages.Etudiants
{
    public class DetailsModel : PageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public DetailsModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }

        public Etudiant Etudiant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Etudiant = await _context.Etudiant
                .Include(e => e.LaFormation).FirstOrDefaultAsync(m => m.ID == id);

            if (Etudiant == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
