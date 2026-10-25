using System.Web.Mvc;
using System.Web.Routing;

namespace LaboratorioCRUDAlumnos
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Alumno", action = "Index", id = UrlParameter.Optional });
        }
    }
}
