using Cuentas.Api.RemoteModel;
using Cuentas.Api.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cuentas.Api.RemoteService
{
    public class ClienteService : IClienteService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ClienteService> _logger;    
        public ClienteService(IHttpClientFactory httpClientFactory, ILogger<ClienteService> logger )
        {
            _httpClientFactory = httpClientFactory; 
            _logger = logger;   
        }
        public async Task<Response> GetCliente(int IdCliente)
        {
            Response _response = new Response();
            try
            {
                
                var client = _httpClientFactory.CreateClient("Cliente");
               var response= await client.GetAsync($"api/cliente/GetClienteById/{IdCliente}");
                if (response.StatusCode== HttpStatusCode.OK)
                {
                    var contenido= await response.Content.ReadAsStringAsync();
                    //var obj= new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado  = JsonConvert.DeserializeObject<ClienteRemote>(contenido.ToString());
                    _response.IsSuccess = true;
                    _response.Data=resultado;
                    _response.Message = "Ok";
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Data = null;
                    _response.Message = response.ReasonPhrase;
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
