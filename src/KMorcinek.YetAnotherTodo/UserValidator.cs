using System;
using System.IO;
using System.Threading;
using System.Web;
using Nancy.Authentication.Basic;
using Nancy.Security;

namespace KMorcinek.YetAnotherTodo
{
    public class UserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            var lines = File.ReadAllLines(
                Path.Combine(
                HttpContext.Current.Request.PhysicalApplicationPath, 
                "App_Data/user.txt"));

            Thread.Sleep(TimeSpan.FromSeconds(2));

            if (username == lines[0] && password ==  lines[1])
            {
                return new DemoUserIdentity { UserName = username };
            }

            // Not recognised => anonymous.
            return null;
        }
    }
}
