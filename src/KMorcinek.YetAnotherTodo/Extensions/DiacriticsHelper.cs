using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMorcinek.YetAnotherTodo.Extensions
{
    public class DiacriticsHelper
    {
        public static string Remove(string content)
        {
            return content.Replace("ó", "o").Replace("ż", "z");
        }
    }
}