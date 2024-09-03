using Desafio_UOL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Desafio_UOL.ViewModel
{
    public class JogadorUpdateViewModel
    {
        [Required(ErrorMessage = "O Id do jogador é obrigatório.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome do jogador é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email do jogador obrigatório.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Insira um endereço de email válido.")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [RegularExpression(pattern: "^\\d{11}$", ErrorMessage = "Insira um telefone no formato: 119xxxxxxxx")]
        public string? Telefone { get; set; }

        [Display(Name = "Grupo")]
        [Required(ErrorMessage = "A seleção de um grupo para o Jogador é obrigatório.")]
        public long GrupoId { get; set; }

        public long GroupIdAtual { get; set; }
        public SelectList Grupos { get; set; }

        public JogadorUpdateViewModel()
        {
            {
                Grupos = new SelectList(Enumerable.Empty<SelectListItem>());
                GroupIdAtual = 0;
            }
        }
    }
}
