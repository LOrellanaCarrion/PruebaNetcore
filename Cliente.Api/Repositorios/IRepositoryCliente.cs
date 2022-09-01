using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cliente.Api.Data
{
    public interface IRepositoryCliente
    {
        Task<int> Agregar(Cliente.Api.Modelo.Cliente cliente);
        Task<int> Actualizar(Cliente.Api.Modelo.Cliente cliente);
         Task<List<Modelo.Cliente>> GetClientes();
        Task<int> ObTenerMaximo();
        Task<Modelo.Cliente> GetClienteById(int id);
        Task<bool> ExisteCliente(int id);
    }
}
