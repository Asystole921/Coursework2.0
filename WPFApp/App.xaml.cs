using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;

            MainWindow = new MainWindow();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
