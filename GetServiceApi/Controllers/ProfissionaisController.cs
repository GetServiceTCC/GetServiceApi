using GetServiceApi.DTOs;
using GetServiceApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    public class ProfissionaisController : ApiController
    {
        ProfissionalRepository repo = null;

        public ProfissionaisController()
        {
            repo = new ProfissionalRepository();
        }

        [ResponseType(typeof(ProfissionalDto))]
        public IHttpActionResult GetProfissional(string id)
        {
            var profissional = repo.GetProfissional(id);

            if (profissional == null)
            {
                return NotFound();
            }

            return Ok(profissional);
        }
        
        public IEnumerable<ProfissionalDto> GetProfissionais(string q, int? estado = null, int? cidade = null, int? categoria = null, int? subCategoria = null)
        {
            if (string.IsNullOrWhiteSpace(q))
                BadRequest("Pesquisa não informada");

            return repo.GetProfissionais(q, estado, cidade, categoria, subCategoria);
        }

        [Route("api/Profissionais/destaque")]
        public IEnumerable<ProfissionalDto> GetProfissionaisDestaque()
        {
            return repo.GetProfissionaisDestaque();
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
