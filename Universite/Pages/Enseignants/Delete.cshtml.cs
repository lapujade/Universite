using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Pages.Enseignants
{
    public class DeleteModel : PageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public DeleteModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enseignant Enseignant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Enseignant = await _context.Enseignant.FirstOrDefaultAsync(m => m.ID == id);

            if (Enseignant == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Enseignant = await _context.Enseignant.FindAsync(id);

            if (Enseignant != null)
            {
                _context.Enseignant.Remove(Enseignant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
