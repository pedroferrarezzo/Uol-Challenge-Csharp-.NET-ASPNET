using Desafio_UOL.Data;
using Desafio_UOL.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_UOL.Repository
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly DatabaseContext _context;
        public JogadorRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<JogadorModel> GetAll() => _context.Jogadores.Include(j => j.Codinome).ThenInclude(c => c.Grupo);

        public IEnumerable<JogadorModel> GetAllByGroupId(long groupId) => _context.Jogadores.
            Include(j => j.Codinome).
            ThenInclude(c => c.Grupo).Where(j => j.Codinome.Grupo.Id == groupId);


        public JogadorModel GetById(long id) => _context.Jogadores.Find(id);

        public void Add(JogadorModel jogador)
        {
            _context.Jogadores.Add(jogador);
            _context.SaveChanges();
        }
        public void Update(JogadorModel jogador)
        {
            _context.Update(jogador);
            _context.SaveChanges();
        }
        public void Delete(JogadorModel jogador)
        {
            _context.Jogadores.Remove(jogador);
            _context.SaveChanges();
        }
    }
}
