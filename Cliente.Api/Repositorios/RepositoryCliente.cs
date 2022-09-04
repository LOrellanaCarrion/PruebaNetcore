using Cliente.Api.Modelo;
using Cliente.Api.Persistencia;
using Cliente.Api.RemoteService;
using Cliente.Api.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Cliente.Api.Data
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private readonly ContextCliente contextCliente;
        private Response _response ;
        private readonly ICuentaService _cuentaService;
        public RepositoryCliente(ContextCliente contextCliente,ICuentaService cuentaService)
        {
            this.contextCliente = contextCliente;
            this._response = new Response();  
            _cuentaService=cuentaService;   
        }

        public async Task<Response> Actualizar(Modelo.Cliente cliente)
        {
            try
            {
                
                contextCliente.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var resultado= await contextCliente.SaveChangesAsync();
                if (resultado > 0)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Ok";
                    _response.Data = cliente;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Ocurrio un Problema Durante la Transacción.";
                    return _response;
                }
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Ocurrio un Problema Durante la Transacción.";
                return _response;
            }
        }

        public async Task<Response> Agregar(Modelo.Cliente cliente)
        {
            try
            {
                var id =await ObTenerMaximo();
                cliente.Id = id;
                contextCliente.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var resultado = await contextCliente.SaveChangesAsync();
                if (resultado>0)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Ok";
                    _response.Data = cliente;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Ocurrio un Problema Durante la Transacción.";
                    return _response;
                }
            }
            catch (System.Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = "Ocurrio un Problema Durante la Transacción.";
                return _response;
            }
        }

        public async Task<Response> Delete(int id)
        {
            using var transaction = contextCliente.Database.BeginTransaction();
            try
            {

                var cliente = await contextCliente.Cliente.FirstOrDefaultAsync(p => p.Id == id);
                cliente.Estado = false;
                contextCliente.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                var EliminarCuentaCliente = await _cuentaService.DeleteCuenta(id);
                await contextCliente.SaveChangesAsync();
                if (EliminarCuentaCliente.IsSuccess)
                {
                    await transaction.CommitAsync();
                    _response.IsSuccess = true;
                    _response.Message = "Ok";
                    _response.Data = cliente;
                    return _response;
                }
                else
                {
                    await transaction.RollbackAsync();
                    _response.IsSuccess = false;
                    _response.Message = "Ocurrio un Problema Durante la Transacción.";
                    return _response;
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                _response.IsSuccess = false;
                _response.Message = "Ocurrio un Problema Durante la Transacción.";
                return _response;
            }


        }

        public async Task<Response>ExisteCliente(int id)
        {
            try
            {
                var existe = await contextCliente.Cliente.AnyAsync(p => p.Id == id);
                if (existe)
                {
                    _response.IsSuccess = existe;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = existe;
                    _response.Message = "No Existe Cliente a Modificar.";
                    return _response;
                }
            }
            catch (Exception)
            {
                _response.IsSuccess = false;
                _response.Message = "Ocurrio un Problema Durante la Transacción.";
                return _response;
            }
 
        }

        public async Task<Response> GetClienteById(int id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions()
                        {
                            IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                        },
                        TransactionScopeAsyncFlowOption.Enabled))
                {
                    var list = await contextCliente.Cliente.FirstOrDefaultAsync(P => P.Id == id);
                    scope.Complete();
                    _response.IsSuccess = true;
                    _response.Data = list;
                    return _response;
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.Message = ex.Message;
                return _response;
            }
        }

        public async Task<Response> GetClientes()
        {
            try
            {
                var list = await  contextCliente.Cliente.ToListAsync();
                _response.IsSuccess = true;
                _response.Data = list;
                return _response;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.Message = ex.Message;
                return _response;
            }
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
