using Cliente.Api.Persistencia;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cliente.Api.Modelo
{
    [Table("tbPersona", Schema = "gen")]
    public abstract class Persona
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage ="El Campo {0} es Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public string Edad { get; set; }

    }
}
