using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;
using HttpStatusCode = Grapevine.Shared.HttpStatusCode;

namespace UserAdmin
{



    class Program
    {
        static void Main(string[] args)
        {
            using (var server = new RestServer())
            {
                server.PublicFolder = new PublicFolder(@".\UserAdmin\pages") { Prefix = "UserAdmin" };

                server.LogToConsole().Start();
                Console.ReadLine();
                server.Stop();
            }
        }
    }

    [RestResource]
    public class UserAdministrationResource
    {

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/login")]
        public IHttpContext LoginMethod(IHttpContext context)
        {
            var username = context.Request.QueryString["username"];
            var password = context.Request.QueryString["password"];

            Console.WriteLine("RequestContext   : username = " + username);
            Console.WriteLine("RequestContext   : password = " + password);
            // Check username and password against DB

            context.Response.SendResponse(HttpStatusCode.Ok);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/changename")]
        public IHttpContext ChangeNameMethod(IHttpContext context)
        {
            bool userexists = true;
            // Update name in user database

            // If user does not exist return 404
            if (!userexists)
            {
                context.Response.SendResponse((HttpStatusCode.Accepted));
                return context;
            }

            // Success
            context.Response.SendResponse((HttpStatusCode.Accepted));
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/adduser")]
        public IHttpContext AddUserMethod(IHttpContext context)
        {
            bool userexists = false;
            // check username against database


            //insert user in database


            // If user already exists return 403 forbidden
            if (userexists)
            {
                context.Response.SendResponse((HttpStatusCode.Forbidden));
                return context;
            }


            // Success
            context.Response.SendResponse((HttpStatusCode.Accepted));
            return context;
        }
    }
}
