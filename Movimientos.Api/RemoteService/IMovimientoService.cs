using Movimientos.Api.RemoteModel;
using Movimientos.Api.Utils;
using System.Threading.Tasks;

namespace Movimientos.Api.RemoteService
{
    public interface IMovimientoService
    {
        Task<Response> GetCuenta(string NumeroCuenta);
        Task<Response> GetCuentasPorCliente(int IdCliente);
        Task<Response> ActualizarSaldoCuenta(CuentaRemote cuenta);
    }
}
