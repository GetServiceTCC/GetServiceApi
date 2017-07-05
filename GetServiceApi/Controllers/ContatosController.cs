using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Models.Enums;
using GetServiceApi.Repositories;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    [Authorize]
    public class ContatosController : ApiController
    {
        ContatoRepository repo = null;
        AuthRepository userRepo = null;

        public ContatosController()
        {
            repo = new ContatoRepository();
            userRepo = new AuthRepository();
        }

        [ResponseType(typeof(ContatoDto))]
        [Route("api/Contatos/{nome}")]
        public IHttpActionResult GetContato(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("Informe o contato");
            }

            ContatoDto contato = repo.GetContato(User.Identity.Name, nome);
            if (contato == null)
            {
                return NotFound();
            }

            return Ok(contato);
        }

        public IEnumerable<ContatoDto> GetContatos()
        {
            return repo.GetContatos(User.Identity.Name);
        }

        [ResponseType(typeof(Contato))]
        public IHttpActionResult PostContato([FromBody]string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("Informe o contato");
            }

            if (nome == User.Identity.Name)
            {
                return BadRequest("Não pode adicionar a si mesmo como um cotato");
            }

            UsuarioDto usuarioContato = userRepo.GetUsuario(nome);

            if (usuarioContato == null)
            {
                return NotFound();
            }

            Contato contato = new Contato();

            contato.UsuarioId = User.Identity.GetUserId();
            contato.UsuarioContatoId = usuarioContato.UserId;
            contato.Status = ContatoStatus.Aceito;

            repo.Add(contato);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
                userRepo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
