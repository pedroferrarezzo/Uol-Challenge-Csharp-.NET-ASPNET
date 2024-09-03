using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Desafio_UOL.Models
{
    public class CodinomeModel
    {
        public long Id { get; set; }  // Not null
        [XmlText]
        [JsonPropertyName("codinome")]
        public string Nome { get; set; } // Not null
        public JogadorModel Jogador { get; set; } // Relacionamento 1:1 T_UOL_JOGADOR
        public long GrupoId { get; set; } // Relacionamento 1:N T_UOL_GRUPO | Not null FK
        public GrupoModel Grupo { get; set; } // Relacionamento 1:N T_UOL_GRUPO
    }
}
