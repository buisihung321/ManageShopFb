using ManageShop.DAL;
using ManageShop.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ManageShop.Controllers.api
{
    public class WebHookController : ApiController
    {
        const string _pageToken = "EAAEk1ybcCooBAOlgyJWSY8ookpW6Lv3FlvqvTZCnS7ZAWFA723LdAyVCjuZBoxOCUeKjahOBWPqUlvzsCM3gfwesEZAtB7PJh2jeaHqTLzHv6BIYsIh758l50ZCznemeJGMzg7Wg7z6Y38ExwC6YCCna7sLZANSAMsEmyvnkhF4gZDZD";
        private ManageShopContext _context;

        public WebHookController()
        {
            _context = new ManageShopContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //FOR Fb Verifycation
        public IHttpActionResult Get()
        {
            NameValueCollection s = HttpContext.Current.Request.QueryString;
            string mode = s.Get("hub.mode");
            if (mode.Equals("subscribe"))
            {
                int challenge = int.Parse(s.Get("hub.challenge"));
                return Ok(challenge);


            }
            return Content(HttpStatusCode.Forbidden, "Cannot Verify.");
        }


        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {

            string str = await Request.Content.ReadAsStringAsync();
            JObject body = JObject.Parse(str);
            //string object = body["object"].ToString();
            // Make sure this is a page subscription
            if (body["object"].ToString().Equals("page"))
            {
                var entries = body["entry"];
                // Iterate over each entry
                // There may be multiple if batched
                foreach (var entry in entries)
                {
                    var messages = entry["messaging"];
                    // Iterate over each messaging event
                    foreach (var messageEvent in messages)
                    {
                        if (messageEvent["message"] != null)
                        {
                            //handle message event
                            if (!await handleMessageEvent(messageEvent))
                                return InternalServerError();
                        }
                        else if (messageEvent["postback"] != null)
                        {
                            //handle pstback event
                        }
                        else
                        {
                            //Not support envent
                        }

                    }

                }
            }


            //var test = body["entry"][0]["messaging"][0]["message"];


            //if (test != null)
            //{

            //    string custId = (string)body["entry"][0]["messaging"][0]["sender"]["id"];
            //    string msg = (string)test["text"];
            //    string responseMsg;
            //    //1. process msg
            //    if (msg.ToLower().Contains("hi") || msg.ToLower().Contains("hello"))
            //    {
            //        responseMsg = "Hello " + custId + ".What can i help you?";
            //    }
            //    else
            //    {
            //        responseMsg = "Sorry, i don't understand it. Would you like to buy something?";
            //    }

            //    //2. send response msg

            //    if (await ReplyMessage(_pageToken, responseMsg, custId))
            //    {
            //        return Ok();
            //    }
            //    else
            //    {
            //        //log error
            //        return InternalServerError();//500
            //    }

            //    MessageWebhookLog mess = new MessageWebhookLog { MessageText = (string)msg, Timestamp = (string)body["entry"][0]["messaging"][0]["timestamp"] };
            //    _context.MessageWebhookLogs.Add(mess);
            //    _context.SaveChanges();
            //}

            return Ok();
        }

        private async Task<bool> handleMessageEvent(JToken messageEvent)
        {
            var senderID = messageEvent["sender"]["id"];//Cust PSID
            var recipientID = messageEvent["recipient"]["id"];//this is page PSID
            var timeOfMessage = messageEvent["timestamp"];
            string message = messageEvent["message"].ToString();
            //var nlp;

            string responseMsg = null;

            //1. Process msg to get the reply msg
            if (message.ToLower().Contains("hi") || message.ToLower().Contains("hello"))
            {
                responseMsg = "Hello " + senderID + ".What can i help you?";
            }
            else
            {
                responseMsg = "Sorry, i don't understand it. Would you like to buy something?";
            }
            //2. Call Send Txt Msg
            return await SendTextMessage(senderID.ToString(), responseMsg);

    }

        private async Task<bool> SendTextMessage(string recipientId,string msg)
        {
            JObject data = JObject.FromObject(new
            {
                messaging_type = "RESPONSE",
                recipient = new
                {
                    id = recipientId
                },
                message = new
                {
                    text = msg
                }
            });

            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            return await CallSendAPI(content);
        }

        private async Task<bool> CallSendAPI(StringContent content)
        {

            HttpClient client = new HttpClient();
            var result = await client.PostAsync($"https://graph.facebook.com/v4.0/me/messages?access_token={_pageToken}", content);

            if (!result.IsSuccessStatusCode)
                return false;
            return true;

        }

        private static async Task<bool> ReplyMessage(string token, string msg, string custId)
    {
        HttpClient client = new HttpClient();

        JObject data = JObject.FromObject(new
        {
            messaging_type = "RESPONSE",
            recipient = new
            {
                id = custId
            },
            message = new
            {
                text = msg
            }
        });
        var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
        var result = await client.PostAsync($"https://graph.facebook.com/v4.0/me/messages?access_token={token}", content);
        if (!result.IsSuccessStatusCode)
            return false;
        return true;
    }

}
}
