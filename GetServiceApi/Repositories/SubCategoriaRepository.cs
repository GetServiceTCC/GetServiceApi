using GetServiceApi.DTOs;
using GetServiceApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class SubCategoriaRepository : BaseRepository<SubCategoria>
    {
        public SubCategoriaDto GetSubCategoria(int id)
        {
            return (from s in Entity
                    where s.Id == id
                    select new SubCategoriaDto()
                    {
                        Id = s.Id,
                        Descricao = s.Descricao,
                        CategoriaId = s.CategoriaId,
                        CategoriaDescricao = s.Categoria.Descricao
                    }).FirstOrDefault();
        }

        public IEnumerable<SubCategoriaDto> GetSubCategorias()
        {
            return (from s in Entity
                    select new SubCategoriaDto()
                    {
                        Id = s.Id,
                        Descricao = s.Descricao,
                        CategoriaId = s.CategoriaId,
                        CategoriaDescricao = s.Categoria.Descricao
                    }).ToList();
        }

        public IEnumerable<SubCategoriaDto> GetSubCategoriaPorCategoria(int categoriaId)
        {
            return (from s in Entity
                    where s.CategoriaId == categoriaId
                    select new SubCategoriaDto()
                    {
                        Id = s.Id,
                        Descricao = s.Descricao,
                        CategoriaId = s.CategoriaId,
                        CategoriaDescricao = s.Categoria.Descricao
                    }).ToList();
        }
    }
}