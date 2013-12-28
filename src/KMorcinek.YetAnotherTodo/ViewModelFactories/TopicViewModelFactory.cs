using System.Linq;
using KMorcinek.YetAnotherTodo.Models;
using KMorcinek.YetAnotherTodo.ViewModels;
using SisoDb.SqlCe4;

namespace KMorcinek.YetAnotherTodo.ViewModelFactories
{
    public class TopicViewModelFactory
    {
        public TopicViewModel Create(int id)
        {
            var db = DbRepository.GetDb();

            var topics = db.UseOnceTo().Query<Topic>().ToArray();
            var topicsViewModel = new TopicsViewModel
            {
                Topics = topics,
            };

            var topicViewModel = new TopicViewModel
            {
                Topic = topics.Single(t => t.Id == id),
                TopicsViewModel = topicsViewModel,
            };

            return topicViewModel;
        }
    }
}