using System.Diagnostics;

namespace KMorcinek.YetAnotherTodo.Models
{
    [DebuggerDisplay("{Content}")]
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}