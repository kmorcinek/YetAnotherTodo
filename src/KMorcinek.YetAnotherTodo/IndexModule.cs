using System.Web;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.Security;

namespace KMorcinek.YetAnotherTodo
{
    public class IndexModule : NancyModule
    {
        dynamic IndexPage()
        {
            var db = DbRepository.GetDb();
            var firstTopic = db.UseOnceTo().Query<Topic>().OrderBy(t => t.Id).First();

            if (firstTopic == null)
                throw new HttpException("No topics created yet.");

            return View["YetAnotherTodo"];
        }

        public IndexModule()
        {
            this.RequiresAuthentication();

            Get["/"] = _ => { return IndexPage(); };
            Get["/(.*)"] = _ => { return IndexPage(); };
            Get["/(.*)/(.*)"] = _ => { return IndexPage(); };
        }
    }
}