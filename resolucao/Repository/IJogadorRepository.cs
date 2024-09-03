using Desafio_UOL.Models;

namespace Desafio_UOL.Repository
{
    public interface IJogadorRepository
    {
        IEnumerable<JogadorModel> GetAll();
        JogadorModel GetById(long id);
        void Add(JogadorModel jogador);
        void Update(JogadorModel jogador);
        void Delete(JogadorModel jogador);
        IEnumerable<JogadorModel> GetAllByGroupId(long groupId);
    }
}
