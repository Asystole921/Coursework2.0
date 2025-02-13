using CommunityToolkit.Mvvm.Input;
using Library.Models;
using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Data;

namespace WPFApp.Pages
{
    public partial class NewComponentPage : ObservableUserControl, INotifyPropertyChanged
    {
        public ComponentsDBContext ComponentsDB { get; set; }

        private List<DataGrid> DataGrids = [];
        private List<StackPanel> StackPanels = [];
        public ICommand SearchCommand { get; set; }

        private string _searchFilter;
        public string SearchFilter
        {
            get => _searchFilter;
            set
            {
                _searchFilter = value;
                OnPropertyChanged(nameof(SearchFilter));
            }
        }


        #region ReturnCommand

        public ICommand ReturnCommand { get; set; }

        public event EventHandler? ReturnCommandExecuted;

        private void ReturnCommandExecute()
        {
            ReturnCommandExecuted?.Invoke(this, EventArgs.Empty);
        }

        #endregion

      

        public NewComponentPage()
        {
            DataContext = this;

            ComponentsDB = new ComponentsDBContext();

            ReturnCommand = new RelayCommand(ReturnCommandExecute);
            SearchCommand = new RelayCommand(ExecuteSearch);

            InitializeComponent();

            SetUpData();

            SetUpPanels();

            SetUp();

            InitializeCollectionsView();
        }
        private void ExecuteSearch()
        {
            ApplyFilter();
        }


        #region CollectionsAndViews

        public ObservableCollection<CPU> CPUList { get; init; } = new();

        private ICollectionView _cpuCollectionView;
        public ICollectionView CPUCollectionView
        {
            get => _cpuCollectionView;
            set
            {
                _cpuCollectionView = value;
                OnPropertyChanged(nameof(CPUCollectionView));
            }
        }
       
        public ObservableCollection<GPU> GPUList { get; init; } = new();

        private ICollectionView _gpuCollectionView;
        public ICollectionView GPUCollectionView
        {
            get => _gpuCollectionView;
            set
            {
                _gpuCollectionView = value;
                OnPropertyChanged(nameof(GPUCollectionView));
            }
        }

        public ObservableCollection<Motherboard> MBList { get; init; } = new();

        private ICollectionView _mbCollectionView;
        public ICollectionView MBCollectionView
        {
            get => _mbCollectionView;
            set
            {
                _mbCollectionView = value;
                OnPropertyChanged(nameof(MBCollectionView));
            }
        }

        public ObservableCollection<RAM> RAMList { get; init; } = new();

        private ICollectionView _ramCollectionView;
        public ICollectionView RAMCollectionView
        {
            get => _ramCollectionView;
            set
            {
                _ramCollectionView = value;
                OnPropertyChanged(nameof(RAMCollectionView));
            }
        }


        public ObservableCollection<CPUCooler> CCList { get; init; } = new();

        private ICollectionView _ccCollectionView;
        public ICollectionView CCCollectionView 
        {
            get => _ccCollectionView;
            set 
            {
                _ccCollectionView = value;
                OnPropertyChanged(nameof(CCCollectionView));
            }
                 
        }


        public ObservableCollection<PCCase> CaseList { get; init; } = new();
       
        private ICollectionView _pcCaseCollectionView;
        public ICollectionView CaseCollectionView
        {
            get => _pcCaseCollectionView;
            set
            {
                _pcCaseCollectionView= value;
                OnPropertyChanged(nameof(CaseCollectionView));
            }
        }    


        public ObservableCollection<PowerSupply> PSList { get; init; } = new();

        private ICollectionView _powerSupplyCollectionView;
        public ICollectionView PSCollectionView
        {
            get => _powerSupplyCollectionView;
            set
            {
                _powerSupplyCollectionView= value;
                OnPropertyChanged(nameof(PSCollectionView));
            }
        }

        public ObservableCollection<Storage> StorageList { get; init; } = new();

        private ICollectionView _psStorageCollectionView;
        public ICollectionView PSStorageCollectionView
        {
            get => _psStorageCollectionView;
            set
            {
                _psStorageCollectionView= value;
                OnPropertyChanged(nameof(PSStorageCollectionView));
            }
        }


        #endregion CollectionsAndViews

        #region SelectedComponents

        private CPU? _selectedCPU;
        public CPU? SelectedCPU 
        {
            get => _selectedCPU;
            set
            {
                _selectedCPU = value;
                ((RelayCommand)RemoveCommandCPU).NotifyCanExecuteChanged();
            }
        }
        private GPU? _selectedGPU;

        public GPU? SelectedGPU
        {
            get => _selectedGPU;
            set
            {
                _selectedGPU = value;
                ((RelayCommand)RemoveCommandGPU).NotifyCanExecuteChanged();
            }
        }

        private Motherboard? _selectedMB;

        public Motherboard? SelectedMB
        {
            get => _selectedMB;
            set
            {
                _selectedMB = value;
                ((RelayCommand)RemoveCommandMB).NotifyCanExecuteChanged();
            }
        }
        private RAM? _selectedRAM;

        public RAM? SelectedRAM
        {
            get => _selectedRAM;
            set
            {
                _selectedRAM = value;
                ((RelayCommand)RemoveCommandRAM).NotifyCanExecuteChanged();
            }
        }
        private CPUCooler? _selectedCC;

        public CPUCooler? SelectedCC
        {
            get => _selectedCC;
            set
            {
                _selectedCC = value;
                ((RelayCommand)RemoveCommandCC).NotifyCanExecuteChanged();
            }
        }
        private PowerSupply? _selectedPS;

        public PowerSupply? SelectedPS
        {
            get => _selectedPS;
            set
            {
                _selectedPS = value;
                ((RelayCommand)RemoveCommandPS).NotifyCanExecuteChanged();
            }
        }
        private Storage? _selectedStorage;

        public Storage? SelectedStorage
        {
            get => _selectedStorage;
            set
            {
                _selectedStorage = value;
                ((RelayCommand)RemoveCommandStorage).NotifyCanExecuteChanged();
            }
        }
        private PCCase? _selectedPCCase;

        public PCCase? SelectedPCCase
        {
            get => _selectedPCCase;
            set
            {
                _selectedPCCase = value;
                ((RelayCommand)RemoveCommandCase).NotifyCanExecuteChanged();
            }
        }
        #endregion SelectedComponents

        #region SetUp

        private void SetUpData()
        {
            DataGrids.Add(DataGridCPU);
            DataGrids.Add(DataGridGPU);
            DataGrids.Add(DataGridMB);
            DataGrids.Add(DataGridRAM);
            DataGrids.Add(DataGridCC);
            DataGrids.Add(DataGridStorage);
            DataGrids.Add(DataGridPS);
            DataGrids.Add(DataGridCase);
        }

        private void SetUpPanels()
        {
            StackPanels.Add(StackPanelCPU);
            StackPanels.Add(StackPanelGPU);
            StackPanels.Add(StackPanelMB);
            StackPanels.Add(StackPanelRAM);
            StackPanels.Add(StackPanelStorage);
            StackPanels.Add(StackPanelPS);
            StackPanels.Add(StackPanelCC);
            StackPanels.Add(StackPanelPCCase);
        }

        public void SetUp()
        {
          

            foreach (var cpu in ComponentsDB.CPUs)
                CPUList.Add(cpu);
           

            foreach (var gpu in ComponentsDB.GPUs)
                GPUList.Add(gpu);

            foreach (var motherboard in ComponentsDB.Motherboards)
                MBList.Add(motherboard);

            foreach (var ram in ComponentsDB.RAMs)
                RAMList.Add(ram);

            foreach (var caseItem in ComponentsDB.PCCases)
                CaseList.Add(caseItem);

            foreach (var cooler in ComponentsDB.CPUCoolers)
                CCList.Add(cooler);

            foreach (var powerSupply in ComponentsDB.PowerSupplies)
                PSList.Add(powerSupply);

            foreach (var storage in ComponentsDB.Storages)
                StorageList.Add(storage);
        }
        private void InitializeCollectionsView()
        {
            CPUCollectionView = CollectionViewSource.GetDefaultView(CPUList);
            GPUCollectionView = CollectionViewSource.GetDefaultView(GPUList);
            MBCollectionView = CollectionViewSource.GetDefaultView(MBList);
            RAMCollectionView = CollectionViewSource.GetDefaultView(RAMList);
            CCCollectionView = CollectionViewSource.GetDefaultView(CCList);
            CaseCollectionView = CollectionViewSource.GetDefaultView(CaseList);
            PSCollectionView = CollectionViewSource.GetDefaultView(PSList);
            PSStorageCollectionView = CollectionViewSource.GetDefaultView(StorageList);
        }


        #endregion SetUp

        #region Filter

        private void ApplyFilter()
        {
            var filterText = SearchFilter?.ToLower() ?? string.Empty;

            Predicate<object> filter = item =>
            {
                if (item is CPU cpu)
                    return cpu.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           cpu.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is GPU gpu)
                    return gpu.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           gpu.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is Motherboard mb)
                    return mb.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           mb.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is RAM ram)
                    return ram.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           ram.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is CPUCooler cc)
                    return cc.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           cc.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is PCCase pcc)
                    return pcc.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           pcc.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is PowerSupply ps)
                    return ps.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           ps.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                if (item is Storage storage)
                    return storage.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase) ||
                           storage.Brand.Contains(filterText, StringComparison.OrdinalIgnoreCase);
                return false;
            };

            CPUCollectionView.Filter = filter;
            GPUCollectionView.Filter = filter;
            MBCollectionView.Filter = filter;
            RAMCollectionView.Filter = filter;
            CCCollectionView.Filter = filter;
            CaseCollectionView.Filter = filter;
            PSCollectionView.Filter = filter;
            PSStorageCollectionView.Filter = filter;
        }

        #endregion

        #region CPU Commands

        public ICommand AddCommandCPU => new RelayCommand(AddItemCPU);
        public ICommand RemoveCommandCPU => new RelayCommand(RemoveItemCPU);
       

        private void AddItemCPU()
        {
            CPU cpu = new()
            {
                Name = "None",
                Brand = "None",
                Architecture = "None",
                BaseClock = 0,
                BoostClock = 0,
                Cores = 0,
                Threads = 0,
                TDP = 0,
                SocketId = 1,
                Price = 0,
                ImgUrl = "None"
            };
            CPUList.Add(cpu);
            ComponentsDB.CPUs.Add(cpu);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemCPU()
        {
            if (SelectedCPU != null)
                ComponentsDB.CPUs.Remove(SelectedCPU);

            ComponentsDB.SaveChanges();
            CPUList.Remove(SelectedCPU);
        }

        #endregion CPU Commands

        #region GPU Commands

        public ICommand AddCommandGPU => new RelayCommand(AddItemGPU);
        public ICommand RemoveCommandGPU => new RelayCommand(RemoveItemGPU);

       

        private void AddItemGPU()
        {
            GPU gpu = new()
            {
                Name = "None",
                Brand = "None",
                Chipset = "None",
                MemoryCapacity = 0,
                MemoryType = "None",
                TDP = 0,
                BaseClock = 0,
                BoostClock = 0,
                Architecture = "None",
                Price = 0,
                ImgUrl = "None"
            };
            GPUList.Add(gpu);
            ComponentsDB.GPUs.Add(gpu);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemGPU()
        {   
            if (SelectedGPU != null)
                ComponentsDB.GPUs.Remove(SelectedGPU);

            ComponentsDB.SaveChanges();
            GPUList.Remove(SelectedGPU);
        }

        #endregion GPU Commands

        #region RAM Commands

        public ICommand AddCommandRAM => new RelayCommand(AddItemRAM);
        public ICommand RemoveCommandRAM => new RelayCommand(RemoveItemRAM);

        private void AddItemRAM()
        {
            RAM ram = new()
            {
                Name = "None",
                Brand = "None",
                RAMTypeId = 1,
                Frequency = 0,
                Capacity = 0,
                Modules = 0,
                CASLatency = 0,
                Price = 0,
                ImgUrl = "None"
            };
            RAMList.Add(ram);
            ComponentsDB.RAMs.Add(ram);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemRAM()
        {   
            if (SelectedRAM != null) 
                ComponentsDB.RAMs.Remove(SelectedRAM);

            ComponentsDB.SaveChanges();
            RAMList.Remove(SelectedRAM);
        }

        #endregion RAM Commands

        #region MB Commands

        public ICommand AddCommandMB => new RelayCommand(AddItemMB);
        public ICommand RemoveCommandMB => new RelayCommand(RemoveItemMB);

        private void AddItemMB()
        {
            Motherboard motherboard = new()
            {
                Name = "None",
                Brand = "None",
                Chipset = "None",
                FormFactor = "None",
                SocketId = 1,
                RAMTypeId = 1,
                MaxRAM = 0,
                PCIeSlots = 0,
                USBPorts = 0,
                SATAPorts = 0,
                Price = 0,
                ImgUrl = "None"
            };
            MBList.Add(motherboard);
            ComponentsDB.Motherboards.Add(motherboard);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemMB()
        {   
            if (SelectedMB != null) 
                ComponentsDB.Motherboards.Remove(SelectedMB); 

            ComponentsDB.SaveChanges();
            MBList.Remove(SelectedMB);
        }

        #endregion MB Commands

        #region Case Commands

        public ICommand AddCommandCase => new RelayCommand(AddItemCase);
        public ICommand RemoveCommandCase => new RelayCommand(RemoveItemCase);

        private void AddItemCase()
        {
            PCCase caseItem = new()
            {
                Name = "None",
                Brand = "None",
                Type = "None",
                FormFactor = "None",
                Color = "None",
                Dimensions = "None",
                Price = 0,
                ImgUrl = "None"
            };
            CaseList.Add(caseItem);
            ComponentsDB.PCCases.Add(caseItem);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemCase()
        {   
            if (SelectedPCCase != null) 
                ComponentsDB.PCCases.Remove(SelectedPCCase);

            ComponentsDB.SaveChanges();
            CaseList.Remove(SelectedPCCase);
        }

        #endregion Case Commands

        #region Cooling Commands

        public ICommand AddCommandCC => new RelayCommand(AddItemCC);
        public ICommand RemoveCommandCC => new RelayCommand(RemoveItemCC);

        private void AddItemCC()
        {
            CPUCooler cooler = new()
            {
                Name = "None",
                Brand = "None",
                Type = "None",
                SocketId = 1,
                FanSpeed = 0,
                NoiseLevel = 0,
                Airflow = 0,
                Dimensions = "None",
                Price = 0,
                ImgUrl = "None"
            };
            CCList.Add(cooler);
            ComponentsDB.CPUCoolers.Add(cooler);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemCC()
        {   
            if (SelectedCC != null)
                ComponentsDB.CPUCoolers.Remove(SelectedCC); 

            ComponentsDB.SaveChanges();
            CCList.Remove(SelectedCC);
        }

        #endregion Cooling Commands

        #region Power Commands

        public ICommand AddCommandPS => new RelayCommand(AddItemPS);
        public ICommand RemoveCommandPS => new RelayCommand(RemoveItemPS);

        private void AddItemPS()
        {
            PowerSupply power = new()
            {
                Name = "None",
                Brand = "None",
                Wattage = 0,
                Efficiency = "None",
                Modularity = "None",
                FormFactor = "None",
                ConnectorTypes = "None",
                Price = 0,
                ImgUrl = "None"
            };
            PSList.Add(power);
            ComponentsDB.PowerSupplies.Add(power);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemPS()
        {   
            if (SelectedPS != null) 
                ComponentsDB.PowerSupplies.Remove(SelectedPS);

            ComponentsDB.SaveChanges();
            PSList.Remove(SelectedPS);
        }

        #endregion Power Commands

        #region Storage Commands

        public ICommand AddCommandStorage => new RelayCommand(AddItemStorage);
        public ICommand RemoveCommandStorage => new RelayCommand(RemoveItemStorage);

        private void AddItemStorage()
        {
            Storage storage = new()
            {
                Name = "None",
                Brand = "None",
                Capacity = "None",
                Interface = "None",
                FormFactor = "None",
                ReadSpeed = 0,
                WriteSpeed = 0,
                Price = 0,
                ImgUrl = "None"
            };
            StorageList.Add(storage);
            ComponentsDB.Storages.Add(storage);
            ComponentsDB.SaveChanges();
        }

        private void RemoveItemStorage()
        {   
            if (SelectedStorage != null) 
                ComponentsDB.Storages.Remove(SelectedStorage);

            ComponentsDB.SaveChanges();
            StorageList.Remove(SelectedStorage);
        }

        #endregion Storage Commands


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            if (combobox != null)
            {
                string selectedItem = (combobox.SelectedItem as ComboBoxItem)?.Content as string;

                foreach (DataGrid dataGrid in DataGrids)
                {
                    dataGrid.Visibility = (dataGrid.Tag as string) == selectedItem ? Visibility.Visible : Visibility.Hidden;
                }

                foreach (StackPanel stackPanel in StackPanels)
                {
                    stackPanel.Visibility = (stackPanel.Tag as string) == selectedItem ? Visibility.Visible : Visibility.Hidden;
                }
                CPUCollectionView.Refresh();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
