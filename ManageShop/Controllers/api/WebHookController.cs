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

        //FOR Fb Verification
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
                            if (!await HandleMessageEvent(messageEvent))
                                return InternalServerError();
                        }
                        else if (messageEvent["postback"] != null)
                        {
                            //handle pstback event
                            if (!await HandlePostbackEvent(messageEvent))
                                return InternalServerError();


                        }
                        else
                        {
                            //Not support envent
                        }

                    }

                }
            }
            return Ok();
        }


        private bool CheckEntity(JToken nlp, string name)
        {
            return nlp?["entities"]?[name]?[0] != null;
        }

        private async Task<bool> HandleMessageEvent(JToken messageEvent)
        {
            var senderID = messageEvent["sender"]["id"];//Cust PSID
            var recipientID = messageEvent["recipient"]["id"];//this is page PSID
            var timeOfMessage = messageEvent["timestamp"];
            string message = messageEvent["message"]["text"].ToString();
            var nlp = messageEvent["message"]["nlp"];
            string responseMsg = null;

            //1. Process msg to get the reply msg
            //Check for ask_price and ask_is_available
            if (CheckEntity(nlp,"intent"))
            {

                
                var intent = nlp["entities"]["intent"][0]["value"];

                //handle intent
                switch (intent.ToString())
                {
                    case "ask_price":
                        //check whether wit.ai detect productId
                        var productId = nlp["entities"]["productId"]?[0]?["value"];
                        if (productId != null)
                        {
                            //get product info from DB
                            var product = _context.Products.Find(int.Parse(productId.ToString()));
                            if (product == null)
                                responseMsg = "Not found product";
                            else
                                responseMsg = $"Product {product.Id} has price {product.Price} thousand-dong";
                            //responseMsg = $"You ask price of product {productId}";
                        }
                        else
                        {
                            responseMsg = "Sorry we can't get product ID, Please enter the productID.";
                        }

                        break;
                    default: break;
                }
                //var productType = nlp["entities"]["productType"];
            }
            else
            {
                responseMsg = $"Hello, You have sent {message}";

            }



            //2. Call Send Txt Msg
            return await SendTextMessage(senderID.ToString(), responseMsg);

        }

        private async Task<bool> HandlePostbackEvent(JToken messageEvent)
        {
            var senderID = messageEvent["sender"]["id"];//Cust PSID
            var recipientID = messageEvent["recipient"]["id"];//this is page PSID
            var timeOfMessage = messageEvent["timestamp"];
            string payload = messageEvent["postback"]["payload"].ToString();
            //var nlp;

            //payload is setting by USER
            //1. Process msg to get the reply msg
            switch (payload)
            {
                case "get_start_payload":
                    return await SendTextMessage(senderID.ToString(), "This is get_started response postback");
                default: break;
            }
            //2. Call Send Txt Msg
            return await SendTextMessage(senderID.ToString(), "Cannot understand payback");
        }

        private async Task<bool> SendTextMessage(string recipientId, string msg)
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
