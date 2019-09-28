using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ManageShop.Controllers.api
{
    public class WebHookController : ApiController
    {

        public IHttpActionResult Get()
        {
            NameValueCollection s =  HttpContext.Current.Request.QueryString;
            int res = int.Parse(s.Get("hub.challenge"));
            return Ok(res);
        }


        public IHttpActionResult Post()
        {
            return Ok();
        }
        public IHttpActionResult Get(string hub_mode,int hub_challenge, string hub_verify_token)
        {
            return Ok(hub_challenge);
        }
        public IHttpActionResult Post(string hub_mode, int hub_challenge, string hub_verify_token)
        {
            return Ok();
        }

    }
}
