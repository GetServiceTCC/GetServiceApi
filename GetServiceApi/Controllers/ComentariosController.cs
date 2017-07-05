using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    [Authorize]
    public class ComentariosController : ApiController
    {
        private ComentarioRepository repo = null;

        public ComentariosController()
        {
            repo = new ComentarioRepository();
        }

        [AllowAnonymous]
        [ResponseType(typeof(Comentario))]
        public IHttpActionResult GetComentario(int id)
        {
            Comentario comentario = repo.GetById(id);
            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }

        [AllowAnonymous]
        [Route("api/Servicos/{idServico}/Comentarios")]
        public IEnumerable<ComentarioDto> GetComentarios(int idServico)
        {
            return repo.GetComentarioPorServico(idServico);
        }

        [ResponseType(typeof(Comentario))]
        public IHttpActionResult PostComentario(Comentario comentario)
        {
            if (comentario == null)
            {
                return BadRequest("Informe o serviço");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comentario.ProfissionalId = User.Identity.GetUserId();
            comentario.Data = DateTime.Now;

            repo.Add(comentario);

            return Ok();
        }

        public IHttpActionResult DeleteComentario(int id)
        {
            Comentario comentario = repo.GetById(id);
            if (comentario == null)
            {
                return NotFound();
            }

            repo.Remove(comentario);

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
