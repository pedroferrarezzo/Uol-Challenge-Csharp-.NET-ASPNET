using Desafio_UOL.Models;

namespace Desafio_UOL.Services
{
    public interface ICodinomeService
    {
        IEnumerable<CodinomeModel> GetAll();
        CodinomeModel GetById(long id);
        void Add(CodinomeModel codinome);
        void LoadCodenames();
        void Update(CodinomeModel codinome);
        void Delete(long id);
        CodinomeModel GetCodenameAvailable(long groupId);
    }
}
