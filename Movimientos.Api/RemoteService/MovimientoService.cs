using Microsoft.Extensions.Logging;
using Movimientos.Api.RemoteModel;
using Movimientos.Api.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movimientos.Api.RemoteService
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MovimientoService> _logger;
        public MovimientoService(IHttpClientFactory httpClientFactory, ILogger<MovimientoService> logger)
        {
            _httpClientFactory = httpClientFactory; 
            _logger = logger;   
        }

        public async Task<Response> ActualizarSaldoCuenta(CuentaRemote cuenta)
        {
            Response _response = new Response();
            try
            {
                var request = JsonConvert.SerializeObject(cuenta);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = _httpClientFactory.CreateClient("Cuenta");
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PutAsync("api/cuenta/Actualizar", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var obj = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = System.Text.Json.JsonSerializer.Deserialize<Response>(contenido, obj);
                    _response.IsSuccess = true;
                    _response.Data = resultado;
                    _response.Message = "Ok";
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Data = null;
                    _response.Message = "No Existe el Numero Cuenta Ingresado";
                    return _response;
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.IsSuccess = false;
                _response.Data = null;
                _response.Message = ex.Message.ToString();
                return _response;
            }
        }

        public async Task<Response> GetCuenta(string NumeroCuenta)
        {
            Response _response = new Response();
            try
            {
                var client = _httpClientFactory.CreateClient("Cuenta");
                var response = await client.GetAsync($"api/cuenta/GetCuentasPorId/{NumeroCuenta}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Response>(contenido.ToString());
                    var _data= JsonConvert.DeserializeObject<CuentaRemote>(resultado.Data.ToString()); 
                    _response.IsSuccess = true;
                    _response.Data = _data;
                    _response.Message = "Ok";
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Data = null;
                    _response.Message = "No Existe el Numero Cuenta Ingresado";
                    return _response;
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.IsSuccess = false;
                _response.Data = null;
                _response.Message = ex.Message.ToString();
                return _response;
            }
        }

        public async Task<Response> GetCuentasPorCliente(int IdCliente)
        {
            Response _response = new Response();
            try
            {
                var client = _httpClientFactory.CreateClient("Cuenta");
                var response = await client.GetAsync($"api/cuenta/GetCuentasPorCliente/{IdCliente}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Response>(contenido.ToString());
                    var _data = JsonConvert.DeserializeObject<List<ClienteCuentaRemote>>(resultado.Data.ToString());
                    _response.IsSuccess = true;
                    _response.Data = _data;
                    _response.Message = "Ok";
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Data = null;
                    _response.Message = "No Existe el Cliente Ingresado.";
                    return _response;
                }

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.IsSuccess = false;
                _response.Data = null;
                _response.Message = ex.Message.ToString();
                return _response;
            }
        }
    }
}
