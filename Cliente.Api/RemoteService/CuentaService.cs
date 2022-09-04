using Cliente.Api.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace Cliente.Api.RemoteService
{
    public class CuentaService : ICuentaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Response _response;
        public CuentaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _response = new Response(); 
        }
        public async Task<Response> DeleteCuenta(int IdCliente)
        {
            string contenido = string.Empty;
            try
            {
                var client = _httpClientFactory.CreateClient("Cuenta");
                var response = await client.DeleteAsync($"api/cuenta/DeleteCuentaPoCliente/{IdCliente}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    contenido = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<Response>(contenido.ToString());
                    return _response;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    contenido = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<Response>(contenido.ToString());
                    return _response;
                }
                else
                {
                    contenido = await response.Content.ReadAsStringAsync();
                    _response = JsonConvert.DeserializeObject<Response>(contenido.ToString());
                    return _response;

                }
            }
            catch (System.Exception)
            {
                _response.IsSuccess = false;
                _response.Message = "Ocurrio un Error Durante la Transaccion.";
                return _response;
            }
           
           }
    }
}
