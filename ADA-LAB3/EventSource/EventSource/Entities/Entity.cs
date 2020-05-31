using System;
using System.Collections.Generic;
using System.Text;

namespace EventSource.Entities
{
    abstract class Entity
    {
        public string Id { get; protected set; }
        protected int Version;
        protected DateTime CreatedAt;
        protected EventBroker Broker;
    }
}
