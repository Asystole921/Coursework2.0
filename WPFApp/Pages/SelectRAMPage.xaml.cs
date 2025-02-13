using Library.CustomEventArgs;
using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFApp.Pages
{
    public partial class SelectRAMPage : ObservableUserControl
    {
        public event EventHandler<SelectedRAMEventArgs>? RAMSelectedHandler;

        public ObservableCollection<RAM> RAMList { get; set; } = [];

        private RAM? _selectedRAM;

        public RAM? SelectedRAM
        {
            get => _selectedRAM;
            set
            {
                _selectedRAM = value;
                OnPropertyChanged(nameof(SelectedRAM));
            }
        }

        public ComponentsDBContext ComponentsDB { get; set; }

        public SelectRAMPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }
        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedRAM))
            {
                if (SelectedRAM != null && SelectedRAM.Id != -1)
                {
                    tbBrand.Text = SelectedRAM.Brand;
                    tbRAMType.Text = (from RamType in ComponentsDB.RAMTypes where RamType.Id == SelectedRAM.RAMTypeId select RamType).ToList().FirstOrDefault().Name;
                    tbFrequency.Text = SelectedRAM.Frequency.ToString();
                    tbCapacity.Text = SelectedRAM.Capacity.ToString();
                    tbModules.Text = SelectedRAM.Modules.ToString();
                    tbCASLatency.Text = SelectedRAM.CASLatency.ToString();
                    tbPrice.Text = SelectedRAM.Price.ToString();

                    string? img_src = SelectedRAM.ImgUrl;
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
            tbRAMType.Text = string.Empty;
            tbFrequency.Text = string.Empty;
            tbCapacity.Text = string.Empty;
            tbModules.Text = string.Empty;
            tbCASLatency.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }
        public void SetUp(int motherboardId = -1, int RamId = -1)
        {
            RAMList.Clear();
            RAM empty = new()
            {
                Id = -1,
                Name = "None"
            };
            RAMList.Add(empty);
            RAM sel = null;

            if (motherboardId == -1)
            {
                if (RamId == -1)
                    ListBoxRAM.SelectedItem = empty;

                foreach (RAM Ram in ComponentsDB.RAMs)
                {
                    RAMList.Add(Ram);
                    if (Ram.Id == RamId)
                        sel = Ram;
                }
            }
            else
            {
                if (RamId == -1)
                    ListBoxRAM.SelectedItem = empty;

                int RamTypeId = (from mb in ComponentsDB.Motherboards where mb.Id == motherboardId select mb.RAMTypeId).First();
                foreach (RAM Ram in (from Ram in ComponentsDB.RAMs where Ram.RAMTypeId == RamTypeId select Ram))
                {
                    RAMList.Add(Ram);
                    if (Ram.Id == RamId)
                        sel = Ram;
                }
            }

            if (sel != null)
            {
                SelectedRAM = sel;
            }
        }
        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            RAMSelectedHandler?.Invoke(this, new SelectedRAMEventArgs(SelectedRAM));
        }
    }
}
