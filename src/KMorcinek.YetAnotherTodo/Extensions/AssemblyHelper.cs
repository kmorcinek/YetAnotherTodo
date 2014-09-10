using System.Diagnostics;
using System.Reflection;
namespace KMorcinek.YetAnotherTodo.Extensions
{
    public class AssemblyHelper
    {
        public static string GetProductVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}