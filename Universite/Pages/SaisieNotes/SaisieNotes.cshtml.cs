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
        public IList<Etudiant> TousEtudiants { get; set; }
        public IList<UE> LesUE{ get; set; } 
        public List<SelectListItem> SelectUESData { get; private set; }
        public Formation LaFormation { get; set; }
        public UE ue { get; set; }
        // Element selectionné dans l'attribut Select
        [BindProperty]
        public int UEID { get; set; }
        [BindProperty]
        public int UEIDUPDATE { get; set; }
        // Liste des notes saisies pas le user
        [BindProperty]
        public String[] noteString  {get; set; }
        // Liste des étudiants concernés
        [BindProperty]
        public int[] etudiantID { get; set; }

        public async Task<IActionResult> OnGetAsync()

        {
            await RemplirListeUEAsync();
            return Page();
        }
        // id c'est l'ID de l'UE sélectionnée
        //public async Task<IActionResult>OnPostUpdateAsync(int? ueid, String[] noteString, int[] etudiantID)
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (UEIDUPDATE != 0)
            {
                UEID = UEIDUPDATE;
                // Etape 1
                // Une UE a été sélectionnée
                // Remplissage de la list box des UES
                await RemplirListeUEAsync();
                // Si c'est un postback olors
                // les notes ont été modifiées, enregistrement des modifications
                if (etudiantID != null)
                {
                    for (int i = 0; i < etudiantID.Length; i++)
                    {
                        int etudid = System.Convert.ToInt32(etudiantID[i]);
                        float noteFloat = (float)System.Convert.ToDouble(noteString[i]);
                        try
                        {
                            Note noteAMettreAJour = _context.Note.Where(n => n.UEID == UEID && n.EtudiantID == etudid).Single();
                            // La note existe déjà, on la met à jour
                            if (noteString[i] != null)
                            {
                                // L'eneignant a modifié une note existante
                                noteAMettreAJour.Valeur = noteFloat;
                            }
                            else
                            {
                                // L'enseignant a supprimé une note existante
                                _context.Note.Remove(noteAMettreAJour);
                            }
                        }
                        catch
                        {
                            if (noteString[i] != null)
                            { 
                                // L'enseignant a rajouté une nouvelle note
                                Note newNote = new Note();
                                newNote.EtudiantID = etudid;
                                newNote.UEID = UEID;
                                newNote.Valeur = noteFloat;
                                await _context.Note.AddAsync(newNote);
                            }
                            else
                            {
                                // Etudiant sans note. Ne pas ajouter d'enregistrement
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                // Remplissage de la iste des étudiants de la formation avec le cas échéant leur note.
                ue = _context.UE.Include(i => i.LaFormation).Include(i => i.LesNotes).Where(
                    i => i.ID == UEID).Single();
                LaFormation = ue.LaFormation;
                TousEtudiants = await _context.Etudiant.Include(i => i.LaFormation).Include(i => i.LesNotes).ThenInclude(i => i.LUe).Where(i => i.FormationID == LaFormation.ID).ToListAsync(); ;
                NotesDatas = new List<SaisieNotesData>();
                foreach (Etudiant e in TousEtudiants)
                {
                    SaisieNotesData snd = new SaisieNotesData();
                    snd.LUE = ue;
                    snd.LEtudiant = e;
                    snd.LaNote = null;
                    // Récupération de la note si elle existe
                    try
                    {
                        Note LaNote = _context.Note.Where(i => i.UEID == UEID && i.EtudiantID == e.ID).Single();
                        snd.LaNote = LaNote;
                        snd.NoteString = LaNote.Valeur.ToString();
                    }
                    catch
                    {
                        snd.LaNote = new Note();
                        snd.LaNote.EtudiantID = e.ID;
                        snd.LaNote.LEtudiant = e;
                        snd.LaNote.UEID = ue.ID;
                        snd.LaNote.LUe = ue;
                        snd.NoteString = null;
                    }
                    NotesDatas.Add(snd);
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostLoadAsync()
        {
            // Post appelé uniquement après le changement de combo box
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Attention si il existe déjà des notes à enregistrer
            if (etudiantID.Length != 0)
            {
                // Des notes ont déjà été chargées. Prévenir

            }
            // Une UE a été sélectionnée
            // Re-remplissage de la list box des UES
            await RemplirListeUEAsync();

            // Remplissage de la iste des étudiants de la formation avec le cas échéant leur note.
            ue = _context.UE.Include(i => i.LaFormation).Include(i => i.LesNotes).Where(
                i => i.ID == UEID).Single();
            LaFormation = ue.LaFormation;
            TousEtudiants = await _context.Etudiant.Include(i => i.LaFormation).Include(i => i.LesNotes).ThenInclude(i => i.LUe).Where(i => i.FormationID == LaFormation.ID).ToListAsync(); ;
            NotesDatas = new List<SaisieNotesData>();
            foreach (Etudiant e in TousEtudiants)
            {
                SaisieNotesData snd = new SaisieNotesData();
                snd.LUE = ue;
                snd.LEtudiant = e;
                snd.LaNote = null;
                // Récupération de la note si elle existe
                try
                {
                    Note LaNote = _context.Note.Where(i => i.UEID == UEID && i.EtudiantID == e.ID).Single();
                    snd.LaNote = LaNote;
                    snd.NoteString = LaNote.Valeur.ToString();
                }
                catch
                {
                    snd.LaNote = null;
                    snd.NoteString = null;
                }
                NotesDatas.Add(snd);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? ueid, String[] noteString, int[] etudiantID)
        {
            // Post appelé uniquement après le changement de combo box
            if (!ModelState.IsValid)
            {
                return Page();
            } 
            // Normalement pas d'appel du post
            return Page();
        }
        private async Task RemplirListeUEAsync()
        {
            // Initialisation de la page. Chargement de la liste déroulante des UES
            LesUE = await _context.UE.ToListAsync();
            SelectUESData = new List<SelectListItem>();
            SelectUESData.Add(new SelectListItem
            {
                Text = "Choisir une UE",
                Value = ""
            });
            foreach (UE u in LesUE)
            {
                SelectUESData.Add(new SelectListItem
                {
                    Text = u.NomComplet,
                    Value = u.ID.ToString()
                });
            }
            ViewData["SelectUEData"] = new SelectList(SelectUESData, "Value", "Text");
            NotesDatas = null;
        }
        private void RemplirSaisieNotesData()
        {

        }
    }
}