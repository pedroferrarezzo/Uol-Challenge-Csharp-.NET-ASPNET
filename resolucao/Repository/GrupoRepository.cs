using Desafio_UOL.Data;
using Desafio_UOL.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_UOL.Repository
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly DatabaseContext _context;
        public GrupoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<GrupoModel> GetAll() => _context.Grupos.ToList();

        public GrupoModel GetById(long id) => _context.Grupos.Find(id);

        public void Add(GrupoModel grupo)
        {
            _context.Grupos.Add(grupo);
            _context.SaveChanges();
        
        }
        public void Update(GrupoModel grupo)
        {
            _context.Update(grupo);
            _context.SaveChanges();
        }
        public void Delete(GrupoModel grupo)
        {
            _context.Grupos.Remove(grupo);
            _context.SaveChanges();
        }
    }
}
