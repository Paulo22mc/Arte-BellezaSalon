using System.ComponentModel.DataAnnotations;

namespace ArteBellezaSalon.Data
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [MaxLength(20)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Telefono es obligatorio")]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [MaxLength(50)]
        public string Tipo { get; set; }
    }
}
