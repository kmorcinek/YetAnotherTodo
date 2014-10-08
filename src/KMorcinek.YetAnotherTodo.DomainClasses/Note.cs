namespace KMorcinek.YetAnotherTodo.DomainClasses
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }
    }
}