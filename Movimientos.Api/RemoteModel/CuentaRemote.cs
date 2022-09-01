using System.ComponentModel.DataAnnotations;

namespace Movimientos.Api.RemoteModel
{
    public class CuentaRemote
    {
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }

        public int IdCliente { get; set; }
    }
}
