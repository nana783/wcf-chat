using System.ServiceModel;

namespace ServerChat
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
