using GetServiceApi.Repositories;
using GetServiceApi.ViewModel;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace GetServiceApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Conta")]
    public class ContaController : ApiController
    {
        private AuthRepository repo = null;

        public ContaController()
        {
            repo = new AuthRepository();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Registrar")]
        public async Task<IHttpActionResult> Registrar(RegistraUsuarioViewModel usuarioModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await repo.UserExists(usuarioModel.NomeUsuario))
            {
                return BadRequest("Nome de usuário já utilizado");
            }

            IdentityResult result = await repo.RegisterUserAsync(usuarioModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }
        
        [Route("Usuario")]
        public IHttpActionResult GetUsuario()
        {
            var usuario = repo.GetUsuario(User.Identity.Name);
                        
            return Ok(usuario);
        }

        [HttpPost]
        [Route("AlterarSenha")]
        public async Task<IHttpActionResult> AlterarSenha(AlterarSenhaUsuarioViewModel alterarSenha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userName = User.Identity.Name;

            IdentityResult result = await repo.AlterarSenha(userName, alterarSenha);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        [HttpPost]
        [Route("AlterarDados")]
        public async Task<IHttpActionResult> AlterarDados(AlterarDadosUsuarioViewModel alterarDados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userName = User.Identity.Name;

            IdentityResult result = await repo.AlterarDados(userName, alterarDados);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }
        
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
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
