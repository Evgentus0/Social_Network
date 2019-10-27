using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SocialNetwork_PL.Hubs;

namespace SocialNetwork_PL.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserFotChat> UserForChats = new List<UserFotChat>();

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        // Подключение нового пользователя
        public void Connect(string UserForChatsForChatName)
        {
            var id = Context.ConnectionId;


            if (!UserForChats.Any(x => x.ConnectionId == id))
            {
                UserForChats.Add(new UserFotChat { ConnectionId = id, Name = UserForChatsForChatName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, UserForChatsForChatName, UserForChats);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserForChatsForChatConnected(id, UserForChatsForChatName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = UserForChats.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                UserForChats.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserForChatsForChatDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}