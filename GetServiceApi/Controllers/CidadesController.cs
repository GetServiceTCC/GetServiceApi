using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    [Authorize(Users = "admin")]
    public class CidadesController : ApiController
    {
        private CidadeRepository repo = null;

        public CidadesController()
        {
            repo = new CidadeRepository();
        }

        [AllowAnonymous]
        public IEnumerable<CidadeDto> GetCidades()
        {
            return repo.GetCidades();
        }
        
        [ResponseType(typeof(CidadeDto))]
        public IHttpActionResult GetCidade(int id)
        {
            CidadeDto cidade = repo.GetCidade(id);

            if (cidade == null)
            {
                return NotFound();
            }

            return Ok(cidade);
        }

        [AllowAnonymous]
        [Route("api/Estados/{estadoId}/Cidades")]
        public IEnumerable<CidadeDto> GetCidadesPorEstado(int estadoId)
        {
            return repo.GetCidadesPorEstado(estadoId);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutCidade(int id, Cidade cidade)
        {
            if (cidade == null)
            {
                return BadRequest("Informe a cidade");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cidade.Id)
            {
                return BadRequest();
            }

            Cidade cidadeAtualizada = repo.GetById(id);

            if (cidadeAtualizada == null)
            {
                return NotFound();
            }

            cidadeAtualizada.Nome = cidade.Nome;
            cidadeAtualizada.EstadoId = cidade.EstadoId;

            repo.Update(cidadeAtualizada);

            return Ok();
        }
        
        [ResponseType(typeof(Cidade))]
        public IHttpActionResult PostCidade(Cidade cidade)
        {
            if (cidade == null)
            {
                return BadRequest("Informe a cidade");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(cidade);

            return Ok();
        }
        
        [ResponseType(typeof(Cidade))]
        public IHttpActionResult DeleteCidade(int id)
        {
            Cidade cidade = repo.GetById(id);
            if (cidade == null)
            {
                return NotFound();
            }

            repo.Remove(cidade);

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