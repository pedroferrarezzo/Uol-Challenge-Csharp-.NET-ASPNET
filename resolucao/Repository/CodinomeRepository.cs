using Desafio_UOL.Data;
using Desafio_UOL.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_UOL.Repository
{
    public class CodinomeRepository : ICodinomeRepository
    {
        private readonly DatabaseContext _context;
        public CodinomeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<CodinomeModel> GetAll()
        {
            return _context.Codinomes.Include(c => c.Grupo).Include(c => c.Jogador);
        }
        public IEnumerable<CodinomeModel> GetAllWhereGroup(long groupId)
        {
            return _context.Codinomes.Include(c => c.Grupo).Include(c => c.Jogador).Where(c => c.GrupoId == groupId);
        }

        public CodinomeModel GetById(long id) => _context.Codinomes.Find(id);

        public void Add(CodinomeModel codinome)
        {
            _context.Codinomes.Add(codinome);
            _context.SaveChanges();
        }
        public void Update(CodinomeModel codinome)
        {
            _context.Update(codinome);
            _context.SaveChanges();
        }
        public void Delete(CodinomeModel codinome)
        {
            _context.Codinomes.Remove(codinome);
            _context.SaveChanges();
        }
    }
}
