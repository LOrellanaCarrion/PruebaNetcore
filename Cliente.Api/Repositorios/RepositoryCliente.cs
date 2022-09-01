using Cliente.Api.Modelo;
using Cliente.Api.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.Api.Data
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private readonly ContextCliente contextCliente;
        public RepositoryCliente(ContextCliente contextCliente)
        {
            this.contextCliente = contextCliente;
        }

        public async Task<int> Actualizar(Modelo.Cliente cliente)
        {
            try
            {
                
                contextCliente.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return await contextCliente.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> Agregar(Modelo.Cliente cliente)
        {
            try
            {
                var id =await ObTenerMaximo();
                cliente.Id = id;
                contextCliente.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                return await contextCliente.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool>ExisteCliente(int id)
        {
            var existe = await contextCliente.Cliente.AnyAsync(p => p.Id == id);
            return existe;
        }

        public async Task<Modelo.Cliente> GetClienteById(int id)
        {
            return await contextCliente.Cliente.Where(P => P.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Modelo.Cliente>> GetClientes()
        {
            return await contextCliente.Cliente.ToListAsync();
        }

        public async Task<int> ObTenerMaximo()
        {
            var valor = await contextCliente.Cliente.AnyAsync();
            if (!valor)
            {
                return 1;
            }
            return await contextCliente.Cliente.MaxAsync(p => p.Id) + 1;
        }
    }
}
