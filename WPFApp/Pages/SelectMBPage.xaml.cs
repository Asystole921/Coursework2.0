using Library.CustomEventArgs;
using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFApp.Pages
{
    public partial class SelectMBPage : ObservableUserControl
    {
        public event EventHandler<SelectedMBEventArgs>? MBSelectedHandler;

        public ObservableCollection<Motherboard> MBList { get; set; } = [];

        private Motherboard? _selectedMB;

        public Motherboard? SelectedMB
        {
            get => _selectedMB;
            set
            {
                _selectedMB = value;
                OnPropertyChanged(nameof(SelectedMB));
            }
        }

        public ComponentsDBContext ComponentsDB { get; set; }

        public SelectMBPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedMB))
            {
                if (SelectedMB != null && SelectedMB.Id != -1)
                {
                    tbBrand.Text = SelectedMB.Brand;
                    tbChipset.Text = SelectedMB.Chipset.ToString();
                    tbFormFactor.Text = SelectedMB.FormFactor;
                    tbUSBPort.Text = SelectedMB.USBPorts.ToString();
                    tbRAMType.Text = (from RamType in ComponentsDB.RAMTypes where RamType.Id == SelectedMB.RAMTypeId select RamType).ToList().FirstOrDefault().Name;
                    tbMAXRAM.Text = SelectedMB.MaxRAM.ToString();
                    tbPCIeSlot.Text = SelectedMB.PCIeSlots.ToString();
                    tbSoket.Text = (from socket in ComponentsDB.Sockets where socket.Id == SelectedMB.SocketId select socket).ToList().FirstOrDefault().Name;
                    tbSATAPort.Text = SelectedMB.SATAPorts.ToString();
                    tbPrice.Text = SelectedMB.Price.ToString();

                    string? img_src = SelectedMB.ImgUrl;
                    if (img_src != null)
                    {
                        try
                        {
                            ImgUrl.Source = new BitmapImage(new Uri(img_src));
                            ImgUrl.Visibility = Visibility.Visible;
                        }
                        catch { }
                    }
                    else if (img_src == null)
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
            tbFormFactor.Text = string.Empty;
            tbUSBPort.Text = string.Empty;
            tbRAMType.Text = string.Empty;
            tbMAXRAM.Text = string.Empty;
            tbPCIeSlot.Text = string.Empty;
            tbSoket.Text = string.Empty;
            tbSATAPort.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Source = null;
            ImgUrl.Visibility = Visibility.Hidden;
        }

        public void SetUp(int motherboardId = -1, int cpuId = -1, int RamId = -1)
        {
            MBList.Clear();
            Motherboard empty = new()
            {
                Id = -1,
                Name = "None"
            };
            MBList.Add(empty);

            CPU cpu;
            Socket socket = new() { Id = -1 };
            if (cpuId != -1)
            {
                cpu = (from Dbcpu in ComponentsDB.CPUs where Dbcpu.Id == cpuId select Dbcpu).First();
                socket = (from DbSocket in ComponentsDB.Sockets where DbSocket.Id == cpu.SocketId select DbSocket).First();
            }


            RAM? ram;
            RAMType ramType = new() { Id = -1 };
            if (RamId != -1)
            {
                ram = (from DbRam in ComponentsDB.RAMs where DbRam.Id == RamId select DbRam).First();
                ramType = (from DbRamType in ComponentsDB.RAMTypes where DbRamType.Id == ram.RAMTypeId select DbRamType).First();
            }

            foreach (Motherboard mb in (from mb in ComponentsDB.Motherboards where (cpuId == -1 || socket.Id == mb.SocketId) && (RamId == -1 || ramType.Id == mb.RAMTypeId) select mb))
                MBList.Add(mb);

            Motherboard? sel = MBList.FirstOrDefault(mb => mb.Id == motherboardId);
            if (sel != null)
                ListBoxMB.SelectedItem = sel;
            else
                ListBoxMB.SelectedItem = empty;
        }


        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            MBSelectedHandler?.Invoke(this, new SelectedMBEventArgs(SelectedMB));
        }
    }
}
