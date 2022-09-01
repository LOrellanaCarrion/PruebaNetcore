using System.ComponentModel.DataAnnotations;

namespace Cliente.Api.ViewModel
{
    public class ClienteViewModel:PersonaViewModel
    {
        [Required(ErrorMessage = "El Campo Contraseña es Requerido")]
        public string Contrasena { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public bool Estado { get; set; }
    }
}
