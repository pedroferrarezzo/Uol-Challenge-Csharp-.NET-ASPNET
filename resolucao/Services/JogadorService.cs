using AutoMapper;
using Desafio_UOL.Exceptions;
using Desafio_UOL.Models;
using Desafio_UOL.Repository;
using Desafio_UOL.ViewModel;

namespace Desafio_UOL.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepository _repository;
        private readonly IXMLService _xmlService;
        private readonly IGrupoService _grupoService;
        private readonly ICodinomeService _codinomeService;
        

        public JogadorService(IJogadorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<JogadorModel> GetAll()
        {
            var jogadores = _repository.GetAll();
            
            return jogadores;
        }

        public IEnumerable<JogadorModel> GetAllByGroupId(long groupId)
        {
            var jogadores = _repository.GetAllByGroupId(groupId);

            return jogadores;
        }

        public JogadorModel GetById(long id) => _repository.GetById(id);

        public void Add(JogadorModel jogador) {

            _repository.Add(jogador);
        }
        public void Update(JogadorModel jogador) {
            _repository.Update(jogador);
        } 

        public void Delete(long id)
        {
            var jogador = _repository.GetById(id);
            if (jogador != null)
            {
                _repository.Delete(jogador);
            }
        }

    }
}
