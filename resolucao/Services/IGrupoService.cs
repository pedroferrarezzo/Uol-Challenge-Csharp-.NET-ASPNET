using Desafio_UOL.Models;

namespace Desafio_UOL.Services
{
    public interface IGrupoService
    {
        IEnumerable<GrupoModel> GetAll();
        GrupoModel GetById(long id);
        void Add(GrupoModel grupo);
        void Update(GrupoModel grupo);
        void Delete(long id);
    }
}
