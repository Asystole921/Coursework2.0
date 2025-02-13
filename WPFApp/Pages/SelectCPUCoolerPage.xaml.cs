using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using Library.CustomEventArgs;

namespace WPFApp.Pages
{
    public partial class SelectCPUCoolerPage : ObservableUserControl
    {
        public event EventHandler<SelectedCCEventArgs>? CCSelectedHandler;

        public ObservableCollection<CPUCooler> CCList { get; set; } = [];

        public ComponentsDBContext ComponentsDB { get; set; }

        private CPUCooler _selectedCC = new() { Id = -1, Name = "None" };

        public CPUCooler SelectedCC
        {
            get => _selectedCC;
            set
            {
                _selectedCC = value;
                OnPropertyChanged(nameof(SelectedCC));
            }
        }

        public SelectCPUCoolerPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedCC))
            {
                if (SelectedCC != null && SelectedCC.Id != -1)
                {
                    tbBrand.Text = SelectedCC.Brand;
                    tbType.Text = SelectedCC.Type;
                    tbFanSpeed.Text = SelectedCC.FanSpeed.ToString();
                    tbNoiseLevel.Text = SelectedCC.NoiseLevel.ToString();
                    tbAirflow.Text = SelectedCC.NoiseLevel.ToString();
                    tbDimensions.Text = SelectedCC.Dimensions;
                    tbSoсket.Text = (from socket in ComponentsDB.Sockets where socket.Id == SelectedCC.SocketId select socket).ToList().FirstOrDefault().Name;
                    tbPrice.Text = SelectedCC.Price.ToString();
                    string? img_src = SelectedCC.ImgUrl;
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
            tbType.Text = string.Empty;
            tbFanSpeed.Text = string.Empty;
            tbNoiseLevel.Text = string.Empty;
            tbAirflow.Text = string.Empty;
            tbDimensions.Text = string.Empty;
            tbSoсket.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }

        public void SetUp(int motherboardId = -1, int CCId = -1)
        {
            CCList.Clear();
            CPUCooler empty = new()
            {
                Id = -1,
                Name = "None"
            };
            CCList.Add(empty);

            CPUCooler sel = null;

            if (motherboardId == -1)
            {
                if (CCId == -1)
                    ListBoxCC.SelectedItem = empty;

                foreach (CPUCooler cpuCooler in ComponentsDB.CPUCoolers)
                {
                    CCList.Add(cpuCooler);
                    if (cpuCooler.Id == CCId)
                        sel = cpuCooler;
                }
            }
            else
            {
                if (CCId == -1)
                    ListBoxCC.SelectedItem = empty;

                int socketId = (from mb in ComponentsDB.Motherboards where mb.Id == motherboardId select mb.SocketId).First();
                foreach (CPUCooler cpuCooler in (from cpuCooler in ComponentsDB.CPUCoolers where cpuCooler.SocketId == socketId select cpuCooler))
                {
                    CCList.Add(cpuCooler);
                    if (cpuCooler.Id == CCId)
                        sel = cpuCooler;
                }
            }

            if (sel != null)
            {
                SelectedCC = sel;
            }
        }

        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            CCSelectedHandler?.Invoke(this, new SelectedCCEventArgs(SelectedCC));
        }
    }
}
