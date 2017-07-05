using GetServiceApi.Models;
using GetServiceApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace GetServiceApi.Controllers
{
    [Authorize(Users = "admin")]
    public class CategoriasController : ApiController
    {
        private CategoriaRepository repo = null;

        public CategoriasController()
        {
            repo = new CategoriaRepository();
        }

        [AllowAnonymous]
        public IEnumerable<Categoria> GetCategorias()
        {
            return repo.GetAll();
        }
        
        public IHttpActionResult GetCategoria(int id)
        {
            Categoria categoria = repo.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }
        
        public IHttpActionResult PutCategoria(int id, Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Informe a categoria");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.Id)
            {
                return BadRequest();
            }

            Categoria categoriaAtualizada = repo.GetById(id);
            
            if (categoriaAtualizada == null)
            {
                return NotFound();
            }

            categoriaAtualizada.Descricao = categoria.Descricao;

            repo.Update(categoriaAtualizada);

            return Ok();
        }
        
        public IHttpActionResult PostCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Informe a categoria");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(categoria);

            return Ok();
        }
        
        public IHttpActionResult DeleteCategoria(int id)
        {
            Categoria categoria = repo.GetById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            repo.Remove(categoria);

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
