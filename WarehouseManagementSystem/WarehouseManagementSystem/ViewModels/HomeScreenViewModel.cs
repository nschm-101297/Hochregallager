using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WarehouseManagementSystem.Commands;

namespace WarehouseManagementSystem.ViewModels
{
    public class HomeScreenViewModel : INotifyPropertyChanged
    {
        #region Properties
        private MainWindowViewModel _parentViewModel;
        public ICommand ADSConnection { get; set; }
        public ICommand ADSVariableConnection { get; set; }
        public ICommand WarehouseOverview { get; set; }
        public ICommand OrderManagement { get; set; }
        public ICommand FreeCommandOne { get; set; }
        public ICommand FreeCommandTwo { get; set; }

        private Visibility _adsConnectionButton;

        public Visibility ADSConnectionButton
        {
            get { return _adsConnectionButton; }
            set 
            { 
                _adsConnectionButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ADSConnectionButton)));
            }
        }

        private Visibility _adsVariableConnectionButton;

        public Visibility ADSVariableConnectionButton
        {
            get { return _adsVariableConnectionButton; }
            set
            {
                _adsVariableConnectionButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ADSVariableConnectionButton)));
            }
        }

        private Visibility _warehouseOverviewButton;

        public Visibility WarehouseOverviewButton
        {
            get { return _warehouseOverviewButton; }
            set 
            { 
                _warehouseOverviewButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseOverviewButton)));
            }
        }

        private Visibility _orderManagementButton;

        public Visibility OrderManagementButton
        {
            get { return _orderManagementButton; }
            set 
            { 
                _orderManagementButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrderManagementButton)));
            }
        }

        private Visibility _freeCommandOneButton;

        public Visibility FreeCommandOneButton
        {
            get { return _freeCommandOneButton; }
            set 
            { 
                _freeCommandOneButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FreeCommandOneButton)));
            }
        }

        private Visibility _freeCommandTwoButton;

        public Visibility FreeCommandTwoButton
        {
            get { return _freeCommandTwoButton; }
            set
            {
                _freeCommandTwoButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FreeCommandTwoButton)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public HomeScreenViewModel()
        {
            ADSConnection = new RelayCommand(ADSConnectionExecute, ADSConnectionCanExecute);
            ADSVariableConnection = new RelayCommand(ADSVariableConnectionExecute, ADSVariableConnectionCanExecute);
            WarehouseOverview = new RelayCommand(WarehouseOverviewExecute, WarehouseOverviewCanExecute);
            OrderManagement = new RelayCommand(OrderManagementExecute, OrderManagementCanExecute);
            FreeCommandOne = new RelayCommand(FreeCommandOneExecute, FreeCommandOneCanExecute);
            FreeCommandTwo = new RelayCommand(FreeCommandTwoExecute, FreeCommandTwoCanExecute);
            ADSConnectionButton = Visibility.Visible;
            ADSVariableConnectionButton = Visibility.Visible;
            WarehouseOverviewButton = Visibility.Visible;
            OrderManagementButton = Visibility.Visible;
            FreeCommandOneButton = Visibility.Hidden;
            FreeCommandTwoButton = Visibility.Hidden;
            _parentViewModel = null;
        }
        public HomeScreenViewModel(MainWindowViewModel mainViewModel)
        {
            ADSConnection = new RelayCommand(ADSConnectionExecute, ADSConnectionCanExecute);
            ADSVariableConnection = new RelayCommand(ADSVariableConnectionExecute, ADSVariableConnectionCanExecute);
            WarehouseOverview = new RelayCommand(WarehouseOverviewExecute, WarehouseOverviewCanExecute);
            OrderManagement = new RelayCommand(OrderManagementExecute, OrderManagementCanExecute);
            FreeCommandOne = new RelayCommand(FreeCommandOneExecute, FreeCommandOneCanExecute);
            FreeCommandTwo = new RelayCommand(FreeCommandTwoExecute, FreeCommandTwoCanExecute);
            ADSConnectionButton = Visibility.Visible;
            ADSVariableConnectionButton = Visibility.Visible;
            WarehouseOverviewButton = Visibility.Visible;
            OrderManagementButton = Visibility.Visible;
            FreeCommandOneButton = Visibility.Hidden;
            FreeCommandTwoButton = Visibility.Hidden;
            _parentViewModel = mainViewModel;
        }
        #endregion

        #region Command-Methods
        public void ADSConnectionExecute(object par)
        {
            
        }
        public bool ADSConnectionCanExecute(object par)
        {
            return ADSConnectionButton == Visibility.Visible;
        }
        public void ADSVariableConnectionExecute(object par)
        {

        }
        public bool ADSVariableConnectionCanExecute(object par)
        {
            return ADSVariableConnectionButton == Visibility.Visible;
        }
        public void WarehouseOverviewExecute(object par)
        {

        }
        public bool WarehouseOverviewCanExecute(object par)
        {
            return WarehouseOverviewButton == Visibility.Visible;
        }
        public void OrderManagementExecute(object par)
        {

        }
        public bool OrderManagementCanExecute(object par)
        {
            return OrderManagementButton == Visibility.Visible;
        }
        public void FreeCommandOneExecute(object par)
        {

        }
        public bool FreeCommandOneCanExecute(object par)
        {
            return FreeCommandOneButton == Visibility.Visible;
        }
        public void FreeCommandTwoExecute(object par)
        {

        }
        public bool FreeCommandTwoCanExecute(object par)
        {
            return FreeCommandTwoButton == Visibility.Visible;
        }
        #endregion

        #region Methods

        #endregion

        #region Interface-Methods

        #endregion
    }
}
