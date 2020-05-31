using EventSource.Entities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSource.Events
{
    [ProtoContract(SkipConstructor = true)]
    class TaskDescriptionChangeEvent : Event
    {

        public Task Target;
        [ProtoMember(2)]
        public string OldDescription;
        [ProtoMember(3)]
        public string NewDescription;


        public TaskDescriptionChangeEvent(Task task, string oldValue, string newValue)
        {
            Type = EventType.Change;
            Target = task;
            OldDescription = oldValue;
            NewDescription = newValue;
            EntityName = Target.GetType().Name;
        }

        public override string ToString()
        {
            return $"Task description from {OldDescription} to {NewDescription}";
        }
    }
}
