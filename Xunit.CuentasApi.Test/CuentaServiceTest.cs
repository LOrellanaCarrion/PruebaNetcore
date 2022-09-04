using Cuentas.Api.Context;
using Cuentas.Api.Model;
using Cuentas.Api.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Xunit.CuentasApi.Test
{
    public class CuentaServiceTest
    {
        [Fact]
        public async Task GetCuentasAsync()
        {
           
            var options = new DbContextOptionsBuilder<ContextCuenta>()
                .UseInMemoryDatabase(databaseName: "CuentaBD")
                .Options;

            var contexto = new ContextCuenta(options);

            RepositoryCuenta repositoryCuenta = new RepositoryCuenta(contexto, null);
            var response = await repositoryCuenta.GetCuentas();
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task AgregarCuenta()
        {
            System.Diagnostics.Debugger.Launch();
            var options = new DbContextOptionsBuilder<ContextCuenta>()
                .UseInMemoryDatabase(databaseName: "CuentaBD")
                .Options;

            var contexto = new ContextCuenta(options);

            RepositoryCuenta repositoryCuenta = new RepositoryCuenta(contexto, null);
            Cuenta cuenta = new Cuenta();
            cuenta.SaldoInicial = 200;
            cuenta.IdCliente = 1;
            cuenta.TipoCuenta = "Ahorro";
            cuenta.Estado = true;
            var response = await repositoryCuenta.Agregar(cuenta);
            Assert.True(response.IsSuccess);
        }
    }
}
