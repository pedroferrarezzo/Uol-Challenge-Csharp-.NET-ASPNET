using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Desafio_UOL.Models.XML
{
    public class VingadorModel
    {
        [JsonPropertyName("vingadores")]
        public List<CodinomeModel> Codinomes { get; set; }
    }
}
