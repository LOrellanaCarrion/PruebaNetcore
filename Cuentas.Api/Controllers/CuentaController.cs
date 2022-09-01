using AutoMapper;
using Cuentas.Api.Model;
using Cuentas.Api.RemoteModel;
using Cuentas.Api.RemoteService;
using Cuentas.Api.Repositorios;
using Cuentas.Api.Utils;
using Cuentas.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cuentas.Api.Controllers
{
    [Route("api/cuenta")]
    [ApiController]
    public class CuentaController : Controller
    {
        public IMapper Mapper { get; }
        private readonly IRepositoryCuenta _mediator;
        private readonly IClienteService _clienteservice;
        public CuentaController(IRepositoryCuenta repositoryCuenta, IMapper mapper, IClienteService clienteService)
        {
            _mediator = repositoryCuenta;
            Mapper = mapper;
            _clienteservice = clienteService;
        }
        [HttpPost("Agregar")]
        public async Task<ActionResult<Response>> Agregar(CuentaCreateViewModel cuenta)
        {


            var ExisteCliente = await _clienteservice.GetCliente(cuenta.IdCliente);
            if (!ExisteCliente.IsSuccess)
            {
                return NotFound(ExisteCliente);
            }
            var _cuenta = Mapper.Map<Cuenta>(cuenta);
            var response = await _mediator.Agregar(_cuenta);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult<Response>> Actualizar(CuentaUpdateViewModel cuenta)
        {

            var existeCuenta = await _mediator.ExisteCuenta(cuenta.NumeroCuenta);
            if (!existeCuenta.IsSuccess)
            {
                return NotFound(existeCuenta);
            }
            var ExisteCliente = await _clienteservice.GetCliente(cuenta.IdCliente);
            if (!ExisteCliente.IsSuccess)
            {
                return NotFound(ExisteCliente);
            }
            var _cliente = Mapper.Map<Cuenta>(cuenta);
            var response = await _mediator.Actualizar(_cliente);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }
        [HttpGet("GetCuentasPorId/{NumeroCuenta}")]
        public async Task<ActionResult<Response>> GetCuentasPorId(string NumeroCuenta)
        {
            var CuentaId = await _mediator.GetCuentasPorId(NumeroCuenta);
            if (CuentaId.Data ==null)
            {
                return NoContent();
            }
            return Ok(CuentaId);
        }

        [HttpGet("GetCuentasPorCliente/{IdCliente}")]
        public async Task<ActionResult<Response>> GetCuentasPorCliente(int IdCliente)
        {
            var Cuenta = await _mediator.GetCuentasPorCliente(IdCliente);
            if (Cuenta.Data == null)
            {
                return NoContent();
            }
            return Ok(Cuenta);
        }
    }
}
