using EventSource.Commands;
using EventSource.Entities;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.IO;

namespace EventSource
{
    class Program
    {
        private static EventBroker _eventBroker = new EventBroker();

        static void Main(string[] args)
        {
            User user = new User("vasea");

            var task = new Task(_eventBroker);
            var taskComment = new TaskComment(_eventBroker);

            _eventBroker.Command(new CreateTaskCommand(task, "Test title", "this is description...", user.Id));
            _eventBroker.Command(new CreateTaskCommentCommand(taskComment, "First"));

            for (int i = 0; i < 500; i++)
            {
                _eventBroker.Command(new ChangeCommentContentCommand(taskComment, $"Description {i}"));
                _eventBroker.Command(new ChangeTaskDescriptionCommand(task, $"Title {i}"));

            }

            string json = JsonConvert.SerializeObject(_eventBroker.AllEvents);
            File.WriteAllText(@"D:\Programming\master\ADA\ADA-LABS\ADA-LAB3\EventSource\events.json", json);

            using (var file = File.Create(@"D:\Programming\master\ADA\ADA-LABS\ADA-LAB3\EventSource\events.proto"))
            {
                Serializer.Serialize(file, _eventBroker.AllEvents);
            }

            Console.ReadKey();
        }
    }
}
