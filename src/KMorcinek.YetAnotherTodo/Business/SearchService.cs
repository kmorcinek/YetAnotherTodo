using KMorcinek.YetAnotherTodo.DataLayer;
using System.Collections.Generic;
using KMorcinek.YetAnotherTodo.ViewModels;

namespace KMorcinek.YetAnotherTodo.Business
{
    public class SearchService
    {
        public IEnumerable<SearchResultViewModel> GetTopicsWithSearchTerm(string searchTerm)
        {
            string upperInvariantSearchTerm = searchTerm.ToUpperInvariant();

            using (var todoModelContext = new TodoModelContext())
            {
                var topics = todoModelContext.Topics;

                foreach (var topic in topics)
                {
                    foreach (var note in topic.Notes)
                    {
                        if (note.Content.ToUpperInvariant().Contains(upperInvariantSearchTerm))
                        {
                            yield return new SearchResultViewModel
                            {
                                Content = note.Content,
                                TopicName = topic.Name,
                                TopicId = topic.TopicId,
                            };
                        }
                    }
                } 
            }
        }
    }
}