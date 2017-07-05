namespace GetServiceApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cidades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estados", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Uf = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 300),
                        ProfissionalId = c.String(maxLength: 128),
                        ServicoId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Avaliacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfissionalId)
                .ForeignKey("dbo.Servicos", t => t.ServicoId)
                .Index(t => t.ProfissionalId)
                .Index(t => t.ServicoId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NomeCompleto = c.String(nullable: false, maxLength: 100),
                        Status = c.String(maxLength: 150),
                        CidadeId = c.Int(nullable: false),
                        Endereco = c.String(maxLength: 100),
                        Profissional = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cidades", t => t.CidadeId)
                .Index(t => t.CidadeId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Servicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                        Sobre = c.String(nullable: false, maxLength: 150),
                        Ativo = c.Boolean(nullable: false),
                        SubCategoriaId = c.Int(nullable: false),
                        TipoValor = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        ProfissionalId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfissionalId)
                .ForeignKey("dbo.SubCategorias", t => t.SubCategoriaId)
                .Index(t => t.SubCategoriaId)
                .Index(t => t.ProfissionalId);
            
            CreateTable(
                "dbo.SubCategorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(maxLength: 50),
                        CategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.CategoriaId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Contatos",
                c => new
                    {
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                        UsuarioContatoId = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.UsuarioContatoId })
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioContatoId)
                .Index(t => t.UsuarioId)
                .Index(t => t.UsuarioContatoId);
            
            CreateTable(
                "dbo.Mensagens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemetenteId = c.String(nullable: false, maxLength: 128),
                        DestinatarioId = c.String(nullable: false, maxLength: 128),
                        Texto = c.String(nullable: false, maxLength: 1000),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DestinatarioId)
                .ForeignKey("dbo.AspNetUsers", t => t.RemetenteId)
                .Index(t => t.RemetenteId)
                .Index(t => t.DestinatarioId);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Mensagens", "RemetenteId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Mensagens", "DestinatarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contatos", "UsuarioContatoId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contatos", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comentarios", "ServicoId", "dbo.Servicos");
            DropForeignKey("dbo.Servicos", "SubCategoriaId", "dbo.SubCategorias");
            DropForeignKey("dbo.SubCategorias", "CategoriaId", "dbo.Categorias");
            DropForeignKey("dbo.Servicos", "ProfissionalId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comentarios", "ProfissionalId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CidadeId", "dbo.Cidades");
            DropForeignKey("dbo.Cidades", "EstadoId", "dbo.Estados");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Mensagens", new[] { "DestinatarioId" });
            DropIndex("dbo.Mensagens", new[] { "RemetenteId" });
            DropIndex("dbo.Contatos", new[] { "UsuarioContatoId" });
            DropIndex("dbo.Contatos", new[] { "UsuarioId" });
            DropIndex("dbo.SubCategorias", new[] { "CategoriaId" });
            DropIndex("dbo.Servicos", new[] { "ProfissionalId" });
            DropIndex("dbo.Servicos", new[] { "SubCategoriaId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CidadeId" });
            DropIndex("dbo.Comentarios", new[] { "ServicoId" });
            DropIndex("dbo.Comentarios", new[] { "ProfissionalId" });
            DropIndex("dbo.Cidades", new[] { "EstadoId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Mensagens");
            DropTable("dbo.Contatos");
            DropTable("dbo.SubCategorias");
            DropTable("dbo.Servicos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Clients");
            DropTable("dbo.Estados");
            DropTable("dbo.Cidades");
            DropTable("dbo.Categorias");
        }
    }
}
