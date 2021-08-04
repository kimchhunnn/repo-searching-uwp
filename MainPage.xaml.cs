using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Newtonsoft.Json;

namespace repo_searching_uwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            TextBlock emptyMessageTextBLock = this.FindName("EmptyMsg") as TextBlock;
            emptyMessageTextBLock.Visibility = Visibility.Collapsed;

        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Sending Request ... ");

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            var headers = httpClient.DefaultRequestHeaders;
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            Uri requestUri = new Uri("https://api.github.com/search/repositories?q=" + this.Keyword.Text);

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(httpResponseBody);

                this.ResultMsg.Text = result.Items.Count() + " Results";
                this.ResultList.ItemsSource = result.Items;

            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

        }
    }

    public class Repo
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class Result
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public Repo[] Items { get; set; }
    }
}
