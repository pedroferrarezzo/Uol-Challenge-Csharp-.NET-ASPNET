using Desafio_UOL.Exceptions;
using Desafio_UOL.Models;
using Desafio_UOL.Models.XML;
using System.Xml.Serialization;

namespace Desafio_UOL.Services
{
    public class XMLService : IXMLService
    {

        private readonly HttpClient _client;


        public XMLService(HttpClient client)
        {
            _client = client;
        }


        public async Task<List<CodinomeModel>> ReadXML()
        {
            string url = "https://raw.githubusercontent.com/uolhost/test-backEnd-Java/master/referencias/liga_da_justica.xml";

            try
            {
                string xmlContent = await _client.GetStringAsync(url);
                XmlSerializer serializer = new XmlSerializer(typeof(LigaDaJusticaModel));
                List<CodinomeModel> codinomesList = new List<CodinomeModel>();

                using (StringReader reader = new StringReader(xmlContent))
                {
                    LigaDaJusticaModel codinomes = (LigaDaJusticaModel)serializer.Deserialize(reader);

                    foreach (CodinomeModel codinome in codinomes.Codinomes)
                    {

                        codinomesList.Add(codinome);
                    }

                }
                return codinomesList;
            }
            catch(Exception ex) 
            {
                throw new XMLLigaDaJusticaSerializeException("Erro ao ler o arquivo liga_da_justica.xml");
            }
        }
        
    }
}
