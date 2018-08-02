
using Universite.Models;
using Universite.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Universite.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public IndexModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }

        public EnseignantIndexData UnEnseignement { get; set; }
        public int EnseignantID { get; set; }
        public int UEID { get; set; }

        public async Task OnGetAsync(int? id, int? ueID)
        {
            UnEnseignement = new EnseignantIndexData();
            UnEnseignement.LesEnseignant = await _context.Enseignant
                  .Include(i => i.LesEnseigne)
                    .ThenInclude(i => i.LUE)
                  .AsNoTracking()
                  .ToListAsync();

            int nb = UnEnseignement.LesEnseignant.Count();

            if (id != null)
            {
                EnseignantID = id.Value;
                Enseignant enseignant = UnEnseignement.LesEnseignant.Where(
                    i => i.ID == id.Value).Single();
                UnEnseignement.LesUE = enseignant.LesEnseigne.Select(s => s.LUE);
            }

            if (ueID != null)
            {
                UEID = ueID.Value;
                UnEnseignement.LesEnseigne = UnEnseignement.LesUE.Where(
                    x => x.ID == ueID).Single().LesEnseigne;
            }
        }
    }
}
