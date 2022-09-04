using AutoMapper;
using Cliente.Api.Data;
using Cliente.Api.Modelo;
using Cliente.Api.Persistencia;
using Cliente.Api.Utils;
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
        public async Task<ActionResult<Response>> Agregar(ClienteCreateViewModel cliente)
        {
           
                var _cliente=Mapper.Map<Cliente.Api.Modelo.Cliente>(cliente);
                var response = await _mediator.Agregar(_cliente);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            

        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult<Response>> Actualizar(ClienteViewModel cliente)
        {
            var existeCliente = await _mediator.ExisteCliente(cliente.Id);
                if (existeCliente.IsSuccess)
                {
                    return NotFound(existeCliente);
                }
                var _cliente = Mapper.Map<Cliente.Api.Modelo.Cliente>(cliente);
                var response = await _mediator.Actualizar(_cliente);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                return BadRequest(response);
        
        }
        [HttpGet]
        public async Task<ActionResult<Response>> Get()
        {
            var response= await _mediator.GetClientes();
            if (response.Data == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpGet("GetClienteById/{IdCliente}")]
        public async Task<ActionResult<Response>> GetClienteById(int IdCliente)
        {
            var response = await _mediator.GetClienteById(IdCliente);
            if (response.Data==null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpDelete("DeleteCliente/{IdCliente}")]
        public async Task<ActionResult<Response>> DeleteCliente(int IdCliente)
        {
            var existeCliente = await _mediator.ExisteCliente(IdCliente);
            if (!existeCliente.IsSuccess)
            {
                return NotFound(existeCliente);
            }
            var response = await _mediator.Delete(IdCliente);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


    }
}
