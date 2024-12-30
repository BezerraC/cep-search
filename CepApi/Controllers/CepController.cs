using CepApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace CepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly ConcurrentDictionary<string, (string data, DateTime timestamp)> _cepCache = new ConcurrentDictionary<string, (string, DateTime)>();

        /// <summary>
        /// Obtém os dados de um CEP, com cache para evitar requisições repetidas.
        /// </summary>
        /// <param name="cep">CEP a ser consultado</param>
        /// <returns>Dados do CEP em formato JSON</returns>
        [HttpGet("{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            // Verifica se o CEP já está no cache
            if (_cepCache.ContainsKey(cep) && _cepCache[cep].timestamp.AddMinutes(5) > DateTime.Now)
            {
                return Ok(DeserializeCepData(_cepCache[cep].data));
            }

            try
            {
                // URL da API ViaCEP
                var url = $"https://viacep.com.br/ws/{cep}/json/";

                // Obtém os dados da API externa
                var response = await _client.GetStringAsync(url);

                // Verifica se a resposta contém erro ou dados inválidos
                if (string.IsNullOrEmpty(response) || response.Contains("erro"))
                {
                    return NotFound(new { message = "CEP não encontrado ou inválido do back" });
                }

                // Armazena a resposta no cache
                _cepCache[cep] = (response, DateTime.Now);

                // Retorna os dados do CEP
                var data = DeserializeCepData(response);
                return Ok(data);
            }
            catch (HttpRequestException ex)
            {
                // Retorna erro se a requisição falhar
                return StatusCode(500, new { message = "Erro ao consultar a API do ViaCEP", error = ex.Message });
            }
        }

        /// <summary>
        /// Desserializa os dados do CEP da string JSON.
        /// </summary>
        /// <param name="json">A string JSON com os dados do CEP</param>
        /// <returns>Objeto CepResponse com os dados do CEP</returns>
        private CepResponse? DeserializeCepData(string json)
        {
            return JsonConvert.DeserializeObject<CepResponse>(json);
        }
    }
}
