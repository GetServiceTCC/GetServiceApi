using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace GetServiceApi.Controllers
{
    [Authorize(Users = "admin")]
    public class SubCategoriasController : ApiController
    {
        private SubCategoriaRepository repo = null;

        public SubCategoriasController()
        {
            repo = new SubCategoriaRepository();
        }

        [AllowAnonymous]
        public IEnumerable<SubCategoriaDto> GetSubCategorias()
        {
            return repo.GetSubCategorias();
        }

        [ResponseType(typeof(SubCategoriaDto))]
        public IHttpActionResult GetSubCategoria(int id)
        {
            SubCategoriaDto subCategoria = repo.GetSubCategoria(id);

            if (subCategoria == null)
            {
                return NotFound();
            }

            return Ok(subCategoria);
        }

        [AllowAnonymous]
        [Route("api/Categorias/{categoriaId}/SubCategorias")]
        public IEnumerable<SubCategoriaDto> GetSubCategoriaPorCategoria(int categoriaId)
        {
            return repo.GetSubCategoriaPorCategoria(categoriaId);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubCategoria(int id, SubCategoria subCategoria)
        {
            if (subCategoria == null)
            {
                return BadRequest("Informe a sub categoria");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategoria.Id)
            {
                return BadRequest();
            }

            SubCategoria subCategoriaAtualizada = repo.GetById(id);

            if (subCategoriaAtualizada == null)
            {
                return NotFound();
            }

            subCategoriaAtualizada.Descricao = subCategoria.Descricao;
            subCategoriaAtualizada.CategoriaId = subCategoria.CategoriaId;

            repo.Update(subCategoriaAtualizada);

            return Ok();
        }

        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult PostSubCategoria(SubCategoria subCategoria)
        {
            if (subCategoria == null)
            {
                return BadRequest("Informe a sub categoria");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(subCategoria);

            return Ok();
        }

        [ResponseType(typeof(SubCategoria))]
        public IHttpActionResult DeleteSubCategoria(int id)
        {
            SubCategoria subCategoria = repo.GetById(id);
            if (subCategoria == null)
            {
                return NotFound();
            }

            repo.Remove(subCategoria);

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
