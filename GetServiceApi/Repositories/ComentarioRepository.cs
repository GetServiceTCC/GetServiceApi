using GetServiceApi.DTOs;
using GetServiceApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class ComentarioRepository : BaseRepository<Comentario>
    {
        public IEnumerable<ComentarioDto> GetComentarioPorServico(int servicoId)
        {
            return (from s in Entity
                    where s.ServicoId == servicoId
                    orderby s.Data descending
                    select new ComentarioDto
                    {
                        Id = s.Id,
                        Descricao = s.Descricao,
                        UserName = s.Profissional.UserName,
                        NomeCompleto = s.Profissional.NomeCompleto,
                        ServicoId = s.ServicoId,
                        Data = s.Data,
                        Avaliacao = s.Avaliacao
                    }).ToList();
        }
    }
}