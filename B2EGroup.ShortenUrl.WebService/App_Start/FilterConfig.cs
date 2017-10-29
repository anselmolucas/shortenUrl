using System.Web;
using System.Web.Mvc;

namespace B2EGroup.ShortenUrl.WebService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
