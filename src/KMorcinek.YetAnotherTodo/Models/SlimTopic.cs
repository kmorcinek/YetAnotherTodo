namespace KMorcinek.YetAnotherTodo.Models
{
    public class SlimTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsShown { get; set; }

        public SlimTopic()
        {
            
        }

        public SlimTopic(DomainClasses.Topic topic)
        {
            Id = topic.TopicId;
            Name = topic.Name;
            IsShown = topic.IsShown;
        }
    }
}