using Microsoft.EntityFrameworkCore;
using Movimientos.Api.Context;
using Movimientos.Api.Model;
using Movimientos.Api.RemoteService;
using Movimientos.Api.Repositorios;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Xunit.MovimientoApi.Test
{
    public class MovimientoServiceTest
    {
        [Fact]
        public async Task GetMovimientosPosFechaClienteAsync()
        {
           // System.Diagnostics.Debugger.Launch();
            var options = new DbContextOptionsBuilder<ContextMovimiento>()
              .UseInMemoryDatabase(databaseName: "MovimientoDB")
              .Options;

            var contexto = new ContextMovimiento(options);
            RepositoryMovimiento repositoryMovimiento = new RepositoryMovimiento(contexto, null);
            var response = await repositoryMovimiento.GetMovientosPorClienteCuenta(DateTime.Now, DateTime.Now, 1);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task AgregarMovimiento()
        {
            System.Diagnostics.Debugger.Launch();
            var options = new DbContextOptionsBuilder<ContextMovimiento>()
              .UseInMemoryDatabase(databaseName: "MovimientoDB")
              .Options;

            var contexto = new ContextMovimiento(options);
            RepositoryMovimiento repositoryMovimiento = new RepositoryMovimiento(contexto, null);
            Movimiento movimiento = new Movimiento();
            movimiento.TipoMovimiento = "DEB";
            movimiento.FechaMovimiento = DateTime.Now;
            movimiento.NumeroCuentaId = "122323";
            movimiento.Saldo = 300;
            var response = await repositoryMovimiento.Agregar(movimiento);
            Assert.True(response.IsSuccess);
        }
    }
}
