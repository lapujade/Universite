using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Universite.Models;

namespace Universite.Pages.UEs
{
    public class DeleteModel : PageModel
    {
        private readonly Universite.Models.UniversiteContext _context;

        public DeleteModel(Universite.Models.UniversiteContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UE UE { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UE = await _context.UE
                .Include(u => u.LaFormation).FirstOrDefaultAsync(m => m.ID == id);

            if (UE == null)
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

            UE = await _context.UE.FindAsync(id);

            if (UE != null)
            {
                _context.UE.Remove(UE);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
