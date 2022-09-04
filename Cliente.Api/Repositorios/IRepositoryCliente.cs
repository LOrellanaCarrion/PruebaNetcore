using Cliente.Api.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cliente.Api.Data
{
    public interface IRepositoryCliente
    {
        Task<Response> Agregar(Cliente.Api.Modelo.Cliente cliente);
        Task<Response> Actualizar(Cliente.Api.Modelo.Cliente cliente);
         Task<Response> GetClientes();
        Task<int> ObTenerMaximo();
        Task<Response> GetClienteById(int id);
        Task<Response> Delete(int id);
        Task<Response> ExisteCliente(int id);
    }
}
