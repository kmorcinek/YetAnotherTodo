using KMorcinek.YetAnotherTodo.Extensions;

namespace KMorcinek.YetAnotherTodo.Models
{
    public class SlimTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsShown { get; set; }

        public SlimTopic()
        {
        }

        public SlimTopic(DomainClasses.Topic topic)
        {
            Id = topic.TopicId;
            Name = topic.Name;
            Slug = DiacriticsHelper.Remove(topic.Name);
            IsShown = topic.IsShown;
        }
    }
}