using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Wcf_Chat_Server
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceChat" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]//для того что б сервис знал об IServerChatCallback
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);
        [OperationContract]
        void Disconnect(int id);
        [OperationContract(IsOneWay = true)] //no need to wait for a response from the server
        void SendMsg(string msg, int id); //id from user

    }
    //для вызова какого то действия на стороне клиента со стороны сервера
    //Реализация Callback на клиентской части (таким образом клиент будет получать сообщение от сервера )
    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }

}
