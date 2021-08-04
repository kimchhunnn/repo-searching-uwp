using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Popups;

namespace repo_searching_uwp.Helper
{
    public static class Dialog
    {
        public static async void ShowErrorDialog(string title, string message)
        {
            var dialog = new MessageDialog(message, title);
            dialog.Commands.Add(new UICommand("Ok"));
            await dialog.ShowAsync();
        }
    }
}
