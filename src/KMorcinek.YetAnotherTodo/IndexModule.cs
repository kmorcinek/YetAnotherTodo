using KMorcinek.YetAnotherTodo.ViewModelFactories;

namespace KMorcinek.YetAnotherTodo
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        private const int DefaultTopicId = 1;

        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return GetView(DefaultTopicId);
            };

            Get["/{name}/{id}"] = parameters =>
            {
                var id = int.Parse(parameters.id.Value);
                return GetView(id);
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