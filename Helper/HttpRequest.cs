using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace repo_searching_uwp.Helper
{
    public static class HttpRequest
    {
        public static async Task<Windows.Web.Http.HttpResponseMessage> HttpGetRequest(string uri)
        {

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            var headers = httpClient.DefaultRequestHeaders;
            headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            Uri requestUri = new Uri(uri);

            Windows.Web.Http.HttpResponseMessage httpResponse;

            //Send the GET request
            httpResponse = await httpClient.GetAsync(requestUri);

            return httpResponse;
        }
    }
}
