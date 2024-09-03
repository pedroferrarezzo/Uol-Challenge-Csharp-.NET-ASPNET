using Desafio_UOL.Models;
using Desafio_UOL.Models.XML;
using System.Xml.Serialization;

namespace Desafio_UOL.Services
{
    public interface IJSONService
    {
        public Task<List<CodinomeModel>> ReadJSON();
    }
}
