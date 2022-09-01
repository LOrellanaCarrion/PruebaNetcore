using System.ComponentModel.DataAnnotations;

namespace Cuentas.Api.RemoteModel
{
    public class ClienteRemote
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Edad { get; set; }
        public string Contrasena { get; set; }
        public string Estado { get; set; }
    }
}
