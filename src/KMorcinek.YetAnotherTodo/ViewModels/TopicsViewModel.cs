using System.Collections.Generic;
using KMorcinek.YetAnotherTodo.Models;

namespace KMorcinek.YetAnotherTodo.ViewModels
{
    public class TopicsViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
    }
}