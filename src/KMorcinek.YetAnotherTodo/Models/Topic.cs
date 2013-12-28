using System.Collections.Generic;

namespace KMorcinek.YetAnotherTodo.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Note> Notes { get; set; }
    }
}