using GetServiceApi.DTOs;
using GetServiceApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class MensagemRepository : BaseRepository<Mensagem>
    {
        public IEnumerable<MensagemDto> GetMensagens(string usuario, string contato)
        {
            return (from s in Entity
                    where (s.Remetente.UserName == usuario && s.Destinatario.UserName == contato) ||
                          (s.Remetente.UserName == contato && s.Destinatario.UserName == usuario)
                    orderby s.Data
                    select new MensagemDto()
                    {
                        remetenteUserName = s.Remetente.UserName,
                        DestinatarioUserName = s.Destinatario.UserName,
                        Texto = s.Texto,
                        Data = s.Data
                    }).ToList();
        }
    }
}