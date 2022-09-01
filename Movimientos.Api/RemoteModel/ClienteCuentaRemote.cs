namespace Movimientos.Api.RemoteModel
{
    public class ClienteCuentaRemote
    {
        public string NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string Cliente { get; set; }
    }
}
