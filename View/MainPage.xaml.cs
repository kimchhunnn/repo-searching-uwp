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
using static repo_searching_uwp.Data.GitHubSearchStore;

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

            string uri;
            if (this.Keyword.Text.Length > 0)
            {
                uri = this.Keyword.Text.Contains('%') ? this.Keyword.Text.Replace("%", "%25") : this.Keyword.Text; // Replace % with unicode
            }
            else
            {
                this.ResultList.ItemsSource = new List<Repo>();
                this.ResultMsg.Text = "0 Result";
                return;
            }

            try
            {
                object response = await SearchRepo(uri);

                if ( response.GetType().Equals(typeof(Result)) )
                {
                    Result result = (Result)response;

                    if (result.Items.Count().Equals(0))
                    {
                        this.EmptyMsg.Visibility = Visibility.Visible;
                        this.ResultList.Visibility = Visibility.Collapsed;
                        this.ResultMsg.Text = result.Items.Count() + " Result";
                        this.ResultList.ItemsSource = result.Items;
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

                } else if ( response.GetType().Equals(typeof(ErrorResponse)))
                {
                    ErrorResponse errorResponse = (ErrorResponse)response;

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
