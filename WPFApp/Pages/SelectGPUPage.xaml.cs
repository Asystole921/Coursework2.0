using Library.CustomEventArgs;
using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFApp.Pages
{
    public partial class SelectGPUPage : ObservableUserControl
    {
        public event EventHandler<SelectedGPUEventArgs>? GPUSelectedHandler;

        public ObservableCollection<GPU> GPUList { get; set; } = [];

        private GPU? _selectedGPU;

        public GPU? SelectedGPU
        {
            get => _selectedGPU;
            set
            {
                _selectedGPU = value;
                OnPropertyChanged(nameof(SelectedGPU));
            }
        }
        
        public ComponentsDBContext ComponentsDB { get; set; }

        public SelectGPUPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedGPU))
            {
                if (SelectedGPU != null && SelectedGPU.Id != -1)
                {
                    tbBrand.Text = SelectedGPU.Brand;
                    tbChipset.Text = SelectedGPU.Chipset.ToString();
                    tbMemoryCapacity.Text = SelectedGPU.MemoryCapacity.ToString();
                    tbMemoryType.Text = SelectedGPU.MemoryType.ToString();
                    tbTDP.Text = SelectedGPU.TDP.ToString();
                    tbBaseClock.Text = SelectedGPU.BaseClock.ToString();
                    tbBoostClock.Text = SelectedGPU.BoostClock.ToString();
                    tbArhitecture.Text = SelectedGPU.Architecture;
                    tbPrice.Text = SelectedGPU.Price.ToString();

                    string? img_src = SelectedGPU.ImgUrl;
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
            tbChipset.Text = string.Empty;
            tbMemoryCapacity.Text = string.Empty;
            tbMemoryType.Text = string.Empty;
            tbTDP.Text = string.Empty;
            tbBaseClock.Text = string.Empty;
            tbBoostClock.Text = string.Empty;
            tbArhitecture.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }

        public void SetUp(int gpuId = -1)
        {
            GPUList.Clear();
            GPU empty = new() { Id = -1, Name = "None" };
            GPUList.Add(empty);
            foreach (GPU gpu in ComponentsDB.GPUs.ToList())
            {
                GPUList.Add(gpu);
            }
            GPU? sel = GPUList.FirstOrDefault(gpu => gpu.Id == gpuId);
            if (sel != null)
            {
                ListBoxGPUs.SelectedItem = sel;
            }
            else
            {
                ListBoxGPUs.SelectedItem = empty;
            }
        }

        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            GPUSelectedHandler?.Invoke(this, new SelectedGPUEventArgs(SelectedGPU));
        }
    }
}
