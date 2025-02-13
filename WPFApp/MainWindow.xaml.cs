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

            SwapPages(SavedBuildsPage, NewBuildPage);
        }

        private void OnSavedBuildsPageReturnCommandExecuted(object? sender, EventArgs e)
        {
            SwapPages(SavedBuildsPage, MainMenuPage);
        }

        private void OnNewBuildBackToMainMenu(object? sender, EventArgs e)
        {
            SwapPages(NewBuildPage, MainMenuPage);
        }
        private void OnNewBuildBackToSavedBuilds(object? sender, EventArgs e)
        {
            SwapPages(NewBuildPage, SavedBuildsPage);
            SavedBuildsPage.SetUp();
        }

        private void OnNewComponentsPageClosed(object? sender, EventArgs e)
        {
            SwapPages(NewComponentPage, MainMenuPage);
        }

        private void OnSavedBuildsPageClick(object? sender, EventArgs e)
        {
            SwapPages(MainMenuPage, SavedBuildsPage);
            SavedBuildsPage.SetUp();
        }
        private void OnNewComponentPageClick(object? sender, EventArgs e)
        {
            SwapPages(MainMenuPage, NewComponentPage);
            NewComponentPage.SetUp();
        }

        private void OnNewBuildPageClick(object? sender, EventArgs e)
        {
            SwapPages(MainMenuPage, NewBuildPage);
            NewBuildPage.SetUp(new() { Id = -1 });
        }

        private void OnExitClick(object? sender, EventArgs e)
        {
            Close();
        }

        public static void SwapPages(UserControl currentPage, UserControl nextPage)
        {
            currentPage.Visibility = Visibility.Hidden;
            nextPage.Visibility = Visibility.Visible;
        }
    }
}