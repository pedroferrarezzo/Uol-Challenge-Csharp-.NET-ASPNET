using Desafio_UOL.Models;
using Desafio_UOL.Repository;

namespace Desafio_UOL.Services
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _repository;

        public GrupoService(IGrupoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<GrupoModel> GetAll() => _repository.GetAll();
        public GrupoModel GetById(long id) => _repository.GetById(id);
        public void Add(GrupoModel grupo) => _repository.Add(grupo);
        public void Update(GrupoModel grupo) => _repository.Update(grupo);

        public void Delete(long id)
        {
            var grupo = _repository.GetById(id);
            if (grupo != null)
            {
                _repository.Delete(grupo);
            }
        }
    }
}
