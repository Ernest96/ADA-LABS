using EventSource.Entities;
using ProtoBuf;

namespace EventSource.Events
{
    [ProtoContract(SkipConstructor = true)]
    class TaskCommentCreateEvent : Event
    {
        public TaskComment Target;

        public TaskCommentCreateEvent(TaskComment taskComment)
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
