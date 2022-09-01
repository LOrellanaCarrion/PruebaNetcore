using System.ComponentModel.DataAnnotations;

namespace Cliente.Api.ViewModel
{
    public class PersonaCreateViewModel
    {

        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Edad { get; set; }
    }
}
