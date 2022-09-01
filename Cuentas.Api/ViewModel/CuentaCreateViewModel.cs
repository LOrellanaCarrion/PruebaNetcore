using System.ComponentModel.DataAnnotations;

namespace Cuentas.Api.ViewModel
{
    public class CuentaCreateViewModel
    {
    
        [MaxLength(100, ErrorMessage ="La Longitud del Campo {0} Supera la Longitud Permitida")]
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        [Required(ErrorMessage = "El Campo {0} es Requerido")]
        public int IdCliente { get; set; }
    }
}
