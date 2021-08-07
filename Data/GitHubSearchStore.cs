using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using static repo_searching_uwp.Helper.HttpRequest;
using repo_searching_uwp.Model;

namespace repo_searching_uwp.Data
{
    public static class GitHubSearchStore
    {
        private static string baseUrl = "https://api.github.com/search/repositories";

        public async static Task<dynamic> SearchRepo(string keyword)
        {
            Debug.WriteLine("Sending Request ... " + keyword);

            string uri = baseUrl + "?q=" + keyword;

            Result result;
            ErrorResponse errorResponse;

            Windows.Web.Http.HttpResponseMessage response;
            response = await HttpGetRequest(uri);

            try
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(responseBody);
                    return result;
                }
                else
                {
                    errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponse>(responseBody);
                    return errorResponse;
                }
            }
            catch (Exception err)
            {
                return err;
            }
        }

    }
}
