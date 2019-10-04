using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ManageShop.ApiHelper
{
    public static class ApiHelper
    {

        public static async Task<HttpResponseMessage> CallApi(string url,string data)
        {
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            return await client.PostAsync(url, content);
        }
    }
}