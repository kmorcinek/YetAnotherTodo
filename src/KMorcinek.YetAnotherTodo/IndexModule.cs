using System.Web;
using KMorcinek.YetAnotherTodo.Models;
using KMorcinek.YetAnotherTodo.ViewModelFactories;
using Nancy.Security;

namespace KMorcinek.YetAnotherTodo
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            this.RequiresAuthentication();

            Get["/"] = _ =>
            {
                var db = DbRepository.GetDb();
                var firstTopic = db.UseOnceTo().Query<Topic>().OrderBy(t => t.Id).First();

                if (firstTopic == null)
                    throw new HttpException("No topics created yet.");

                return GetView(firstTopic.Id);
            };

            Get["/{name}/{id}"] = parameters =>
            {
                var id = int.Parse(parameters.id.Value);
                return GetView(id);
            };

            Get["/admin"] = _ =>
            {
                return View["Admin"];
            };
        }

        private dynamic GetView(int id)
        {
            var factory = new TopicViewModelFactory();
            var viewModel = factory.Create(id);
            return View["index", viewModel];
        }
    }
}