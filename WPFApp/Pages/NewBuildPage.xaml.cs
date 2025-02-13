using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Library.Models;
using Library;
using Library.CustomEventArgs;

namespace WPFApp.Pages
{
    public partial class NewBuildPage : ObservableUserControl
    {
        public event EventHandler? BackToMainMenu;
        public event EventHandler? BackToSavedBuilds;

        public Build Build { get; set; } = new() { Id = -1 };
        public CPU SelectedCPU = new() { Id = -1 };
        public GPU SelectedGPU = new() { Id = -1 };
        public Motherboard SelectedMB = new() { Id = -1 };
        public RAM SelectedRAM = new() { Id = -1 };
        public CPUCooler SelectedCC = new() { Id = -1 };
        public PCCase SelectedCase = new() { Id = -1 };
        public Storage SelectedStorage = new() { Id = -1 };
        public PowerSupply SelectedPS = new() { Id = -1 };

        public double TotalPrice { get; set; }

        public double USD_UAH { get; set; }

        public ComponentsDBContext ComponentsDB { get; set; }

        public NewBuildPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            SetUpEventHandler();
        }

        private void SetUpEventHandler()
        {
            SelectCPUPage.CPUSelectedHandler += OnCPUSelect;
            SelectGPUPage.GPUSelectedHandler += OnGPUSelect;
            SelectMBPage.MBSelectedHandler += OnMBSelect;
            SelectRAMPage.RAMSelectedHandler += OnRAMSelect;
            SelectCPUCoolerPage.CCSelectedHandler += OnCCSelect;
            SelectPCCasePage.PCCaseSelectedHandler += OnPCCaseSelect;
            SelectStoragePage.StorageSelectedHandler += OnStorageSelect;
            SelectPSPage.PSSelectedHandler += OnPSSelect;
        }


        #region CPU Command
        private void OnCPUSelect(object? sender, SelectedCPUEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectCPUPage.Visibility = Visibility.Hidden;
            SelectedCPU = e.CPU;
            if (SelectedCPU.Id != -1)
            {
                ImgUrlCPU.Source = new BitmapImage(new Uri(SelectedCPU.ImgUrl));
                TextBoxCPU.Content = "";
                TextBoxCPU.Background.Opacity = 0;
            }
            else
            {
                ImgUrlCPU.Source = new BitmapImage();
                if (SelectedCPU.Id == -1)
                {
                    TextBoxCPU.Content = "CPU";
                    TextBoxCPU.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxCPU.Content = SelectedGPU.Name;
                    TextBoxCPU.Background.Opacity = 0.4;
                }
            }
            CalculateTotalPrice();

        }
        private void TextBoxCPU_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectCPUPage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectCPUPage.SetUp(cpuId: SelectedCPU.Id, motherboardId: SelectedMB.Id);
        }
        #endregion

        #region GPU Command
        private void OnGPUSelect(object? sender, SelectedGPUEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectGPUPage.Visibility = Visibility.Hidden;
            SelectedGPU = e.GPU;
            if (SelectedGPU.Id != -1)
            {
                ImgUrlGPU.Source = new BitmapImage(new Uri(SelectedGPU.ImgUrl));
                TextBoxGPU.Content = "";
                TextBoxGPU.Background.Opacity = 0;
            }
            else
            {
                ImgUrlMB.Source = new BitmapImage();
                if (SelectedGPU.Id == -1)
                {
                    TextBoxGPU.Content = "GPU";
                    TextBoxGPU.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxGPU.Content = SelectedGPU.Name;
                    TextBoxGPU.Background.Opacity = 0.4;
                }
            }
            CalculateTotalPrice();
        }
        private void TextBoxGPU_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectGPUPage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectGPUPage.SetUp(gpuId: SelectedGPU.Id);
        }
        #endregion

        #region MB Command
        private void OnMBSelect(object? sender, SelectedMBEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectMBPage.Visibility = Visibility.Hidden;
            SelectedMB = e.Motherboard;
            if (SelectedMB.ImgUrl != null)
            {
                ImgUrlMB.Source = new BitmapImage(new Uri(SelectedMB.ImgUrl));
                TextBoxMB.Content = "";
                TextBoxMB.Background.Opacity = 0;
            }
            else
            {
                ImgUrlMB.Source = new BitmapImage();
                if (SelectedMB.Id == -1)
                {
                    TextBoxMB.Content = "Motherboard";
                    TextBoxMB.Background.Opacity = 0.4;

                }
                else
                {
                    TextBoxMB.Content = SelectedMB.Name;
                    TextBoxMB.Background.Opacity = 0.4;
                }

            }
            CalculateTotalPrice();

        }
        private void TextBoxMB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectMBPage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectMBPage.SetUp(motherboardId: SelectedMB.Id, cpuId: SelectedCPU.Id, RamId: SelectedRAM.Id);
        }
        #endregion

        #region RAM Command
        private void OnRAMSelect(object? sender, SelectedRAMEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectRAMPage.Visibility = Visibility.Hidden;
            SelectedRAM = e.RAM;
            if (SelectedRAM.ImgUrl != null)
            {
                ImgUrlRAM.Source = new BitmapImage(new Uri(SelectedRAM.ImgUrl));
                TextBoxRAM.Content = "";
                TextBoxRAM.Background.Opacity = 0;
            }
            else
            {
                ImgUrlRAM.Source = new BitmapImage();
                if (SelectedRAM.Id == -1)
                {
                    TextBoxRAM.Content = "RAM";
                    TextBoxRAM.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxRAM.Content = SelectedRAM.Name;
                    TextBoxRAM.Background.Opacity = 0.4;
                }
                CalculateTotalPrice();
            }
        }

        private void TextBoxRAM_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectRAMPage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectRAMPage.SetUp(RamId: SelectedRAM.Id, motherboardId: SelectedMB.Id);
        }
        #endregion

        #region Case Command
        private void OnPCCaseSelect(object? sender, SelectedPCCaseEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectPCCasePage.Visibility = Visibility.Hidden;
            SelectedCase = e.PCCase;
            if (SelectedCase.Id != -1)
            {
                ImgUrlCase.Source = new BitmapImage(new Uri(SelectedCase.ImgUrl));
                TextBoxPCCase.Content = "";
                TextBoxPCCase.Background.Opacity = 0;
            }
            else
            {
                ImgUrlCase.Source = new BitmapImage();
                if (SelectedCase.Id == -1)
                {
                    TextBoxPCCase.Content = "Case";
                    TextBoxPCCase.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxPCCase.Content = SelectedCase.Name;
                    TextBoxPCCase.Background.Opacity = 0.4;
                }
            }
            CalculateTotalPrice();

        }

        private void TextBoxPCCase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectPCCasePage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectPCCasePage.SetUp(caseId: SelectedCase.Id);
        }
        #endregion

        #region CC Command
        private void TextBoxCC_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectCPUCoolerPage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectCPUCoolerPage.SetUp(motherboardId: SelectedMB.Id, CCId: SelectedCC.Id);

        }
        private void OnCCSelect(object? sender, SelectedCCEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectCPUCoolerPage.Visibility = Visibility.Hidden;
            SelectedCC = e.CPUCooler;
            if (SelectedCC.Id != -1)
            {
                try
                {
                    if (!string.IsNullOrEmpty(SelectedCC.ImgUrl))
                    {
                        ImgUrlCC.Source = new BitmapImage(new Uri(SelectedCC.ImgUrl));
                    }
                    else
                    {
                        ImgUrlCC.Source = new BitmapImage();
                    }
                }
                catch (UriFormatException ex)
                {
                    MessageBox.Show($"Invalid URI: {ex.Message}");
                    ImgUrlCC.Source = new BitmapImage();
                }

                TextBoxCC.Content = "";
                TextBoxCC.Background.Opacity = 0;
            }
            else
            {
                ImgUrlCC.Source = new BitmapImage();
                if (SelectedCC.Id == -1)
                {
                    TextBoxCC.Content = "CPU Cooler";
                    TextBoxCC.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxCC.Content = SelectedCC.Name;
                    TextBoxCC.Background.Opacity = 0.4;
                }
            }

            CalculateTotalPrice();

        }
        #endregion

        #region Storage Command
        private void TextBoxStorage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectStoragePage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectStoragePage.SetUp(StorageId: SelectedStorage.Id);

        }
        private void OnStorageSelect(object? sender, SelectedStorageEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectStoragePage.Visibility = Visibility.Hidden;
            SelectedStorage = e.Storage;
            if (SelectedStorage.Id != -1)
            {
                ImgUrlStorage.Source = new BitmapImage(new Uri(SelectedStorage.ImgUrl));
                TextBoxStorage.Content = "";
                TextBoxStorage.Background.Opacity = 0;
            }
            else
            {
                ImgUrlStorage.Source = new BitmapImage();
                if (SelectedStorage.Id == -1)
                {
                    TextBoxStorage.Content = "Storage";
                    TextBoxStorage.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxStorage.Content = SelectedStorage.Name;
                    TextBoxStorage.Background.Opacity = 0.4;
                }
            }
            CalculateTotalPrice();

        }

        #endregion

        #region PowerSupply Command
        private void TextBoxPS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectPSPage.Visibility = Visibility.Visible;
            NewBuild.Visibility = Visibility.Hidden;
            SelectPSPage.SetUp(psId: SelectedPS.Id);
        }

        private void OnPSSelect(object? sender, SelectedPSEventArgs e)
        {
            NewBuild.Visibility = Visibility.Visible;
            SelectPSPage.Visibility = Visibility.Hidden;
            SelectedPS = e.PowerSupply;
            if (SelectedPS.Id != -1)
            {
                ImgUrlPS.Source = new BitmapImage(new Uri(SelectedPS.ImgUrl));
                TextBoxPowerSupply.Content = "";
                TextBoxPowerSupply.Background.Opacity = 0;
            }
            else
            {
                ImgUrlPS.Source = new BitmapImage();
                if (SelectedPS.Id == -1)
                {
                    TextBoxPowerSupply.Content = "Power Supply";
                    TextBoxPowerSupply.Background.Opacity = 0.4;
                }
                else
                {
                    TextBoxPowerSupply.Content = SelectedPS.Name;
                    TextBoxPowerSupply.Background.Opacity = 0.4;
                }
            }
            CalculateTotalPrice();
        }

        #endregion

        #region Calculate Command
        public void CalculateTotalPrice()
        {
            TotalPrice = 0;
            TotalPrice += SelectedCPU.Price;
            TotalPrice += SelectedGPU.Price;
            TotalPrice += SelectedMB.Price;
            TotalPrice += SelectedRAM.Price;
            TotalPrice += SelectedCC.Price;
            TotalPrice += SelectedCase.Price;
            TotalPrice += SelectedPS.Price;
            TotalPrice += SelectedStorage.Price;
            if ((string)((ComboBoxItem)ComboBoxTotalPrice.SelectedItem).Content == "USD")
                TextBoxTotalPrice.Content = "Total price: " + TotalPrice.ToString("F2") + "$";
            else
            {
                TextBoxTotalPrice.Content = "Total price: " + (TotalPrice * USD_UAH).ToString("F2") + "₴";
            }

        }
        private void ComboBoxTotalPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateTotalPrice();
        }
        #endregion

        #region Reset Command
        private void ResetSelectedComponents()
        {
            SelectedCPU = new() { Id = -1 };
            SelectedGPU = new() { Id = -1 };
            SelectedMB = new() { Id = -1 };
            SelectedRAM = new() { Id = -1 };
            SelectedCC = new() { Id = -1 };
            SelectedCase = new() { Id = -1 };
            SelectedStorage = new() { Id = -1 };
            SelectedPS = new() { Id = -1 };
        }
        private void ResetSelectedImage()
        {
            ImgUrlCPU.Source = new BitmapImage();
            ImgUrlGPU.Source = new BitmapImage();
            ImgUrlRAM.Source = new BitmapImage();
            ImgUrlMB.Source = new BitmapImage();
            ImgUrlPS.Source = new BitmapImage();
            ImgUrlCC.Source = new BitmapImage();
            ImgUrlStorage.Source = new BitmapImage();
            ImgUrlCase.Source = new BitmapImage();
        }
        private void ResetLable()
        {
            TextBoxStorage.Content = "Storage";
            TextBoxStorage.Background.Opacity = 20;
            TextBoxCC.Content = "CPU Coller";
            TextBoxCC.Background.Opacity = 20;
            TextBoxPCCase.Content = "Case";
            TextBoxPCCase.Background.Opacity = 20;
            TextBoxRAM.Content = "RAM";
            TextBoxRAM.Background.Opacity = 20;
            TextBoxMB.Content = "Motherboard";
            TextBoxMB.Background.Opacity = 20;
            TextBoxGPU.Content = "GPU";
            TextBoxGPU.Background.Opacity = 20;
            TextBoxCPU.Content = "CPU";
            TextBoxCPU.Background.Opacity = 20;
            TextBoxPowerSupply.Content = "Power Sypply";
            TextBoxPowerSupply.Background.Opacity = 20;
            BuildNameTextBox.Clear();
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("You didnt save build", "Warning", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                if (Build.Id == -1)
                {
                    BackToMainMenu?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    BackToSavedBuilds?.Invoke(this, EventArgs.Empty);
                }
                ResetSelectedComponents();
                ResetSelectedImage();
                ResetLable();
                TotalPrice = 0;
                TextBoxTotalPrice.Content = "Total price:";
            }
        }

        private void SaveBuild()
        {
            var isNew = Build.Id == -1;
            var currentBuild = isNew
                ? new Build()
                : Build;

            if (isNew)
                currentBuild.Name = BuildNameTextBox.Text;

            currentBuild.CPUId = SelectedCPU.Id;
            currentBuild.GPUId = SelectedGPU.Id;
            currentBuild.MotherboardId = SelectedMB.Id;
            currentBuild.PowerSupplyId = SelectedPS.Id;
            currentBuild.RAMId = SelectedRAM.Id;
            currentBuild.StorageId = SelectedStorage.Id;
            currentBuild.PCCaseId = SelectedCase.Id;
            currentBuild.CPUCoolerId = SelectedCC.Id;
            currentBuild.BuildTotalPrice = Math.Round(TotalPrice, 2);

            if (isNew)
                ComponentsDB.Builds.Add(currentBuild);

            ComponentsDB.SaveChanges();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuildNameTextBox.Text != string.Empty || Build.Id != -1)
            {
                SaveBuild();
                if (Build.Id == -1)
                {
                    BackToMainMenu?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    BackToSavedBuilds?.Invoke(this, EventArgs.Empty);
                }
                ResetSelectedComponents();
                ResetSelectedImage();
                ResetLable();
            }
            else
                MessageBox.Show("You didnt Enter Name", "Warning", MessageBoxButton.OK);
        }
        public void SetUp(Build build)
        {
            Build = build;
            if (Build.Id == -1)
            {
                BuildNameLable.Visibility = Visibility.Visible;
                BuildNameTextBox.Visibility = Visibility.Visible;
                CalculateTotalPrice();
                ResetLable();
                ResetSelectedComponents();
                ResetSelectedImage();
            }
            else
            {
                BuildNameLable.Visibility = Visibility.Hidden;
                BuildNameTextBox.Visibility = Visibility.Hidden;

                CPU? cpu =  ComponentsDB.CPUs.FirstOrDefault(cpu => cpu.Id == Build.CPUId);
                OnCPUSelect(null, new(cpu == null ? new CPU() { Id = -1 } : cpu));

                GPU? gpu =  ComponentsDB.GPUs.FirstOrDefault(GPU => GPU.Id == Build.GPUId);
                OnGPUSelect(null, new(gpu == null ? new GPU() { Id = -1 } : gpu));

                RAM? ram =  ComponentsDB.RAMs.FirstOrDefault(ram => ram.Id == Build.RAMId);
                OnRAMSelect(null, new(ram == null ? new RAM() { Id = -1 } : ram));

                Motherboard? mb =  ComponentsDB.Motherboards.FirstOrDefault(mb => mb.Id == Build.MotherboardId);
                OnMBSelect(null, new(mb == null ? new Motherboard() { Id = -1 } : mb));

                CPUCooler? CC =  ComponentsDB.CPUCoolers.FirstOrDefault(CC => CC.Id == Build.CPUCoolerId);
                OnCCSelect(null, new(CC == null ? new CPUCooler() { Id = -1 } : CC));

                PCCase? Case =  ComponentsDB.PCCases.FirstOrDefault(Case => Case.Id == Build.PCCaseId);
                OnPCCaseSelect(null, new(Case == null ? new PCCase() { Id = -1 } : Case));

                PowerSupply? PS =  ComponentsDB.PowerSupplies.FirstOrDefault(PS => PS.Id == Build.PowerSupplyId);
                OnPSSelect(null, new(PS == null ? new PowerSupply() { Id = -1 } : PS));

                Storage? storage =  ComponentsDB.Storages.FirstOrDefault(storage => storage.Id == Build.StorageId);
                OnStorageSelect(null, new(storage == null ? new Storage() { Id = -1 } : storage));
            }
        }
    }
}
