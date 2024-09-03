using Desafio_UOL.Exceptions;
using Desafio_UOL.Models;
using Desafio_UOL.Models.XML;
using System.Text.Json;

namespace Desafio_UOL.Services
{
    public class JSONService : IJSONService
    {

        private readonly HttpClient _client;


        public JSONService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CodinomeModel>> ReadJSON()
        {
            string url = "https://raw.githubusercontent.com/uolhost/test-backEnd-Java/master/referencias/vingadores.json";

            try
            {
                string jsonContent = await _client.GetStringAsync(url);
                VingadorModel jsonDeserialized = JsonSerializer.Deserialize<VingadorModel>(jsonContent);
                return jsonDeserialized.Codinomes;
            }
            catch(Exception ex) 
            {
                throw new JSONVingadoresSerializeException("Erro ao ler o arquivo vingadores.json");
            }
        }
        
    }
}
