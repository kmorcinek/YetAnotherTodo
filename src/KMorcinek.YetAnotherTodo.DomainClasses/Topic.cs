using System.Collections.Generic;

namespace KMorcinek.YetAnotherTodo.DomainClasses
{
    public class Topic
    {
        private ICollection<Note> _notes;

        public Topic()
        {
            _notes = new List<Note>();
        }

        public int TopicId { get; set; }
        public string Name { get; set; }
        public bool IsShown { get; set; }

        public virtual ICollection<Note> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
    }
}