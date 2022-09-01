using Cuentas.Api.RemoteModel;
using Cuentas.Api.Utils;
using System.Threading.Tasks;

namespace Cuentas.Api.RemoteService
{
    public interface IClienteService
    {
        Task<Response> GetCliente(int IdCliente);
    }
}
