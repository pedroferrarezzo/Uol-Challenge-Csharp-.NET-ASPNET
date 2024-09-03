using Desafio_UOL.Models;

namespace Desafio_UOL.Repository
{
    public interface ICodinomeRepository
    {
        IEnumerable<CodinomeModel> GetAll();
        CodinomeModel GetById(long id);
        void Add(CodinomeModel codinome);
        void Update(CodinomeModel codinome);
        void Delete(CodinomeModel codinome);
        IEnumerable<CodinomeModel> GetAllWhereGroup(long groupId);
    }
}
