using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;


namespace ServerChat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<User> users = new List<User>();
        int nextId = 1;

        public int Connect(string name)
        {
            User user = new User()
            {
                Name = name,
                Id = nextId,
                operationContext = OperationContext.Current
            };
            nextId++;
            Send(user.Name + " доєднався", 0);
            users.Add(user);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                users.Remove(user);
            }
            Send(user.Name + " від'єднався", 0);
        }

        public void Send(string message, int id)
        {
            foreach (var u in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    answer += $" {user.Name} ";
                }
                answer += message;

                u.operationContext.GetCallbackChannel<IServerChatCallback>().SendCallback(answer);
            }
        }
    }
}
