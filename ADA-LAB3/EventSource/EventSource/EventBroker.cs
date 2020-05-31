using EventSource.Commands;
using EventSource.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventSource
{
    class EventBroker
    {
        public IList<Event> AllEvents = new List<Event>();

        public event EventHandler<Command> Commands;

        public event EventHandler<Query> Queries;

        public void Command(Command command)
        {
            Commands.Invoke(this, command);
        }

        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }
    }
}
