using EventSource.Commands;
using EventSource.Events;
using EventSource.Queries;
using ProtoBuf;
using System;

namespace EventSource.Entities
{

    class TaskComment : Entity
    {
        private string Content;

        public TaskComment(EventBroker broker)
        {
            Broker = broker;
            Broker.Commands += BrokerOnCommands;
            Broker.Queries += BrokerOnQueries;
        }

        private void BrokerOnCommands(object sender, Command command)
        {
            var createCommand = command as CreateTaskCommentCommand;
            if (createCommand != null && createCommand.Target == this)
            {
                Broker.AllEvents.Add(new TaskCommentCreateEvent(this));
                Version = createCommand.Version;
                Id = createCommand.Id;
                CreatedAt = createCommand.CreatedAt;
                Content = createCommand.Content;
            }

            var changeCommentContentCommand = command as ChangeCommentContentCommand;
            if (changeCommentContentCommand != null && changeCommentContentCommand.Target == this)
            {
                Broker.AllEvents.Add(new TaskCommentChangeEvent(this, Content, changeCommentContentCommand.Content));
                Content = changeCommentContentCommand.Content;
                Version++;
            }
        }

        private void BrokerOnQueries(object sender, Query query)
        {
            var contentQuery = query as CommentQuery;
            if (contentQuery != null && contentQuery.Target == this)
            {
                contentQuery.Result = this.Content;
            }
        }
    }
}
