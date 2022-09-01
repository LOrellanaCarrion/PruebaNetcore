using AutoMapper;
using Cliente.Api.Data;
using Cliente.Api.Modelo;
using Cliente.Api.Persistencia;
using Cliente.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cliente.Api.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IRepositoryCliente _mediator;

        public IMapper Mapper { get; }

        public ClienteController(IRepositoryCliente mediator,IMapper mapper)
        {
            _mediator = mediator;
            Mapper = mapper;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<int>> Agregar(ClienteCreateViewModel cliente)
        {
            try
            {
                var _cliente=Mapper.Map<Cliente.Api.Modelo.Cliente>(cliente);
                var response = await _mediator.Agregar(_cliente);
                if (response > 0)
                {
                    return Ok();
                }
                return BadRequest("Ocurrio un Problema Durante la Transacción");
            }
            catch (System.Exception) 
            {

                return BadRequest("Ocurrio un Problema Durante la Transacción");
            }
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult<int>> Actualizar(ClienteViewModel cliente)
        {
            try
            {
                if(!await _mediator.ExisteCliente(cliente.Id))
                {
                    return NotFound("No Existe Cliente a Modificar");
                }
                var _cliente = Mapper.Map<Cliente.Api.Modelo.Cliente>(cliente);
                var response = await _mediator.Actualizar(_cliente);
                if (response > 0)
                {
                    return Ok();
                }
                return BadRequest("Ocurrio un Problema Durante la Transacción");
            }
            catch (System.Exception)
            {

                return BadRequest("Ocurrio un Problema Durante la Transacción");
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<ClienteViewModel>>> Get()
        {
            var response= await _mediator.GetClientes();

            return Mapper.Map<List<ClienteViewModel>>(response);
        }

        [HttpGet("GetClienteById/{IdCliente}")]
        public async Task<ActionResult<ClienteViewModel>> GetClienteById(int IdCliente)
        {
            var response = await _mediator.GetClienteById(IdCliente);

            return Mapper.Map<ClienteViewModel>(response);
        }


}
}
