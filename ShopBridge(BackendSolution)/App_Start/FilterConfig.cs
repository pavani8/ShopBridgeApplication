using System.Web;
using System.Web.Mvc;

namespace ShopBridge_BackendSolution_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
