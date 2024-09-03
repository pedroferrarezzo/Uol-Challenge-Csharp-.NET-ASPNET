using Desafio_UOL.Exceptions;
using Desafio_UOL.Models;
using Desafio_UOL.Repository;

namespace Desafio_UOL.Services
{
    public class CodinomeService : ICodinomeService
    {
        private readonly ICodinomeRepository _repository;
        private readonly IXMLService _xmlService;
        private readonly IGrupoService _grupoService;
        private readonly IJSONService _jsonService;

        public CodinomeService(ICodinomeRepository repository, IXMLService xmlService, IGrupoService grupoService, IJSONService jsonService)
        {
            _repository = repository;
            _xmlService = xmlService;
            _grupoService = grupoService;
            _jsonService = jsonService;
        }

        public IEnumerable<CodinomeModel> GetAll()
        {
            return _repository.GetAll();
        }
        public CodinomeModel GetById(long id) => _repository.GetById(id);
        public void Add(CodinomeModel codinome) => _repository.Add(codinome);

        public CodinomeModel GetCodenameAvailable(long groupId)
        {
            var codenomes = _repository.GetAllWhereGroup(groupId);
            foreach (var codename in codenomes)
            {
                if (codename.Jogador == null)
                {
                    return codename;
                }
                
            }
            throw new CodinomesEsgotadosException($"Nenhum codenome disponível para o grupo: {codenomes.FirstOrDefault().Grupo.Nome}");
        }

        public void LoadCodenames()
        {
            var codinomesLigaDaJustica = _xmlService.ReadXML().GetAwaiter().GetResult();
            var codinomesVingadores = _jsonService.ReadJSON().GetAwaiter().GetResult();

            var ligaDaJustica = new GrupoModel { Nome = "Liga da Justiça" };
            var vingadores = new GrupoModel { Nome = "Os Vingadores" };

            _grupoService.Add(vingadores);
            _grupoService.Add(ligaDaJustica);

            foreach (var cod in codinomesLigaDaJustica)
            {
                cod.GrupoId = ligaDaJustica.Id;
                _repository.Add(cod);
            }
            foreach (var cod in codinomesVingadores)
            {
                cod.GrupoId = vingadores.Id;
                _repository.Add(cod);
            }
        }
        public void Update(CodinomeModel codinome) => _repository.Update(codinome);

        public void Delete(long id)
        {
            var codinome = _repository.GetById(id);
            if (codinome != null)
            {
                _repository.Delete(codinome);
            }
        }
    }
}
