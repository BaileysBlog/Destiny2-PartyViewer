using D2DataAccess.Data;
using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace PartyWebApi
{
    public static class WebApiConfig
    {
        public static Destiny2Api _DestinyApi { get; private set; }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            var ApiKey = Properties.Settings.Default.ApiKey;

            _DestinyApi = new Destiny2Api(ApiKey,
                new UserAgentHeader("Destiny 2 Party Viewer", 
                Assembly.GetExecutingAssembly().GetName().Version.ToString(), 
                "Web API", 1, "https://www.d2-partyviewer.com", "baileymiller@live.com")
                , new DirectoryInfo(HttpContext.Current.Server.MapPath("~")));

            _DestinyApi.UpdateDatabaseIfRequired();
        }
    }
}
