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
using System.Threading.Tasks;
using Windows.UI.Popups;

using static repo_searching_uwp.Helper.HttpRequest;
using static repo_searching_uwp.Helper.Dialog;
using repo_searching_uwp.Model;

namespace repo_searching_uwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Sending Request ... ");

            string uri = "";
            if (this.Keyword.Text.Length > 0)
            {
                string searchKeyword = this.Keyword.Text.Contains('%') ? this.Keyword.Text.Replace("%", "%25") : this.Keyword.Text; // Replace % with unicode
                uri = "https://api.github.com/search/repositories?q=" + searchKeyword;
            }
            else
            {
                this.ResultList.ItemsSource = new List<Repo>();
                this.ResultMsg.Text = "0 Result";
                return;
            }


            try
            {
                Windows.Web.Http.HttpResponseMessage response;
                response = await HttpGetRequest(uri);
                if (response.Content == null)
                {
                    throw new Exception();
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(responseBody);

                    Debug.WriteLine(result);

                    this.ResultMsg.Text = result.Items.Count() + " Results";
                    this.ResultList.ItemsSource = result.Items;

                    if (result.Items.Count().Equals(0))
                    {
                        this.EmptyMsg.Visibility = Visibility.Visible;
                        this.ResultList.Visibility = Visibility.Collapsed;
                    }
                    else if (!result.Items.Count().Equals(0) && result.Items.Count() < result.TotalCount)
                    {
                        this.EmptyMsg.Visibility = Visibility.Collapsed;
                        this.ResultList.Visibility = Visibility.Visible;
                        this.ResultList.ItemsSource = result.Items;
                        this.ResultMsg.Text = "Top " + result.Items.Count() + " Results out of " + result.TotalCount + " Results";
                    }
                    else
                    {
                        this.EmptyMsg.Visibility = Visibility.Collapsed;
                        this.ResultList.Visibility = Visibility.Visible;
                        this.ResultList.ItemsSource = result.Items;
                        this.ResultMsg.Text = result.Items.Count() + " Results";
                    }

                } else
                {
                    ErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponse>(responseBody);

                    string errMsg = errorResponse.Errors[0].Message;
                    string title = errorResponse.Message;

                    ShowErrorDialog(title, errMsg);
                }
            }
            catch (Exception err)
            {
                ShowErrorDialog("Technical Issue", err.Message);
            }
        }
    }
}
