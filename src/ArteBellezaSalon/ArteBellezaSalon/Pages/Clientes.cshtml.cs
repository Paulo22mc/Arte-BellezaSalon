using ArteBellezaSalon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ArteBellezaSalon.Pages
{
    public class ClientesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ClientesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validar cédula única
            if (_context.Clientes.Any(c => c.Cedula == Cliente.Cedula))
            {
                ModelState.AddModelError("Cliente.Cedula", "Esta cédula ya está registrada.");
                return Page();
            }

            // Validar email único
            if (!string.IsNullOrEmpty(Cliente.Email) && _context.Clientes.Any(c => c.Email == Cliente.Email))
            {
                ModelState.AddModelError("Cliente.Email", "Este correo ya está registrado.");
                return Page();
            }

            _context.Clientes.Add(Cliente);
            _context.SaveChanges();

            TempData["Mensaje"] = "Cliente registrado correctamente!";
            return RedirectToPage("/Clientes");
        }
    }
}
