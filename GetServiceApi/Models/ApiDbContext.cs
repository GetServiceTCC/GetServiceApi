using GetServiceApi.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GetServiceApi.Models
{
    public class ApiDbContext : IdentityDbContext<Usuario>
    {
        public ApiDbContext() : base("ApiDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApiDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApiDbContext, Configuration>());

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
    }
}