﻿using System.ServiceModel;

namespace server
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
