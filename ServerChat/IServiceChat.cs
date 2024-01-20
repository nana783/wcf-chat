using System.ServiceModel;

namespace ServerChat
{
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void Send(string message, int id);
    }


    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendCallback(string message);
    }
}
