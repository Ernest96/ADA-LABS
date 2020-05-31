
using EventSource.Entities;

namespace EventSource.Commands
{
    class ChangeTaskDescriptionCommand : Command
    {
        public Task Target;
        public string Description;

        public ChangeTaskDescriptionCommand(Task task, string description)
        {
            Target = task;
            Description = description;
        }
    }
}
