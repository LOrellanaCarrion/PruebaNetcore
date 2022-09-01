using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movimientos.Api.Model;
using Movimientos.Api.RemoteService;
using Movimientos.Api.Repositorios;
using Movimientos.Api.Utils;
using Movimientos.Api.ViewModel;
using System;
using System.Threading.Tasks;

namespace Movimientos.Api.Controllers
{
    [Route("api/movimiento")]
    [ApiController]
    public class MovimientoController : Controller
    {
        public IMapper Mapper { get; }
        private readonly IMovimientoService _movimientoservice;
        private readonly IRepositoryMovimiento _mediator;
        public MovimientoController(IRepositoryMovimiento repositoryMovimiento, IMovimientoService movimientoService, IMapper mapper)
        {
            _mediator = repositoryMovimiento;
            _movimientoservice = movimientoService;
            Mapper = mapper;
        }

        [HttpPost("Agregar")]
        public async Task<ActionResult<Response>> Agregar(MovimientoViewModel movimiento)
        {
           
            var _movimiento = Mapper.Map<Movimiento>(movimiento);
            var response = await _mediator.Agregar(_movimiento);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

        [HttpGet("GetMovientosPorClienteCuenta/{FechaInicial}/{FechaFinal}/{Idcliente}")]
        public async Task<ActionResult<Response>> GetMovientosPorClienteCuenta(DateTime FechaInicial, DateTime FechaFinal, int Idcliente)
        {
            var Cuenta = await _mediator.GetMovientosPorClienteCuenta(FechaInicial, FechaFinal, Idcliente);
            if (Cuenta.Data == null)
            {
                return NoContent();
            }
            return Ok(Cuenta);
        }
    }
}
