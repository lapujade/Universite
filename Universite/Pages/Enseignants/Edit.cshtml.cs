using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Pages.Enseignants
{
    public class EditModel : EnseignePageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public EditModel(Universite.Models.UniversiteContext context)
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

            //Enseignant = await _context.Enseignant.FirstOrDefaultAsync(m => m.ID == id);
            Enseignant = await _context.Enseignant
                                 .Include(i => i.LesEnseigne).ThenInclude(i => i.LUE)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(m => m.ID == id);

            if (Enseignant == null)
            {
                return NotFound();
            }
            LoadUECheckBoxData(_context, Enseignant);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedUE)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var EnseignantAModifier = await _context.Enseignant
                .Include(i => i.LesEnseigne)
                    .ThenInclude(i => i.LUE)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Enseignant>(
                EnseignantAModifier,
                "Enseignant",
                i => i.Prenom, i => i.Nom))
            {
                UpdateEnseigne(_context, selectedUE, EnseignantAModifier);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateEnseigne(_context, selectedUE, EnseignantAModifier);
            LoadUECheckBoxData(_context, EnseignantAModifier);
            return RedirectToPage("./Index");
        }

        private bool EnseignantExists(int id)
        {
            return _context.Enseignant.Any(e => e.ID == id);
        }
    }
}
