using Cliente.Api.Utils;
using System.Threading.Tasks;

namespace Cliente.Api.RemoteService
{
    public interface ICuentaService
    {
        Task<Response> DeleteCuenta(int IdCliente);
    }
}
