using System.Web;
using System.Web.Mvc;

namespace caece_proyecto1_escuelas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}