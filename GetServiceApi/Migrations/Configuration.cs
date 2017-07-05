namespace GetServiceApi.Migrations
{
    using Helpers;
    using Models;
    using Models.Enums;
    using Repositories;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ViewModel;

    internal sealed class Configuration : DbMigrationsConfiguration<ApiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApiDbContext context)
        {
            if (context.Estados.Count() == 0)
            {
                context.Estados.Add(new Estado() { Nome = "Rondônia", Uf = "RO" });

                context.SaveChanges();
            }

            if (context.Cidades.Count() == 0)
            {
                context.Cidades.Add(new Cidade() { Nome = "Porto Velho", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Ji-Paraná", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Guajará-Mirim", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Ariquemes", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Vilhena", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Rolim de Moura", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Cacoal", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Alta Floresta D'Oeste", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Cerejeiras", EstadoId = 1 });
                context.Cidades.Add(new Cidade() { Nome = "Jaru", EstadoId = 1 });

                context.SaveChanges();
            }

            if (context.Categorias.Count() == 0)
            {
                context.Categorias.Add(new Categoria() { Descricao = "Informática e TI" });
                context.Categorias.Add(new Categoria() { Descricao = "Comunicação e Marketing" });
                context.Categorias.Add(new Categoria() { Descricao = "Construção e Reforma" });

                context.SaveChanges();
            }

            if (context.SubCategorias.Count() == 0)
            {
                context.SubCategorias.Add(new SubCategoria() { Descricao = "Softwares e Aplicativos", CategoriaId = 1 });
                context.SubCategorias.Add(new SubCategoria() { Descricao = "Computador Desktop", CategoriaId = 1 });
                context.SubCategorias.Add(new SubCategoria() { Descricao = "Notebook e Ultrabook", CategoriaId = 1 });
                context.SubCategorias.Add(new SubCategoria() { Descricao = "Redes e Equipamentos", CategoriaId = 1 });

                context.SubCategorias.Add(new SubCategoria() { Descricao = "Designer Gráfico", CategoriaId = 2 });
                context.SubCategorias.Add(new SubCategoria() { Descricao = "Marketing Digital", CategoriaId = 2 });

                context.SubCategorias.Add(new SubCategoria() { Descricao = "Arquitetos", CategoriaId = 3 });
                context.SubCategorias.Add(new SubCategoria() { Descricao = "Engenharia Civil", CategoriaId = 3 });

                context.SaveChanges();
            }

            if (context.Clients.Count() == 0)
            {
                context.Clients.Add(new Client
                {
                    Id = "GetServiceWeb",
                    Secret = Helper.GetHash("web@123"),
                    Name = "Aplicação Web GetService",
                    ApplicationType = ApplicationTypes.Web,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                });

                context.Clients.Add(new Client
                {
                    Id = "GetServiceMobile",
                    Secret = Helper.GetHash("mobile@123"),
                    Name = "Aplicação Mobile GetService",
                    ApplicationType = ApplicationTypes.Mobile,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                });

                context.SaveChanges();
            }

            if (context.Users.Count() == 0)
            {
                AuthRepository auth = new AuthRepository(context);

                auth.RegisterUser(new RegistraUsuarioViewModel()
                {
                    NomeUsuario = "admin",
                    NomeCompleto = "Admin",
                    Status = "",
                    CidadeId = 1,
                    Endereco = "",
                    Profissional = false,
                    Senha = "123456",
                    ConfirmaSenha = "123456"
                });
            }
        }
    }
}
