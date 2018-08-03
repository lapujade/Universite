using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universite.Models;
using Universite.Models.ViewModels;

namespace Universite.Pages.SaisieNotes
{
    public class SaisieNotesModel : PageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public SaisieNotesModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }
        public List<SaisieNotesData> NotesDatas { get; set; }
        [BindProperty]
        public IList<Etudiant> TousEtudiants { get; set; }
        [BindProperty]
        public IList<UE> LesUE{ get; set; }
        public Formation LaFormation { get; set; }
        public UE ue { get; set; }
        public int UEID { get; set; }
        public int EtudiantID { get; set; }
        public async Task<IActionResult> OnGetAsync(int? ueID, int? etudiantID)

        {
            LesUE = await _context.UE.ToListAsync();
            NotesDatas = new List<SaisieNotesData>();
            TousEtudiants = await _context.Etudiant
                  .Include(i => i.LaFormation)
                  .Include(i => i.LesNotes)
                    .ThenInclude(i => i.LEtudiant)
                  .Include(i => i.LesNotes)
                    .ThenInclude(i => i.LUe)
                  .AsNoTracking()
                  .OrderBy(i => i.NomComplet)
                  .ToListAsync();
            ViewData["LesUE"] = new SelectList(_context.UE, "ID", "NomComplet");
            if (ueID != null)
            {
                // Une UE a été sélectionnée
                UEID = ueID.Value;
                ue = _context.UE.Include(i=>i.LaFormation).Include(i => i.LesNotes).Where(
                    i => i.ID == ueID.Value).Single();
                LaFormation = ue.LaFormation;
                TousEtudiants = await _context.Etudiant.Include(i => i.LaFormation).Include(i => i.LesNotes).ThenInclude(i => i.LUe).Where(i => i.FormationID == LaFormation.ID).ToListAsync(); ;
                foreach (Etudiant e in TousEtudiants)
                {
                    SaisieNotesData snd = new SaisieNotesData();
                    snd.LUE = ue;
                    snd.LEtudiant = e;
                    snd.LaNote = null;
                    // Récupération de la note si elle existe
                    Note LaNote = _context.Note.Where(i=>i.UEID==UEID).Single();
                    if (LaNote !=null)
                    {
                        snd.LaNote = LaNote;
                    }
                }
            }
            return Page();
        }
    }
}