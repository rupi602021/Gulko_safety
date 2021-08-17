using System.Web;
using System.Web.Mvc;

namespace final_proj_gulkosafety
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
