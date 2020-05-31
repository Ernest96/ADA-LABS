using EventSource.Entities;

namespace EventSource.Commands
{
    class ChangeCommentContentCommand : Command
    {
        public TaskComment Target;
        public string Content;

        public ChangeCommentContentCommand(TaskComment taskComment, string content)
        {
            Target = taskComment;
            Content = content;
        }
    }
}
