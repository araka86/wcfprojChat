using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Wcf_Chat_Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServiceChat" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]//все клиенты которые будут подключатся к нашему хосту(сервисисом)- будут работать с нашым сервисом
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {

            ServerUser user = new ServerUser()
            {
                Id = nextId++,
                Name = name,
                operationContext = OperationContext.Current
            };
            SendMsg(user.Name + " Подключился к чату!!!", 0);
            users.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(user.Name + " покинул Чвт!!!", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {

            foreach (var item in users)
            {
                DateTime currentDate = DateTime.Now;
                string answer = currentDate.ToString("dd.MM.yyyy HH:mm:ss");
                // string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                    answer += ": " + user.Name + " ";


                answer += $" {msg}";

                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }

        }
    }
}
