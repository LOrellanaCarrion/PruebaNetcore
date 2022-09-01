using Cuentas.Api.Model;
using Cuentas.Api.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cuentas.Api.Repositorios
{
    public interface IRepositoryCuenta
    {
        Task<Response> Agregar(Cuenta cuenta);
        Task<Response> Actualizar(Cuenta cuenta);
        Task<Response> GetCuentas();
        Task<Response> GetCuentasPorId(string NumeroCuenta);
        Task<Response> GetCuentasPorCliente(int IdCliente);
        Task<Response> ExisteCuenta(string NumeroCuenta);
    }
}
