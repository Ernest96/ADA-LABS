using EventSource.Entities;
using System;

namespace EventSource.Commands
{
    class CreateTaskCommand : Command
    {
        public Task Target;
        public string Id;
        public int Version;
        public string Description;
        public string Title;
        public string UserId;
        public DateTime CreatedAt;

        public CreateTaskCommand(Task task, string title, string description, string userId)
        {
            Target = task;
            Version = 1;
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;

            Title = title;
            Description = description;
            UserId = userId;
        }
    }
}
