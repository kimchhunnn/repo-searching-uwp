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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace repo_searching_uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public Repository[] Repositories { get; } =
            new Repository[]
        {
            ("Apricot", "AAAAAAA", "React Calendar", 1622, "JavaScript"),
            ("Banana", "BBBBBB", "React Calendar", 2000, "JavaScript"),
            ("Cherry", "CCCCCC", "React Calendar", 2099, "JavaScript"),
            ("Date Palm", "DDDDDD", "React Calendar", 1299, "JavaScript"),
            ("Elephant Apple", "EEEEEE", "React Calendar", 210, "JavaScript"),
            ("Elephant Apple", "EEEEEE", "React Calendar", 30, "JavaScript"),
        };
    }

    public class Repository
    {
        public string Name { get; set; }
        public string HtmlUrl { get; set; }
        public string Description { get; set; }
        public int Stargazers { get; set; }
        public string Language { get; set; }



        public static implicit operator Repository((string Name, string HtmlUrl, string Description, int Stargazers, string Language) info)
        {
            return new Repository { Name = info.Name, HtmlUrl = info.HtmlUrl, Description = info.Description, Stargazers = info.Stargazers, Language = info.Language };
        }
    }
}
