using System.ServiceModel;

namespace wcf_chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;
            SendMessage(user.Name + " приєднався до чату", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage(user.Name + " залишив чат", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var u in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(x => x.ID == id);
                if (user != null)
                {
                    answer += " " + user.Name + " ";
                }
                answer += message;
                u.operationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}
