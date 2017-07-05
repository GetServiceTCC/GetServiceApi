using GetServiceApi.DTOs;
using GetServiceApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace GetServiceApi.Controllers
{
    [Authorize]
    public class MensagensController : ApiController
    {
        MensagemRepository repo = null;

        public MensagensController()
        {
            repo = new MensagemRepository();
        }

        [Route("api/{contato}/Mensagens")]
        public IEnumerable<MensagemDto> GetMensagens(string contato)
        {
            return repo.GetMensagens(User.Identity.Name, contato);
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
