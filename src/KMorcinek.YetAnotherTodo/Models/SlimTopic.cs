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

        public SlimTopic(Topic topic)
        {
            Id = topic.Id;
            Name = topic.Name;
            IsShown = topic.IsShown;
        }
    }
}