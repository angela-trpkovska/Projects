using System;
using System.Web;
using System.Web.Mvc;

namespace AmsmProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           filters.Add(new HandleErrorAttribute());
        
            //The AuthorizeAttribute indicates that all web service functionality including all web pages require authentication.
            filters.Add(new AuthorizeAttribute());

            filters.Add(new RequireHttpsAttribute());
        }
    }
}
