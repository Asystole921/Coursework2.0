using Library.CustomEventArgs;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using Library.Models;

namespace WPFApp.Pages
{
    public partial class SelectStoragePage : ObservableUserControl
    {
        public event EventHandler<SelectedStorageEventArgs> StorageSelectedHandler;

        public ObservableCollection<Storage> StorageList { get; set; } = [];

        private Storage? _selectedStorage;

        public Storage SelectedStorage
        {
            get => _selectedStorage;
            set
            {
                _selectedStorage = value;
                OnPropertyChanged(nameof(SelectedStorage));
            }
        }
        public ComponentsDBContext ComponentsDB { get; set; }

        public SelectStoragePage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedStorage))
            {
                if (SelectedStorage != null && SelectedStorage.Id != -1)
                {
                    tbBrand.Text = SelectedStorage.Brand;
                    tbCapacity.Text = SelectedStorage.Capacity;
                    tbInterface.Text = SelectedStorage.Interface;
                    tbFormFactor.Text = SelectedStorage.FormFactor;
                    tbReedSpeed.Text = SelectedStorage.ReadSpeed.ToString();
                    tbWriteSpeed.Text = SelectedStorage.WriteSpeed.ToString();
                    tbPrice.Text = SelectedStorage.Price.ToString();

                    string? img_src = SelectedStorage.ImgUrl;
                    if (img_src != null)
                    {
                        ImgUrl.Source = new BitmapImage(new Uri(img_src));
                        ImgUrl.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ImgUrl.Visibility = Visibility.Hidden;
                    }

                }
                else
                {
                    ClearFields();
                }

            }

        }
        private void ClearFields()
        {
            tbBrand.Text = string.Empty;
            tbCapacity.Text = string.Empty;
            tbInterface.Text = string.Empty;
            tbFormFactor.Text = string.Empty;
            tbReedSpeed.Text = string.Empty;
            tbWriteSpeed.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }
        public void SetUp(int StorageId = -1)
        {
            StorageList.Clear();
            Storage empty = new() { Id = -1, Name = "None" };
            StorageList.Add(empty);
            foreach (Storage storage in ComponentsDB.Storages.ToList())
            {
                StorageList.Add(storage);
            }
            Storage? sel = StorageList.FirstOrDefault(storage => storage.Id == StorageId);
            if (sel != null)
            {
                ListBoxStorage.SelectedItem = sel;
            }
            else
            {
                ListBoxStorage.SelectedItem = empty;
            }
        }
        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {

            StorageSelectedHandler?.Invoke(this, new SelectedStorageEventArgs(SelectedStorage));

        }

    }
}
