using EventSource.Entities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSource.Events
{
    [ProtoContract(SkipConstructor = true)]
    class TaskCreateEvent : Event
    {
        public Task Target;

        public TaskCreateEvent(Task taskComment)
        {
            Type = EventType.Create;
            Target = taskComment;
            EntityName = Target.GetType().Name;
        }

        public override string ToString()
        {
            return $"Comment Created {Target.Id}";
        }
    }
}
