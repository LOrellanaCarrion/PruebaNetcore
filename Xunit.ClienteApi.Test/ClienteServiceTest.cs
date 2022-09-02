
using Cliente.Api.Data;
using Cliente.Api.Persistencia;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xunit.ClienteApi.Test
{
    public class ClienteServiceTest
    {

        private IEnumerable<Cliente.Api.Modelo.Cliente> ObtenerDatosPrueba()
        {
            List<Cliente.Api.Modelo.Cliente> lst = new List<Cliente.Api.Modelo.Cliente>();
            Cliente.Api.Modelo.Cliente model = new Cliente.Api.Modelo.Cliente();
            model.Id = 1;
            model.Nombre = "Luis Orellana";
            model.Estado = true;
            model.Edad = "28";
            model.Contrasena = "1234";
            model.Genero = "Masculino";
            lst.Add(model);
            model = new Cliente.Api.Modelo.Cliente();
            model.Id = 2;
            model.Nombre = "Manuel Orellana";
            model.Estado = true;
            model.Edad = "29";
            model.Contrasena = "34535";
            model.Genero = "Masculino";
            lst.Add(model);
            return lst;

        }

        private Cliente.Api.Modelo.Cliente Cliente()
        {
            Cliente.Api.Modelo.Cliente model = new Cliente.Api.Modelo.Cliente();
            model.Nombre = "Luis Orellana";
            model.Estado = true;
            model.Edad = "28";
            model.Contrasena = "1234";
            model.Genero = "Masculino";
            return model;

        }
        private Mock<ContextCliente> CrearContexto()
        {
            var data = ObtenerDatosPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<Cliente.Api.Modelo.Cliente>>();
            dbSet.As<IQueryable<Cliente.Api.Modelo.Cliente>>().Setup(p => p.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<Cliente.Api.Modelo.Cliente>>().Setup(p => p.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Cliente.Api.Modelo.Cliente>>().Setup(p => p.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Cliente.Api.Modelo.Cliente>>().Setup(p => p.GetEnumerator()).Returns(data.GetEnumerator());

            dbSet.As<IAsyncEnumerable<Cliente.Api.Modelo.Cliente>>().Setup(p => p.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<Cliente.Api.Modelo.Cliente>(data.GetEnumerator()));
            //return dbSet;
            var contexto = new Mock<ContextCliente>();
            contexto.Setup(p => p.Cliente).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async void GetClientesAsync()
        {
            System.Diagnostics.Debugger.Launch();
            var mockContexto = CrearContexto();
            RepositoryCliente repositoryCliente = new RepositoryCliente(mockContexto.Object);
            var response = await repositoryCliente.GetClientes();

            Assert.True(response.Any());

            //var cliente = Cliente();
            //var agregar = await repositoryCliente.Agregar(cliente);

            //Assert.True(agregar>0?true:false);
        }
    }
}
