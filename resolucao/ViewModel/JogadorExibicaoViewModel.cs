using Desafio_UOL.Models;
using System.ComponentModel.DataAnnotations;

namespace Desafio_UOL.ViewModel
{
    public class JogadorExibicaoViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public CodinomeModel Codinome { get; set; }
    }
}
