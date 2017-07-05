using GetServiceApi.DTOs;
using GetServiceApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>
    {
        IQueryable<ContatoDto> consulta;

        public ContatoRepository()
        {
            consulta = (from s in Entity
                        select new ContatoDto()
                        {
                            UsuarioId = s.UsuarioId,
                            UsuarioUserName = s.Usuario.UserName,
                            UsuarioNomeCompleto = s.Usuario.NomeCompleto,
                            UsuarioStatus = s.Usuario.Status,
                            ContatoId = s.UsuarioContatoId,
                            ContatoUserName = s.UsuarioContato.UserName,
                            ContatoNomeCompleto = s.UsuarioContato.NomeCompleto,
                            ContatoStatus = s.UsuarioContato.Status,
                            Status = s.Status
                        });
        }

        public ContatoDto GetContato(string de, string para)
        {
            return consulta
                .Where(w => (w.UsuarioUserName == de && w.ContatoUserName == para) ||
                            (w.UsuarioUserName == para && w.ContatoUserName == de))
                .FirstOrDefault();
        }

        public IEnumerable<ContatoDto> GetContatos(string nome)
        {
            return consulta
                .Where(w => w.UsuarioUserName == nome || w.ContatoUserName== nome)
                .ToList();
        }
    }
}