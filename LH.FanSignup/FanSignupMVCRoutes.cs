using DotNetNuke.Web.Mvc.Routing;

namespace IMT.LH.FanSignup
{

    public partial class RouteConfig : IMvcRouteMapper
    {
        public void RegisterRoutes(IMapRoute  mapRouteManager)
        {
            mapRouteManager.MapRoute(
                "LH.FanSignup",
                "FanSignupActions",
                "FanSignup/{action}",
                defaults: new
                {
                    controller = "FanSignup",action="GetSignup",route="FanSignupActions"
                },
                namespaces: new[]
                {
                    "IMT.LH.FanSignup.Controllers"
                });

            mapRouteManager.MapRoute(
                "LH.FanSignup",
                "FanSignupActions",
                "FanSignup/{action}",
                defaults: new
                {
                    controller = "FanSignup",action="SaveSignup",route="FanSignupActions"
                },
                namespaces: new[]
                {
                    "IMT.LH.FanSignup.Controllers"
                });


        }

        
    }
    
}