using EventSource.Entities;
using System;

namespace EventSource.Commands
{
    class CreateTaskCommentCommand : Command
    {
        public TaskComment Target;
        public string Id;
        public int Version;
        public string Content;
        public DateTime CreatedAt;

        public CreateTaskCommentCommand(TaskComment taskComment, string content)
        {
            Target = taskComment;
            Version = 1;
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Content = content;
        }
    }
}
