using CommunityToolkit.Mvvm.Input;
using Library.CustomEventArgs;
using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.Pages
{
    public partial class SavedBuildsPage : ObservableUserControl
    {
        #region EditCommand

        public ICommand EditCommand { get; set; }

        public event EventHandler<SelectedBuildEventArgs>? EditCommandExecuted;

        private void EditCommandExecute()
        {
            EditCommandExecuted?.Invoke(this, new SelectedBuildEventArgs(SelectedBuild ?? throw new Exception("Null Build selected")));
        }

        private bool EditCommandCanExecute()
        {
            return SelectedBuild != null;
        }

        #endregion

        #region DeleteCommand

        public ICommand DeleteCommand { get; set; }

        private void DeleteCommandExecute()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                ComponentsDB.Builds.Remove(SelectedBuild);
                ComponentsDB.SaveChanges();
                BuildList.Remove(SelectedBuild);
                SelectedBuild = null;
            }
        }

        private bool DeleteCommandCanExecute()
        {
            return SelectedBuild != null;
        }

        #endregion

        #region ReturnCommand

        public ICommand ReturnCommand { get; set; }

        public event EventHandler? ReturnCommandExecuted;

        private void ReturnCommandExecute()
        {
            ReturnCommandExecuted?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public ObservableCollection<Build> BuildList { get; set; } = [];

        private Build? _selectedBuild = null;

        public Build? SelectedBuild
        {
            get => _selectedBuild;
            set
            {
                _selectedBuild = value;
                OnPropertyChanged(nameof(SelectedBuild));
            }
        }

        public ComponentsDBContext ComponentsDB { get; set; }

        public SavedBuildsPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();

            EditCommand = new RelayCommand(EditCommandExecute, EditCommandCanExecute);
            DeleteCommand = new RelayCommand(DeleteCommandExecute, DeleteCommandCanExecute);
            ReturnCommand = new RelayCommand(ReturnCommandExecute);

            InitializeComponent();
        }

        public void SetUp()
        {
            BuildList.Clear();
            foreach (Build build in ComponentsDB.Builds)
            {
                BuildList.Add(build);
            }
        }
    }
}
