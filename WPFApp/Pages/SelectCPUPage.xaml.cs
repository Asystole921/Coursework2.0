using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using Library.CustomEventArgs;

namespace WPFApp.Pages
{
    public partial class SelectCPUPage : ObservableUserControl
    {
        public event EventHandler<SelectedCPUEventArgs>? CPUSelectedHandler;

        public ObservableCollection<CPU> CPUList { get; set; } = [];

        public ComponentsDBContext ComponentsDB { get; set; }

        private CPU _selectedCPU = new() { Id = -1, Name = "None" };

        public CPU SelectedCPU
        {
            get => _selectedCPU;
            set
            {
                _selectedCPU = value;
                OnPropertyChanged(nameof(SelectedCPU));
            }
        }

        public SelectCPUPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedCPU))
            {
                if (SelectedCPU != null && SelectedCPU.Id != -1)
                {
                    tbArchitecture.Text = SelectedCPU.Architecture;
                    tbTDP.Text = SelectedCPU.TDP.ToString();
                    tbBrand.Text = SelectedCPU.Brand;
                    tbBaseClock.Text = SelectedCPU.BaseClock.ToString();
                    tbBoostClock.Text = SelectedCPU.BoostClock.ToString();
                    tbCores.Text = SelectedCPU.Cores.ToString();
                    tbThreads.Text = SelectedCPU.Threads.ToString();
                    tbSocket.Text = (from socket in ComponentsDB.Sockets where socket.Id == SelectedCPU.SocketId select socket).ToList().FirstOrDefault().Name;
                    tbPrice.Text = SelectedCPU.Price.ToString();
                    string? img_src = _selectedCPU.ImgUrl;
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
            tbArchitecture.Text = string.Empty;
            tbTDP.Text = string.Empty;
            tbBrand.Text = string.Empty;
            tbBaseClock.Text = string.Empty;
            tbBoostClock.Text = string.Empty;
            tbCores.Text = string.Empty;
            tbThreads.Text = string.Empty;
            tbSocket.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }

        public void SetUp(int motherboardId = -1, int cpuId = -1)
        {
            CPUList.Clear();
            CPU empty = new()
            {
                Id = -1,
                Name = "None"
            };
            CPUList.Add(empty);

            CPU sel = null;

            if (motherboardId == -1)
            {
                if (cpuId == -1)
                    ListBoxCPUs.SelectedItem = empty;

                foreach (CPU cpu in ComponentsDB.CPUs)
                {
                    CPUList.Add(cpu);
                    if (cpu.Id == cpuId)
                        sel = cpu;
                }
            }
            else
            {
                if (cpuId == -1)
                    ListBoxCPUs.SelectedItem = empty;

                int socketId = (from mb in ComponentsDB.Motherboards where mb.Id == motherboardId select mb.SocketId).First();
                foreach (CPU cpu in (from cpu in ComponentsDB.CPUs where cpu.SocketId == socketId select cpu))
                {
                    CPUList.Add(cpu);
                    sel = cpu;
                }
            }

            if (sel != null)
            {
                SelectedCPU = sel;
            }
        }

        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            CPUSelectedHandler?.Invoke(this, new SelectedCPUEventArgs(SelectedCPU));
        }
    }
}
