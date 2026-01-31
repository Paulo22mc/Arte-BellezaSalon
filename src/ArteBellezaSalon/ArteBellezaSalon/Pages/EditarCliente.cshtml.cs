using ArteBellezaSalon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ArteBellezaSalon.Pages
{
    public class EditarClienteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditarClienteModel(ApplicationDbContext context)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clienteOriginal = await _context.Clientes.FindAsync(Cliente.Id);
            if (clienteOriginal == null)
            {
                return RedirectToPage("/ListaClientes");
            }

            if (_context.Clientes.Any(c => c.Cedula == Cliente.Cedula && c.Id != Cliente.Id))
            {
                ModelState.AddModelError("Cliente.Cedula", "Esta cédula ya está registrada.");
                return Page();
            }

            if (!string.IsNullOrEmpty(Cliente.Email) &&
                _context.Clientes.Any(c => c.Email == Cliente.Email && c.Id != Cliente.Id))
            {
                ModelState.AddModelError("Cliente.Email", "Este correo ya está registrado.");
                return Page();
            }

            clienteOriginal.Cedula = Cliente.Cedula;
            clienteOriginal.Nombre = Cliente.Nombre;
            clienteOriginal.Apellido = Cliente.Apellido;
            clienteOriginal.Email = Cliente.Email;
            clienteOriginal.Telefono = Cliente.Telefono;
            clienteOriginal.Tipo = Cliente.Tipo;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Cliente actualizado correctamente!";

            return RedirectToPage("/ListaClientes");
        }

    }
}
