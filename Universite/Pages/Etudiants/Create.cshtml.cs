using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Universite.Models;

namespace Universite.Pages.Etudiants
{
    public class CreateModel : PageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public CreateModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FormationID"] = new SelectList(_context.Formation, "ID", "NomComplet");
            return Page();
        }

        [BindProperty]
        public Etudiant Etudiant { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Etudiant.Add(Etudiant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}