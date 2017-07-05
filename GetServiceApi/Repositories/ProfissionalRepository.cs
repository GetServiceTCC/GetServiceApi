using GetServiceApi.DTOs;
using GetServiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class ProfissionalRepository: IDisposable
    {
        protected ApiDbContext context = null;

        private IQueryable<ProfissionalDto> consulta;

        public ProfissionalRepository()
        {
            context = new ApiDbContext();

            consulta = (from p in context.Users
                        join s in context.Servicos on p.Id equals s.ProfissionalId
                        join c in context.Comentarios on s.Id equals c.ServicoId into comentarios
                        from cs in comentarios.DefaultIfEmpty()

                        group new { p, cs } by new
                        {
                            p.Id,
                            p.UserName,
                            p.NomeCompleto,
                            p.Status,
                            p.CidadeId,
                            Cidade = p.Cidade.Nome,
                            p.Cidade.EstadoId,
                            Estado = p.Cidade.Estado.Nome,
                            Uf = p.Cidade.Estado.Uf,
                            p.Endereco
                        } into g

                        select new ProfissionalDto
                        {
                            UserId = g.Key.Id,
                            UserName = g.Key.UserName,
                            NomeCompleto = g.Key.NomeCompleto,
                            Status = g.Key.Status,
                            CidadeId = g.Key.CidadeId,
                            Cidade = g.Key.Cidade,
                            EstadoId = g.Key.EstadoId,
                            Estado = g.Key.Estado,
                            Uf = g.Key.Uf,
                            Endereco = g.Key.Endereco,
                            QtdComentarios = g.Count(a => a.cs != null),
                            Avaliacao = g.Average(a => a.cs.Avaliacao)
                        });
        }

        public ProfissionalDto GetProfissional(string profissional)
        {
            return consulta.Where(w => w.UserName == profissional).FirstOrDefault();
        }

        public IEnumerable<ProfissionalDto> GetProfissionais(string q, int? estado, int? cidade, int? categoria, int? subCategoria)
        {
            var profissionais = consulta.Where(w => w.NomeCompleto.Contains(q));

            if (estado != null)
                profissionais = consulta.Where(w => w.EstadoId == estado);

            if (cidade != null)
                profissionais = consulta.Where(w => w.CidadeId == cidade);

            if (categoria != null)
                profissionais = consulta.Where(w => context.Servicos.Any(a => a.ProfissionalId == w.UserId && a.SubCategoria.CategoriaId == categoria));

            if (subCategoria != null)
                profissionais = consulta.Where(w => context.Servicos.Any(a => a.ProfissionalId == w.UserId && a.SubCategoriaId == subCategoria));

            return profissionais.ToList();
        }

        public IEnumerable<ProfissionalDto> GetProfissionaisDestaque()
        {
            return consulta.
                Where(w => w.Avaliacao > 0).
                OrderByDescending(o => o.Avaliacao).
                ThenByDescending(t => t.QtdComentarios).
                Take(10);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}