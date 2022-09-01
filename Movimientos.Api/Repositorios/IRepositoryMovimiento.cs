using Movimientos.Api.Model;
using Movimientos.Api.Utils;
using System;
using System.Threading.Tasks;

namespace Movimientos.Api.Repositorios
{
    public interface IRepositoryMovimiento
    {
        Task<Response> Agregar(Movimiento movimiento);
        Task<Response> GetMovientosPorClienteCuenta(DateTime FechaInicial, DateTime FechaFinal, int Idcliente);
    }
}
