using System.Web;
using KMorcinek.YetAnotherTodo.Models;
using Nancy;
using Nancy.Security;
using KMorcinek.YetAnotherTodo.Extensions;

namespace KMorcinek.YetAnotherTodo
{
    public class IndexModule : NancyModule
    {
        dynamic IndexPage()
        {
            return View["YetAnotherTodo", new
            {
                ProductVersion = AssemblyHelper.GetProductVersion(),
            }];
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