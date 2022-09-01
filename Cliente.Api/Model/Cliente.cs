using Cliente.Api.Persistencia;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cliente.Api.Modelo
{
    [Table("tbCliente", Schema = "gen")]
    public class Cliente:Persona
    {

        [Required(ErrorMessage = "El Campo Contraseña es Requerido")]
        public string Contrasena { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public bool Estado { get; set; }
    }
}
