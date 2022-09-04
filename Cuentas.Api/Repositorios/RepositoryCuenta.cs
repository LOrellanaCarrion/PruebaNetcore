using Cuentas.Api.Context;
using Cuentas.Api.Model;
using Cuentas.Api.RemoteModel;
using Cuentas.Api.RemoteService;
using Cuentas.Api.Utils;
using Cuentas.Api.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cuentas.Api.Repositorios
{
    public class RepositoryCuenta : IRepositoryCuenta
    {
        private readonly ContextCuenta _contextCuenta;
        private readonly IClienteService _clienteservice;
        private Response _response;
        public RepositoryCuenta(ContextCuenta contextCuenta, IClienteService clienteservice)
        {
            _contextCuenta = contextCuenta;
            _clienteservice = clienteservice;
            this._response = new Response();
        }
        public async Task<Response> Actualizar(Cuenta cuenta)
        {
            try
            {
                var data = await _contextCuenta.Cuenta.FindAsync(cuenta.NumeroCuenta);

                Type t = typeof(Cuenta);
                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var item in propInfos)
                {
                    var fieldValue = item.GetValue(cuenta);
                    if (fieldValue != null)
                    {
                        item.SetValue(data, fieldValue);
                    }
                }
                _contextCuenta.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var resultado = await _contextCuenta.SaveChangesAsync();
                if (resultado > 0)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Ok";
                    _response.Data = data;
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

        public async Task<Response> ExisteCuenta(string NumeroCuenta)
        {
            try
            {
                var existe = await _contextCuenta.Cuenta.AnyAsync(p => p.NumeroCuenta == NumeroCuenta && p.Estado==true);
                if (existe)
                {
                    _response.IsSuccess = existe;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = existe;
                    _response.Message = "No Existe Cuenta a Modificar.";
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
        public async Task<Response> Agregar(Cuenta cuenta)
        {
            try
            {

                Random rdn = new Random();
                int a = rdn.Next(10000, 100000);
                cuenta.NumeroCuenta = a.ToString();
                _contextCuenta.Entry(cuenta).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var resultado = await _contextCuenta.SaveChangesAsync();
                if (resultado > 0)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Ok";
                    _response.Data = cuenta;
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
        public async Task<Response> GetCuentas()
        {
            try
            {
                var list = await _contextCuenta.Cuenta.ToListAsync();
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
        public async Task<Response> GetCuentasPorId(string NumeroCuenta)
        {
            try
            {
                var list = await _contextCuenta.Cuenta.FirstOrDefaultAsync(p => p.NumeroCuenta == NumeroCuenta);
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

        public async Task<Response> GetCuentasPorCliente(int IdCliente)
        {
            List<ClienteRemote> cliente = new List<ClienteRemote>();
            try
            {
                var _cliente = await _clienteservice.GetCliente(IdCliente);
                if (_cliente.IsSuccess)
                {
                    cliente.Add(_cliente.Data as ClienteRemote);
                }
                var list = await _contextCuenta.Cuenta.Where(p=>p.IdCliente==IdCliente).ToListAsync();
                var cuentaCliente = (from p in cliente
                              join e in list
                              on p.Id equals e.IdCliente
                              select new ClienteCuenta
                              {
                                  NumeroCuenta = e.NumeroCuenta,
                                  Tipo = e.TipoCuenta,
                                  SaldoInicial = e.SaldoInicial,
                                  Estado = e.Estado,
                                  Cliente = p.Nombre
                              }).ToList();
                if (cuentaCliente.Any())
                {
                    _response.IsSuccess = true;
                    _response.Data = cuentaCliente;
                    return _response;
                }
                _response.IsSuccess = false;
                _response.Message = "No Existe Cuenta para el Cliente Ingresado.";
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }

        public async Task<Response> Delete(string NumeroCuenta)
        {
            try
            {
                var data = await _contextCuenta.Cuenta.FindAsync(NumeroCuenta);
                data.Estado = false;
                _contextCuenta.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var resultado = await _contextCuenta.SaveChangesAsync();
                if (resultado > 0)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Ok";
                    _response.Data = data;
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
    }
}
