using System.Xml.Serialization;

namespace Desafio_UOL.Models.XML
{
    [XmlRoot("liga_da_justica")]
    public class LigaDaJusticaModel
    {
        [XmlArray("codinomes")]
        [XmlArrayItem("codinome")]
        public List<CodinomeModel> Codinomes { get; set; }
    }
}
