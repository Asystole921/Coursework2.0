using System.Windows;
using System.Windows.Controls;

namespace WPFApp.Pages
{
    public partial class MainMenuPage : UserControl
    {
        public event EventHandler? SavedBuildsPageClick;
        public event EventHandler? NewBuildPageClick;
        public event EventHandler? NewComponentClick;
        public event EventHandler? ExitClick;

        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void NBButton_Click(object sender, RoutedEventArgs e)
        {
            NewBuildPageClick?.Invoke(this, new EventArgs());
        }
        private void SBButton_Click(object sender, RoutedEventArgs e)
        {
            SavedBuildsPageClick?.Invoke(this, new EventArgs());
        }

        private void NPButton_Click(object sender, RoutedEventArgs e)
        {
            NewComponentClick?.Invoke(this, new EventArgs());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitClick?.Invoke(this, new EventArgs());
        }
    }
}
