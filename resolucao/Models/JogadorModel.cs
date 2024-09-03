namespace Desafio_UOL.Models
{
    public class JogadorModel
    {
        public long Id {  get; set; } // Not null
        public string Nome { get; set; } // Not null
        public string Email { get; set; } // Not null
        public string Telefone { get; set; } // Null
        public long CodinomeId { get; set; } // Relacionamento 1:1 T_UOL_CODINOME | Not null FK | Unique
        public CodinomeModel Codinome { get; set; } // Not null
    }
}
