using Desafio_UOL.Models;

namespace Desafio_UOL.Services
{
    public interface IXMLService
    {
        public Task<List<CodinomeModel>> ReadXML();
    }
}
