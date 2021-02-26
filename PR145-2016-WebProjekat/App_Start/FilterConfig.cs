using System.Web;
using System.Web.Mvc;

namespace PR145_2016_WebProjekat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
