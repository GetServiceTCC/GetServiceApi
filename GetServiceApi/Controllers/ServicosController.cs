using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Repositories;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    [Authorize]
    public class ServicosController : ApiController
    {
        private ServicoRepository repo = null;

        public ServicosController()
        {
            repo = new ServicoRepository();
        }

        [AllowAnonymous]
        [ResponseType(typeof(ServicoDto))]
        public IHttpActionResult GetServico(int id)
        {
            ServicoDto servico = repo.GetServico(id);
            if (servico == null)
            {
                return NotFound();
            }

            if (servico.Profissional != User.Identity.Name)
            {
                return BadRequest("Profissional Invalido");
            }

            return Ok(servico);
        }

        [AllowAnonymous]
        public IEnumerable<ServicoDto> GetServicos(string q, int? estado = null, int? cidade = null, int? categoria = null, int? subCategoria = null)
        {
            return repo.GetServicos(q, estado, cidade, categoria, subCategoria);
        }

        [AllowAnonymous]
        [Route("api/Servicos/destaque")]
        public IEnumerable<ServicoDto> GetServicosDestaque()
        {
            return repo.GetServicosDestaque();
        }
        
        [AllowAnonymous]
        [Route("api/Profissionais/{profissional}/Servicos")]
        public IEnumerable<ServicoDto> GetServicosPorProfissional(string profissional)
        {
            return repo.GetServicosPorProfissional(profissional);
        }
        
        [Route("api/Servicos/meus")]
        public IEnumerable<ServicoDto> GetServicosMeus()
        {
            return repo.GetServicosMeus(User.Identity.Name);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutServico(int id, Servico servico)
        {
            if (servico == null)
            {
                return BadRequest("Informe o serviço");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servico.Id)
            {
                return BadRequest();
            }

            Servico servicoAtualizado = repo.GetById(id);

            if (servicoAtualizado == null)
            {
                return NotFound();
            }

            if (servicoAtualizado.ProfissionalId != User.Identity.GetUserId())
            {
                return BadRequest("Profissional Invalido");
            }

            servicoAtualizado.Descricao = servico.Descricao;
            servicoAtualizado.Sobre = servico.Sobre;
            servicoAtualizado.Ativo = servico.Ativo;
            servicoAtualizado.SubCategoriaId = servico.SubCategoriaId;
            servicoAtualizado.TipoValor = servico.TipoValor;
            servicoAtualizado.Valor = servico.Valor;

            repo.Update(servicoAtualizado);

            return Ok();
        }

        [ResponseType(typeof(Servico))]
        public IHttpActionResult PostServico(Servico servico)
        {
            if (servico == null)
            {
                return BadRequest("Informe o serviço");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            servico.ProfissionalId = User.Identity.GetUserId();

            repo.Add(servico);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
