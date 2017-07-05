using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetServiceApi.Repositories
{
    public class AuthRepository : IDisposable
    {
        private ApiDbContext _ctx;

        private UserManager<Usuario> _userManager;

        public AuthRepository()
        {
            _ctx = new ApiDbContext();
            _userManager = new UserManager<Usuario>(new UserStore<Usuario>(_ctx));
        }

        public AuthRepository(ApiDbContext context)
        {
            _ctx = context;
            _userManager = new UserManager<Usuario>(new UserStore<Usuario>(_ctx));
        }

        public async Task<bool> UserExists(string userName)
        {
            var usuario = await _userManager.FindByNameAsync(userName);
            return usuario != null;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegistraUsuarioViewModel usuarioModel)
        {
            Usuario user = new Usuario
            {
                UserName = usuarioModel.NomeUsuario,
                NomeCompleto = usuarioModel.NomeCompleto,
                Status = usuarioModel.Status,
                CidadeId = usuarioModel.CidadeId,
                Endereco = usuarioModel.Endereco,
                Profissional = usuarioModel.Profissional                            
            };
                        
            var result = await _userManager.CreateAsync(user, usuarioModel.Senha);

            return result;
        }

        public void RegisterUser(RegistraUsuarioViewModel usuarioModel)
        {
            Usuario user = new Usuario
            {
                UserName = usuarioModel.NomeUsuario,
                NomeCompleto = usuarioModel.NomeCompleto,
                Status = usuarioModel.Status,
                CidadeId = usuarioModel.CidadeId,
                Endereco = usuarioModel.Endereco,
                Profissional = usuarioModel.Profissional
            };

            _userManager.Create(user, usuarioModel.Senha);
        }

        public async Task<IdentityResult> AlterarSenha(string userName, AlterarSenhaUsuarioViewModel alterarSenha)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var update = await _userManager.ChangePasswordAsync(user.Id, alterarSenha.AntigaSenha, alterarSenha.NovaSenha);
            return update;
        }

        public async Task<IdentityResult> AlterarDados(string userName, AlterarDadosUsuarioViewModel alterarDados)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.NomeCompleto = alterarDados.NomeCompleto;
            user.Status = alterarDados.Status;
            user.CidadeId = alterarDados.CidadeId;
            user.Endereco = alterarDados.Endereco;
            user.Profissional = alterarDados.Profissional;
            var update = await _userManager.UpdateAsync(user);
            return update;
        }

        public async Task<Usuario> FindUser(string nome, string senha)
        {
            Usuario user = await _userManager.FindAsync(nome, senha);

            return user;
        }

        public UsuarioDto GetUsuario(string userName)
        {
            var usuario = (from s in _ctx.Users
                           where s.UserName == userName
                           select new UsuarioDto()
                           {
                               UserId = s.Id,
                               NomeUsuario = s.UserName,
                               NomeCompleto = s.NomeCompleto,
                               Status = s.Status,
                               CidadeId = s.CidadeId,
                               EstadoId = s.Cidade.EstadoId,
                               Endereco = s.Endereco,
                               Profissional = s.Profissional
                           }).FirstOrDefault();

            if (usuario == null)
                throw new Exception("usuário não encontrado");

            return usuario;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }
    }
}