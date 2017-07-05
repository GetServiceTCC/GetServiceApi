using GetServiceApi.DTOs;
using GetServiceApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class CidadeRepository : BaseRepository<Cidade>
    {
        public CidadeDto GetCidade(int id)
        {
            return (from s in Entity
                    where s.Id == id
                    select new CidadeDto()
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        EstadoId = s.Estado.Id,
                        EstadoNome = s.Estado.Nome
                    }).FirstOrDefault();
        }

        public IEnumerable<CidadeDto> GetCidades()
        {
            return (from s in Entity
                    select new CidadeDto()
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        EstadoId = s.Estado.Id,
                        EstadoNome = s.Estado.Nome
                    }).ToList();
        }

        public IEnumerable<CidadeDto> GetCidadesPorEstado(int estadoId)
        {
            return (from s in Entity
                    where s.EstadoId == estadoId
                    select new CidadeDto()
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        EstadoId = s.Estado.Id,
                        EstadoNome = s.Estado.Nome
                    }).ToList();
        }
    }
}