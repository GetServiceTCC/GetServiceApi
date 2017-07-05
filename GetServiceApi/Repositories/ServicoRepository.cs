using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class ServicoRepository : BaseRepository<Servico>
    {
        IQueryable<ServicoDto> consulta;

        public ServicoRepository()
        {
            consulta = (from s in Context.Servicos
                        join c in Context.Comentarios on s.Id equals c.ServicoId into comentarios
                        from cs in comentarios.DefaultIfEmpty()

                        group new { s, cs } by new
                        {
                            s.Profissional.UserName,
                            s.Id,
                            s.Descricao,
                            s.Sobre,
                            s.Profissional.CidadeId,
                            s.Profissional.Cidade.EstadoId,
                            s.SubCategoria.CategoriaId,
                            Categoria = s.SubCategoria.Categoria.Descricao,
                            s.SubCategoriaId,
                            SubCategoria = s.SubCategoria.Descricao,
                            s.TipoValor,
                            s.Valor,
                            s.Ativo
                        } into g

                        select new ServicoDto
                        {
                            Profissional = g.Key.UserName,
                            Id = g.Key.Id,
                            Descricao = g.Key.Descricao,
                            Sobre = g.Key.Sobre,
                            CidadeId = g.Key.CidadeId,
                            EstadoId = g.Key.EstadoId,
                            CategoriaId = g.Key.CategoriaId,
                            Categoria = g.Key.Categoria,
                            SubCategoriaId = g.Key.SubCategoriaId,
                            SubCategoria = g.Key.SubCategoria,
                            TipoValor = g.Key.TipoValor,
                            Valor = g.Key.Valor,
                            Ativo = g.Key.Ativo,
                            QtdComentarios = g.Count(a => a.cs != null),
                            Avaliacao = g.Average(a => a.cs.Avaliacao)
                        });
        }

        public IEnumerable<ServicoDto> GetServicosMeus(string userName)
        {
            return consulta.Where(w => w.Profissional == userName).ToList();
        }

        public IEnumerable<ServicoDto> GetServicosPorProfissional(string profissional)
        {
            return consulta.Where(w => w.Profissional == profissional && w.Ativo).ToList();
        }

        public ServicoDto GetServico(int id)
        {
            return consulta.Where(w => w.Id == id).FirstOrDefault();
        }

        public IEnumerable<ServicoDto> GetServicos(string q, int? estado, int? cidade, int? categoria, int? subCategoria)
        {
            var servicos = consulta.Where(w => w.Ativo && w.Descricao.Contains(q));

            if (estado != null)
                servicos = servicos.Where(w => w.EstadoId == estado);

            if (cidade != null)
                servicos = servicos.Where(w => w.CidadeId == cidade);

            if (categoria != null)
                servicos = servicos.Where(w => w.CategoriaId == categoria);

            if (subCategoria != null)
                servicos = servicos.Where(w => w.SubCategoriaId == subCategoria);

            return servicos.ToList();
        }

        public IEnumerable<ServicoDto> GetServicosDestaque()
        {
            return consulta.
                Where(w => w.Ativo && w.Avaliacao > 0).
                OrderByDescending(o => o.Avaliacao).
                ThenByDescending(o => o.QtdComentarios).
                Take(10);
        }
    }
}