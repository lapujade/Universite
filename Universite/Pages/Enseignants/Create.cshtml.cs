using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Universite.Models;

namespace Universite.Pages.Enseignants
{
    public class CreateModel : EnseignePageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public CreateModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Création d'un enseignant vide permettant de décocher toutes les checkbox
            var enseignant = new Enseignant();
            enseignant.LesEnseigne = new List<Enseigne>();

            // Chargement des données des checkBox. 
            LoadUECheckBoxData(_context, enseignant);
            return Page();
        }

        [BindProperty]
        public Enseignant Enseignant { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedUE)
        {
            // selectedUE est un tableau de String dont chaque case contient l'IDUE d'une UE cochée. 
            // Cette liste est envoyée automatiquement lorsque la page est Post

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
                    var newEnseigne = new Enseigne
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
            LoadUECheckBoxData(_context, newEnseignant);
            return RedirectToPage("./Index");
        }
    }
}