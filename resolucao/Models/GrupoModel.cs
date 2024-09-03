namespace Desafio_UOL.Models
{
    public class GrupoModel
    {
        public long Id { get; set; } // Not null
        public string Nome { get; set; } // Not null
        public List<CodinomeModel> Codinomes { get; set; } // Relacionamento 1:N T_UOL_CODINOME
    }
}
