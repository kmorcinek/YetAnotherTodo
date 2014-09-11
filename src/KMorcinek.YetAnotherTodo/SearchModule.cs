using KMorcinek.YetAnotherTodo.Business;
using KMorcinek.YetAnotherTodo.Extensions;
using KMorcinek.YetAnotherTodo.ViewModels;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System.Linq;

namespace KMorcinek.YetAnotherTodo
{
    public class SearchModule : NancyModule
    {
        public SearchModule()
        {
            this.RequiresAuthentication();

            Get["/search/"] = parameters =>
            {
                SearchViewModel searchVM = this.Bind<SearchViewModel>();

                SearchService searchService = new SearchService();

                return View["Search", new
                {
                    SearchResultViewModels = searchService.GetTopicsWithSearchTerm(searchVM.SearchTerm).ToArray(),
                    ProductVersion = AssemblyHelper.GetProductVersion(),
                }];
            };
        }
    }
}