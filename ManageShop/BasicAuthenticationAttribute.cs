using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using ManageShop.Controllers;

namespace ManageShop
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute,IAuthenticationFilter
    {
       

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //perform check for authentication



            ////action Context give us to access req and res
            //if (actionContext.Request.Headers.Authorization == null)
            //{
            //    //user not authorize
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //}
            //else
            //{
            //    //Check valid username & password
            //    string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
            //    //the structure will be <username>:<password> and in Base64 encoded
            //    //1.Decode the token to get the actual string 
            //    string decodedAuthentication = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            //    string[] usernamePasswordArr = decodedAuthentication.Split(':');

            //    string username = usernamePasswordArr[0];
            //    string password = usernamePasswordArr[1];

            //    if (AccountSecurity.Login(username, password))
            //    {
            //        //after set current princeple we can access this user from controller
            //        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
            //    }
            //    else
            //    {
            //        actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            //    }

            //}

            throw new NotImplementedException();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //execute after OnAuthentication 

            throw new NotImplementedException();
        }
    }
}