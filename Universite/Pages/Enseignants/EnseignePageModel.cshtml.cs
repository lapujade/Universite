using Universite.Models;
using Universite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Universite.Pages.Enseignants
{
    public class EnseignePageModel : PageModel
    {

        public List<CheckEnseigne> LesCheckEnseigne;

        public void LoadUECheckBoxData(UniversiteContext context, 
                                               Enseignant enseignant)
        {
            var lesUE = context.UE;
            var lesEnseigne = new HashSet<int>(enseignant.LesEnseigne.Select(c => c.UEID));
            LesCheckEnseigne = new List<CheckEnseigne>();
            foreach (var UE in lesUE)
            {
                LesCheckEnseigne.Add(new CheckEnseigne
                {
                    UEID = UE.ID,
                    NomComplet = UE.NomComplet,
                    IsCheck = lesEnseigne.Contains(UE.ID)
                });
            }
        }

        public void UpdateEnseigne(UniversiteContext context, 
            string[] selectedUE, Enseignant enseignantAModifier)
        {
            if (selectedUE == null)
            {
                enseignantAModifier.LesEnseigne = new List<Enseigne>();
                return;
            }

            var selectedUEHS = new HashSet<string>(selectedUE);
            var enseigne = new HashSet<int>
                (enseignantAModifier.LesEnseigne.Select(c => c.LUE.ID));
            foreach (var ue in context.UE)
            {
                if (selectedUEHS.Contains(ue.ID.ToString()))
                {
                    if (!enseigne.Contains(ue.ID))
                    {
                        enseignantAModifier.LesEnseigne.Add(
                            new Enseigne
                            {
                                EnseignantID = enseignantAModifier.ID,
                                UEID = ue.ID
                            });
                    }
                }
                else
                {
                    if (enseigne.Contains(ue.ID))
                    {
                        Enseigne enseigneAEnlever
                            = enseignantAModifier
                                .LesEnseigne
                                .SingleOrDefault(i => i.UEID == ue.ID);
                        context.Remove(enseigneAEnlever);
                    }
                }
            }
        }
    }
}