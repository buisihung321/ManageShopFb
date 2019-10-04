using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageShop.Models
{
    public class MessageWebhookLog
    {
        public int id { get; set; }
        public string MessageText { get; set; }
        public string Timestamp { get; set; }
    }
}