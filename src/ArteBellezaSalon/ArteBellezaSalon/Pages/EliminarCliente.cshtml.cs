using ArteBellezaSalon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ArteBellezaSalon.Pages
{
    public class EliminarClienteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EliminarClienteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Cliente = await _context.Clientes.FindAsync(id);

            if (Cliente == null)
            {
                return RedirectToPage("/ListaClientes");
            }

            _context.Clientes.Remove(Cliente);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ListaClientes");
        }
    }
}
