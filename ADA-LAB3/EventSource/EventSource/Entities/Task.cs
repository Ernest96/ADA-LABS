using EventSource.Commands;
using EventSource.Events;
using EventSource.Queries;
using ProtoBuf;

namespace EventSource.Entities
{
    class Task : Entity
    {
        private string Description;
        private string Title;
        private string UserId;

        public Task(EventBroker broker)
        {
            Broker = broker;
            Broker.Commands += BrokerOnCommands;
            Broker.Queries += BrokerOnQueries;
        }

        private void BrokerOnCommands(object sender, Command command)
        {
            var createCommand = command as CreateTaskCommand;
            if (createCommand != null && createCommand.Target == this)
            {
                Broker.AllEvents.Add(new TaskCreateEvent(this));
                Version = createCommand.Version;
                Id = createCommand.Id;
                CreatedAt = createCommand.CreatedAt;
                Description = createCommand.Description;
                Title = createCommand.Title;
                UserId = createCommand.UserId;
            }

            var changeDescriptionCommand = command as ChangeTaskDescriptionCommand;
            if (changeDescriptionCommand != null && changeDescriptionCommand.Target == this)
            {
                Broker.AllEvents.Add(new TaskDescriptionChangeEvent(this, Description, changeDescriptionCommand.Description));
                Description = changeDescriptionCommand.Description;
                Version++;
            }
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            
        }
    }
}
