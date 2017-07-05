using GetServiceApi.Models;
using GetServiceApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    [Authorize(Users = "admin")]
    public class EstadosController : ApiController
    {
        private EstadoRepository repo = null;

        public EstadosController()
        {
            repo = new EstadoRepository();
        }

        [AllowAnonymous]
        public IEnumerable<Estado> GetEstados()
        {
            return repo.GetAll();
        }
        
        [ResponseType(typeof(Estado))]
        public IHttpActionResult GetEstado(int id)
        {
            Estado estado = repo.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }

            return Ok(estado);
        }
        
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstado(int id, Estado estado)
        {
            if (estado == null)
            {
                return BadRequest("Informe o estado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estado.Id)
            {
                return BadRequest();
            }

            Estado estadoAtualizado = repo.GetById(id);

            if (estadoAtualizado == null)
            {
                return NotFound();
            }

            estadoAtualizado.Nome = estado.Nome;
            estadoAtualizado.Uf = estado.Uf;

            repo.Update(estadoAtualizado);

            return Ok();
        }
        
        [ResponseType(typeof(Estado))]
        public IHttpActionResult PostEstado(Estado estado)
        {
            if (estado == null)
            {
                return BadRequest("Informe o estado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(estado);

            return Ok();
        }
        
        [ResponseType(typeof(Estado))]
        public IHttpActionResult DeleteEstado(int id)
        {
            Estado estado = repo.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }

            repo.Remove(estado);

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