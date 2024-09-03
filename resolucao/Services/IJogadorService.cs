using Desafio_UOL.Models;
using Desafio_UOL.ViewModel;

namespace Desafio_UOL.Services
{
    public interface IJogadorService
    {
        IEnumerable<JogadorModel> GetAll();
        JogadorModel GetById(long id);
        void Add(JogadorModel jogador);
        void Update(JogadorModel jogador);
        void Delete(long id);
        IEnumerable<JogadorModel> GetAllByGroupId(long groupId);
    }
}
