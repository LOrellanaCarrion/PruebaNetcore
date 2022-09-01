using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cliente.Api.ViewModel
{
    public abstract class PersonaViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Edad { get; set; }
    }
}
