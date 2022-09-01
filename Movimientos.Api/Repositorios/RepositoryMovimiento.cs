using Microsoft.EntityFrameworkCore;
using Movimientos.Api.Context;
using Movimientos.Api.Model;
using Movimientos.Api.RemoteModel;
using Movimientos.Api.RemoteService;
using Movimientos.Api.Utils;
using Movimientos.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movimientos.Api.Repositorios
{
    public class RepositoryMovimiento : IRepositoryMovimiento
    {
        private readonly ContextMovimiento _contextMovimiento;
        private readonly IMovimientoService _repositoryMovimiento;
        public RepositoryMovimiento(ContextMovimiento contextMovimiento, IMovimientoService repositoryMovimiento)
        {
            _contextMovimiento = contextMovimiento;
            _repositoryMovimiento = repositoryMovimiento;
        }
        public async Task<Response> Agregar(Movimiento movimiento)
        {
            Response _response = new Response();
            using var transaction = _contextMovimiento.Database.BeginTransaction();
            decimal saldoDisponible = 0;
            try
            {
                if (movimiento.TipoMovimiento.ToUpper() != "CRE" && movimiento.TipoMovimiento.ToUpper() != "DEB")
                {
                    _response.IsSuccess = false;
                    _response.Message = "Los Tipos de Movimientos solo Pueden ser CRE (credito) O DEB (debito)";
                    return _response;
                }
                    var _existeNumeroCuenta = await _repositoryMovimiento.GetCuenta(movimiento.NumeroCuentaId);
                if (!_existeNumeroCuenta.IsSuccess)
                {
                    return _existeNumeroCuenta;
                }
                var _cuentaRemote = _existeNumeroCuenta.Data as CuentaRemote;
                
                if (await _contextMovimiento.Movimiento.AnyAsync())
                {
                    var _IdMovimiento = await _contextMovimiento.Movimiento.Where(p => p.NumeroCuentaId == movimiento.NumeroCuentaId).MaxAsync(p => p.IdMovimiento);
                    var _saldoDisponible = await _contextMovimiento.Movimiento.FirstOrDefaultAsync(p => p.IdMovimiento == _IdMovimiento);
                    if (_saldoDisponible.Saldo <= 0 && movimiento.TipoMovimiento.ToUpper()== "DEB")
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Saldo no disponible";
                        return _response;
                    }
                    else
                    {
                        saldoDisponible = _saldoDisponible.Saldo;
                    }
                }
                else
                {
                    saldoDisponible = _cuentaRemote.SaldoInicial;
                }
                if (movimiento.TipoMovimiento.ToUpper() == "CRE")
                {
                    saldoDisponible = saldoDisponible + movimiento.Valor;
                }
                else
                {
                    if (movimiento.Valor > saldoDisponible)
                    {
                        _response.IsSuccess = false;
                        _response.Message = "Fondos Insuficientes para Realizar la transaccion.";
                        return _response;
                    }
                    saldoDisponible = saldoDisponible - movimiento.Valor;
                }
                movimiento.Saldo = saldoDisponible;
                movimiento.FechaMovimiento = DateTime.Now;
                _contextMovimiento.Entry(movimiento).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await _contextMovimiento.SaveChangesAsync();

                await transaction.CommitAsync();
                _response.IsSuccess = true;
                _response.Message = "Ok";
                return _response;

                //var _actualizaSaldoCuenta = await _repositoryMovimiento.ActualizarSaldoCuenta(_cuentaRemote);
                //if (_actualizaSaldoCuenta.IsSuccess)
                //{
                //    await transaction.CommitAsync();
                //    _response.IsSuccess = true;
                //    _response.Message = "Ok";
                //    return _response;
                //}
                //else
                //{

                //}

            }
            catch (System.Exception ex)
            {
                await transaction.RollbackAsync();
                _response.IsSuccess = false;
                _response.Message = "Ocurrio un Problema Durante la Transacción.";
                return _response;
            }
        }

        public async Task<Response> GetMovientosPorClienteCuenta(DateTime FechaInicial, DateTime FechaFinal, int Idcliente)
        {
            //p => p.FecIng >= settings.ExtraData.FechaDesde && p.FecIng <= settings.ExtraData.FechaHasta
            Response _response = new Response();
            try
            {
                var _cliente = await _repositoryMovimiento.GetCuentasPorCliente(Idcliente);
                var _cuentaCliente = _cliente.Data as List<ClienteCuentaRemote>;
                var list = await _contextMovimiento.Movimiento.ToListAsync();
                var cuentaCliente = (from p in _cuentaCliente
                                     join e in list
                                     on p.NumeroCuenta equals e.NumeroCuentaId
                                     where e.FechaMovimiento.Date >= FechaInicial.Date && e.FechaMovimiento.Date <= FechaFinal.Date 
                                     select new MovimientoClienteCuenta
                                     {
                                         NumeroCuenta = e.NumeroCuentaId,
                                         Tipo = p.Tipo,
                                         SaldoInicial = p.SaldoInicial,
                                         Estado = p.Estado,
                                         Cliente = p.Cliente,
                                         Fecha=e.FechaMovimiento.ToShortDateString(),
                                         Movimiento=e.TipoMovimiento=="DEB"  ? - e.Valor :e.Valor,
                                         SaldoDisponible=e.Saldo
                                     }).ToList();
                _response.IsSuccess = true;
                _response.Data = cuentaCliente;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return _response;
            }
        }
    }
}
