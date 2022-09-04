
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



        [Fact]
        public async void GetClientesAsync()
        {
       
            ///Obtenemos todos los clientes
            var options = new DbContextOptionsBuilder<ContextCliente>()
                .UseInMemoryDatabase(databaseName: "BaseDatosCliente")
                .Options;

           var  contexto = new ContextCliente(options);

            RepositoryCliente repositoryCliente = new RepositoryCliente(contexto, null);
            var response = await repositoryCliente.GetClientes();
            Assert.True(response.IsSuccess);


        }

        [Fact]
        public async void AgregarCliente()
        {
            System.Diagnostics.Debugger.Launch();
            var options = new DbContextOptionsBuilder<ContextCliente>()
                .UseInMemoryDatabase(databaseName: "BaseDatosCliente")
                .Options;

            var contexto = new ContextCliente(options);

            RepositoryCliente repositoryCliente = new RepositoryCliente(contexto, null);
            var cliente = Cliente();
            var response = await repositoryCliente.Agregar(cliente);
            Assert.True(response.IsSuccess);

        }
    }
}
