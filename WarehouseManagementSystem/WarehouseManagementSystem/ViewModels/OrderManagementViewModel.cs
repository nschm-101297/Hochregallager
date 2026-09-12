using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Commands;
using WarehouseManagementSystem.Services.Database;
using WarehouseManagementSystem.Models.Orders;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;
using WarehouseManagementSystem.Commands;
using System.Windows;

namespace WarehouseManagementSystem.ViewModels
{
    public class OrderManagementViewModel : INotifyPropertyChanged
    {
        #region Properties
        private DatabaseService _databaseServiceClient;
        private bool _orConditionActive;

        public bool OrConditionActive
        {
            get { return _orConditionActive; }
            set 
            { 
                _orConditionActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrConditionActive)));
            }
        }
        private bool _andConditionActive;

        public bool AndConditionActive
        {
            get { return _andConditionActive; }
            set 
            { 
                _andConditionActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AndConditionActive)));
            }
        }
        private bool _filterTypeInfeedActive;

        public bool FilterTypeInfeedActive
        {
            get { return _filterTypeInfeedActive; }
            set 
            { 
                _filterTypeInfeedActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterTypeInfeedActive)));
            }
        }
        private bool _filterTypeOutfeedActive;

        public bool FilterTypeOutfeedActive
        {
            get { return _filterTypeOutfeedActive; }
            set 
            { 
                _filterTypeOutfeedActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterTypeOutfeedActive)));
            }
        }
        private bool _filterPriorityLowActive;

        public bool FilterPriorityLowActive
        {
            get { return _filterPriorityLowActive; }
            set 
            { 
                _filterPriorityLowActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterPriorityLowActive)));
            }
        }
        private bool _filterPriorityMiddleActive;

        public bool FilterPriorityMiddleActive
        {
            get { return _filterPriorityMiddleActive; }
            set 
            { 
                _filterPriorityMiddleActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterPriorityMiddleActive)));
            }
        }
        private bool _filterPriorityHighActive;

        public bool FilterPriorityHighActive
        {
            get { return _filterPriorityHighActive; }
            set 
            { 
                _filterPriorityHighActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterPriorityHighActive)));
            }
        }
        private bool _filterStatusOpenActive;

        public bool FilterStatusOpenActive
        {
            get { return _filterStatusOpenActive; }
            set 
            { 
                _filterStatusOpenActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterStatusOpenActive)));
            }
        }
        private bool _filterStatusInProgressActive;

        public bool FilterStatusInProgressActive
        {
            get { return _filterStatusInProgressActive; }
            set 
            { 
                _filterStatusInProgressActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterStatusInProgressActive)));
            }
        }
        private bool _filterStatusDoneActive;

        public bool FilterStatusDoneActive
        {
            get { return _filterStatusDoneActive; }
            set 
            { 
                _filterStatusDoneActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterStatusDoneActive)));
            }
        }
        public ICommand AddNewOrder { get; set; }
        public ICommand OpenEditingView { get; set; }
        public ICommand DeleteOrder { get; set; }

        private ObservableCollection<Order>? _orders;

        public ObservableCollection<Order>? Orders
        {
            get { return _orders; }
            set 
            { 
                _orders = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Orders)));
            }
        }
        private ListCollectionView _shownOrders;

        public ListCollectionView ShownOrders
        {
            get { return _shownOrders; }
            set 
            { 
                _shownOrders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShownOrders)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public OrderManagementViewModel()
        {
            App app = (App)Application.Current;
            _databaseServiceClient = app?.DatabaseClient;
            OrConditionActive = true;
            AndConditionActive = false;
            FilterTypeInfeedActive = true;
            FilterTypeOutfeedActive = true;
            FilterPriorityLowActive = true;
            FilterPriorityMiddleActive = true;
            FilterPriorityHighActive = true;
            FilterStatusOpenActive = true;
            FilterStatusInProgressActive = true;
            FilterStatusDoneActive = true;
            AddNewOrder = new RelayCommand(AddNewOrderExecute, AddNewOrderCanExecute);
            OpenEditingView = new RelayCommand(OpenEditingViewExecute, OpenEditingViewCanExecute);
            DeleteOrder = new RelayCommand(DeleteOrderExecute, DeleteOrderCanExecute);
            InitializeOrders();
        }
        #endregion

        #region Command-Methods
        public async void AddNewOrderExecute(object par)
        {

        }
        public bool AddNewOrderCanExecute(object par)
        {
            Order selectedOrder = par as Order;
            return selectedOrder is not null;
        }
        public async void OpenEditingViewExecute(object par)
        {

        }
        public bool OpenEditingViewCanExecute(object par)
        {
            Order selectedOrder = par as Order;
            return selectedOrder is not null;
        }
        public async void DeleteOrderExecute(object par)
        {
            if(_databaseServiceClient == null)
            {
                return;
            }

            Order selectedOrder = par as Order;
            if (selectedOrder == null)
            {
                return;
            }

            bool? deleteResult = await _databaseServiceClient.DeleteOrder(selectedOrder);
            if (!deleteResult.HasValue || deleteResult.Value==false)
            {
                MessageBox.Show("Deleting doesn't worked!",
                                "Error deleting",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
        }
        public bool DeleteOrderCanExecute(object par)
        {
            Order selectedOrder = par as Order;
            return selectedOrder is not null;
        }
        #endregion

        #region Methods
        private async Task InitializeOrders()
        {
            if (_databaseServiceClient == null)
            {
                return;
            }

            Orders = await _databaseServiceClient.GetAllOrders();

            if(Orders == null)
            {
                MessageBox.Show("Loading doesn't worked!",
                                "Loading deleting",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            ShownOrders = new ListCollectionView(Orders);
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
