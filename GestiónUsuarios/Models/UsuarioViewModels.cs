using System.ComponentModel.DataAnnotations;

namespace GestiónUsuarios.Models
{
    public class UsuarioViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El documento es obligatorio")]
        [RegularExpression(@"^[0-9]{7,8}$", ErrorMessage = "Formato de documento inválido")]
        public string Documento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        [StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un perfil")]
        public string Rol { get; set; } = string.Empty;

        public string NombreCompleto => $"{Apellido}, {Nombre}";
    }
}
