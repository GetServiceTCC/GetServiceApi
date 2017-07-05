using GetServiceApi.DTOs;
using GetServiceApi.Models;
using GetServiceApi.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetServiceApi
{
    [Authorize]
    public class ChatHub : Hub
    {
        private class UsuarioHub
        {
            public string ConnectionId { get; set; }

            public string UserId { get; set; }

            public string UserName { get; set; }

            public string NomeCompleto { get; set; }
        }

        AuthRepository userRepo = null;
        MensagemRepository mensagemRepo = null;

        public ChatHub()
        {
            userRepo = new AuthRepository();
            mensagemRepo = new MensagemRepository();
        }

        static List<UsuarioHub> ConnectedUsers = new List<UsuarioHub>();
        
        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;

            if (ConnectedUsers.Count(c => c.ConnectionId == connectionId) == 0)
            {
                ConnectedUsers.Add(new UsuarioHub()
                {
                    ConnectionId = connectionId,
                    UserName = Context.User.Identity.Name,
                    NomeCompleto = userRepo.GetUsuario(Context.User.Identity.Name).NomeCompleto
                });
            }

            return base.OnConnected();
        }

        public void EnviarMensagem(string paraUserName, string mensagem)
        {
            if (string.IsNullOrEmpty(mensagem))
                return;

            UsuarioDto usuario = userRepo.GetUsuario(paraUserName);

            if (usuario == null)
                return;

            Mensagem msg = new Mensagem();

            msg.RemetenteId = Context.User.Identity.GetUserId();
            msg.DestinatarioId = usuario.UserId;
            msg.Texto = mensagem;
            msg.Data = DateTime.Now;

            mensagemRepo.Add(msg);

            string deUserId = Context.ConnectionId;

            var paraUser = ConnectedUsers.FirstOrDefault(f => f.UserName == paraUserName);
            var deUser = ConnectedUsers.FirstOrDefault(f => f.ConnectionId == deUserId);

            if (paraUser != null && deUser != null)
            {
                Clients.Client(paraUser.ConnectionId).ReceberMensagem(deUser.UserName, deUser.NomeCompleto, mensagem);
            }

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(f => f.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);
            }

            return base.OnDisconnected(stopCalled);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userRepo.Dispose();
                mensagemRepo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}