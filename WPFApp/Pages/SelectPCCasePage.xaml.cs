using Library.CustomEventArgs;
using Library.Models;
using Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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

namespace WPFApp.Pages
{
    public partial class SelectPCCasePage : ObservableUserControl
    {
        public event EventHandler<SelectedPCCaseEventArgs>? PCCaseSelectedHandler;

        public ObservableCollection<PCCase> PCCaseList { get; set; } = [];

        public ComponentsDBContext ComponentsDB { get; set; }

        private PCCase _selectedCase { get; set; }

        public PCCase SelectedCase
        {
            get => _selectedCase;
            set
            {
                _selectedCase = value;
                OnPropertyChanged(nameof(SelectedCase));
            }
        }
        public SelectPCCasePage()
        {
            DataContext = this;
            ComponentsDB = new ComponentsDBContext();
            InitializeComponent();
            PropertyChanged += Refresh;
        }

        private void Refresh(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedCase))
            {
                if (SelectedCase != null && SelectedCase.Id != -1)
                {
                    tbBrand.Text = SelectedCase.Brand;
                    tbType.Text = SelectedCase.Type;
                    tbColor.Text = SelectedCase.Brand;
                    tbDimensoins.Text = SelectedCase.Dimensions;
                    tbFormFactor.Text = SelectedCase.FormFactor;
                    tbPrice.Text = SelectedCase.Price.ToString();
                    string? img_src = SelectedCase.ImgUrl;
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
            tbColor.Text = string.Empty;
            tbDimensoins.Text = string.Empty;
            tbFormFactor.Text = string.Empty;
            tbPrice.Text = string.Empty;
            ImgUrl.Visibility = Visibility.Hidden;
        }

        public void SetUp(int caseId = -1)
        {
            PCCaseList.Clear();
            PCCase empty = new() { Id = -1, Name = "None" };
            PCCaseList.Add(empty);
            foreach (PCCase Case in ComponentsDB.PCCases.ToList())
            {
                PCCaseList.Add(Case);
            }
            PCCase? sel = PCCaseList.FirstOrDefault(Case => Case.Id == caseId);
            if (sel != null)
            {
                ListBoxCase.SelectedItem = sel;
            }
            else
            {
                ListBoxCase.SelectedItem = empty;
            }
        }

        private void ButtonChooseComponent_Click(object sender, RoutedEventArgs e)
        {
            PCCaseSelectedHandler?.Invoke(this, new SelectedPCCaseEventArgs(SelectedCase));
        }
    }
}
