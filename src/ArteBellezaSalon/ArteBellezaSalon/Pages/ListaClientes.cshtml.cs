using ArteBellezaSalon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArteBellezaSalon.Pages
{
    public class ListaClientesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ListaClientesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cliente> Clientes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Busqueda { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FiltroTipo { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(Busqueda))
            {
                var busquedaLower = Busqueda.ToLower();
                query = query.Where(c =>
                    c.Nombre.ToLower().Contains(busquedaLower) ||
                    c.Apellido.ToLower().Contains(busquedaLower) ||
                    c.Cedula.Contains(busquedaLower) || 
                    c.Email.ToLower().Contains(busquedaLower)
                );
            }

            if (!string.IsNullOrEmpty(FiltroTipo))
            {
                query = query.Where(c => c.Tipo == FiltroTipo);
            }

            Clientes = await query.ToListAsync();
        }
    }
}
