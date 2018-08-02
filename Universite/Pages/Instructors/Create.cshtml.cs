using Universite.Models;
using Universite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Universite.Pages.Instructors
{
    public class CreateModel : EnseignePageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public CreateModel(UniversiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var enseignant = new Enseignant();
            enseignant.LesEnseigne = new List<Enseigne>();

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            AddEnseigne(_context, enseignant);
            return Page();
        }

        [BindProperty]
        public Enseignant Enseignant { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedUE)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newEnseignant = new Enseignant();
            if (selectedUE != null)
            {
                newEnseignant.LesEnseigne = new List<Enseigne>();
                foreach (var ue in selectedUE)
                {
                    var newEnseigne= new Enseigne
                    {
                        UEID = int.Parse(ue)
                    };
                    newEnseignant.LesEnseigne.Add(newEnseigne);
                }
            }

            if (await TryUpdateModelAsync<Enseignant>(
                newEnseignant,
                "Enseignant",
                i => i.Nom, i => i.Prenom))
            {
                _context.Enseignant.Add(newEnseignant);                
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            AddEnseigne(_context, newEnseignant);
            return Page();
        }
    }
}