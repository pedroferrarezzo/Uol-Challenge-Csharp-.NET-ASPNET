using Desafio_UOL.Models;

namespace Desafio_UOL.Repository
{
    public interface IGrupoRepository
    {
        IEnumerable<GrupoModel> GetAll();
        GrupoModel GetById(long id);
        void Add(GrupoModel grupo);
        void Update(GrupoModel grupo);
        void Delete(GrupoModel grupo);
    }
}
