using EventSource.Entities;
using ProtoBuf;

namespace EventSource.Events
{
    [ProtoContract(SkipConstructor = true)]
    class TaskCommentChangeEvent : Event
    {
        public TaskComment Target;
        [ProtoMember(2)]
        public string OldComment;
        [ProtoMember(3)]
        public string NewComment;


        public TaskCommentChangeEvent(TaskComment taskComment, string oldValue, string newValue)
        {
            Type = EventType.Change;
            Target = taskComment;
            OldComment = oldValue;
            NewComment = newValue;
            EntityName = Target.GetType().Name;
        }

        public override string ToString()
        {
            return $"Comment changed from {OldComment} to {NewComment}";
        }
    }
}
