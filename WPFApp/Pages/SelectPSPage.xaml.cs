using Library.CustomEventArgs;
using Library.Models;
using Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp.Pages
{
    public partial class SelectPSPage : ObservableUserControl
    {
        public event EventHandler<SelectedPSEventArgs>? PSSelectedHandler;

        public ObservableCollection<PowerSupply> PSList { get; set; } = [];

        public PowerSupply? SelectedPS
        {
            get => _selectedPS;
            set
            {
                _selectedPS = value;
                OnPropertyChanged(nameof(SelectedPS));
            }
        }
        private PowerSupply? _selectedPS;

        public ComponentsDBContext ComponentsDB { get; set; }

        public SelectPSPage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedPS))
            {
                if (SelectedPS != null && SelectedPS.Id != -1)
                {
                    tbBrand.Text = SelectedPS.Brand;
                    tbWattage.Text = SelectedPS.Wattage.ToString();
                    tbEfficiency.Text = SelectedPS.Efficiency;
                    tbBModularity.Text = SelectedPS.Modularity;
                    tbFormFactor.Text = SelectedPS.FormFactor;
                    tbConnectorTypes.Text = SelectedPS.ConnectorTypes;
                    tbPrice.Text = SelectedPS.Price.ToString();

                    string? img_src = SelectedPS.ImgUrl;
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
            tbWattage.Text = string.Empty;
            tbEfficiency.Text = string.Empty;
            tbBModularity.Text = string.Empty;
            tbFormFactor.Text = string.Empty;
            tbConnectorTypes.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }
        public void SetUp(int psId = -1)
        {
            PSList.Clear();
            PowerSupply empty = new() { Id = -1, Name = "None" };
            PSList.Add(empty);
            foreach (PowerSupply PS in ComponentsDB.PowerSupplies.ToList())
            {
                PSList.Add(PS);
            }
            PowerSupply? sel = PSList.FirstOrDefault(PS => PS.Id == psId);
            if (sel != null)
            {
                ListBoxPS.SelectedItem = sel;
            }
            else
            {
                ListBoxPS.SelectedItem = empty;
            }
        }
        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            PSSelectedHandler?.Invoke(this, new SelectedPSEventArgs(SelectedPS));
        }
    }
}
