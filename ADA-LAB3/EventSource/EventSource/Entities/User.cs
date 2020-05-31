using System;

namespace EventSource.Entities
{
    class User
    {
        public string Id;
        public string Name;

        public User(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }
    }
}
