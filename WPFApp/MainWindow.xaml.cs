using Library.CustomEventArgs;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WPFApp.Pages;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        const string currencySite = "https://finance.i.ua//";

        public ListBox ListBoxComponents { get; private set; } = new() { };

        public MainWindow()
        {
            InitializeComponent();

            MainMenuPage.NewBuildPageClick += OnNewBuildPageClick;
            MainMenuPage.SavedBuildsPageClick += OnSavedBuildsPageClick;
            MainMenuPage.NewBuildPageClick += OnNewBuildPageClick;
            MainMenuPage.NewComponentClick += OnNewComponentPageClick;
            MainMenuPage.ExitClick += OnExitClick;

            NewBuildPage.BackToMainMenu += OnNewBuildBackToMainMenu;
            NewBuildPage.BackToSavedBuilds += OnNewBuildBackToSavedBuilds;

            SavedBuildsPage.ReturnCommandExecuted += OnSavedBuildsPageReturnCommandExecuted;
            SavedBuildsPage.EditCommandExecuted += OnSavedBuildsPageEditCommandExecuted;

            NewComponentPage.ReturnCommandExecuted += OnNewComponentsPageClosed;


            using WebClient web = new();
            string pageSource = web.DownloadString(currencySite);
            Match match = Regex.Match(pageSource, @"<tr><th>USD<\/th><td><span class=""value -decrease""><span>([0-9.]*)<\/span>");
            if (match.Success)
            {
                NewBuildPage.USD_UAH = double.Parse(match.Groups[1].Value);
            }
            else
            {
                //throw new Exception("Failed to load Currency");
            }
        }

        private void OnSavedBuildsPageEditCommandExecuted(object? sender, SelectedBuildEventArgs e)
        {
            NewBuildPage.SetUp(e.Build);

            SavedBuildsPage.Visibility = Visibility.Hidden;
            NewBuildPage.Visibility = Visibility.Visible;
        }

        private void OnSavedBuildsPageReturnCommandExecuted(object? sender, EventArgs e)
        {
            SavedBuildsPage.Visibility = Visibility.Hidden;
            MainMenuPage.Visibility = Visibility.Visible;
        }

        private void OnNewBuildBackToMainMenu(object? sender, EventArgs e)
        {
            NewBuildPage.Visibility = Visibility.Hidden;
            MainMenuPage.Visibility = Visibility.Visible;
        }
        private void OnNewBuildBackToSavedBuilds(object? sender, EventArgs e)
        {
            NewBuildPage.Visibility = Visibility.Hidden;
            SavedBuildsPage.Visibility = Visibility.Visible;
            SavedBuildsPage.SetUp();
        }

        private void OnNewComponentsPageClosed(object? sender, EventArgs e)
        {
            NewComponentPage.Visibility = Visibility.Hidden;
            MainMenuPage.Visibility = Visibility.Visible;
        }

        private void OnSavedBuildsPageClick(object? sender, EventArgs e)
        {
            MainMenuPage.Visibility = Visibility.Hidden;
            SavedBuildsPage.Visibility = Visibility.Visible;
            SavedBuildsPage.SetUp();
        }
        private void OnNewComponentPageClick(object? sender, EventArgs e)
        {
            MainMenuPage.Visibility = Visibility.Hidden;
            NewComponentPage.Visibility = Visibility.Visible;
            NewComponentPage.SetUp();
        }

        private void OnNewBuildPageClick(object? sender, EventArgs e)
        {
            MainMenuPage.Visibility = Visibility.Hidden;
            NewBuildPage.Visibility = Visibility.Visible;
            NewBuildPage.SetUp(new() { Id = -1 });
        }
        private void OnExitClick(object? sender, EventArgs e)
        {
            Close();
        }
    }
}