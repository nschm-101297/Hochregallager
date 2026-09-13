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
using WarehouseManagementSystem.Views;
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
                ShownOrders?.Refresh();
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
            ShownOrders.Filter = new Predicate<object>(FilterOrder);
            ShownOrders?.Refresh();
        }
        #endregion

        #region Command-Methods
        public async void AddNewOrderExecute(object par)
        {
            OrderEditorScreen editor = new OrderEditorScreen();
            OrderEditorViewModel editorViewModel = new OrderEditorViewModel(editor);
            editor.DataContext = editorViewModel;
            editor.Owner = Application.Current.MainWindow;
            editor.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? editorResult = editor.ShowDialog();
            if (!editorResult.HasValue || editorResult.Value == false)
            {
                return;
            }

            ShownOrders.AddNewItem(editorViewModel.ShownOrder);
            ShownOrders.Refresh();
        }
        public bool AddNewOrderCanExecute(object par)
        {
            Order selectedOrder = par as Order;
            return selectedOrder is not null;
        }
        public async void OpenEditingViewExecute(object par)
        {
            Order selectedOrder = par as Order;
            if(selectedOrder == null)
            {
                return;
            }
            OrderEditorScreen editor = new OrderEditorScreen();
            OrderEditorViewModel editorViewModel = new OrderEditorViewModel(editor, selectedOrder);
            editor.DataContext = editorViewModel;
            editor.Owner = Application.Current.MainWindow;
            editor.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? editorResult = editor.ShowDialog();
            if (!editorResult.HasValue || editorResult.Value == false)
            {
                return;
            }

            selectedOrder.TypeOfOrder = editorViewModel.ShownOrder.TypeOfOrder;
            selectedOrder.Priority = editorViewModel.ShownOrder.Priority;
            selectedOrder.Status = editorViewModel.ShownOrder.Status;
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
            bool? result = await LoadOrdersFromDatabase();
            if (!result.HasValue || result.Value == false)
            {
                Orders = new ObservableCollection<Order>();
                ShownOrders = new ListCollectionView(Orders);
            }
        }
        private async Task<bool?> LoadOrdersFromDatabase()
        {
            if (_databaseServiceClient == null)
            {
                return null;
            }

            Orders = await _databaseServiceClient.GetAllOrders();

            if (Orders == null)
            {
                MessageBox.Show("Loading doesn't worked!",
                                "Loading deleting",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }

            ShownOrders = new ListCollectionView(Orders);
            return true;
        }
        public bool FilterOrder(object ord)
        {
            Order order = ord as Order;
            if (order == null)
            {
                return false;
            }

            if (OrConditionActive)
            {
                return OrCondition(order);
            }
            else
            {
                return AndCondition(order);
            }

        }
        private bool OrCondition(Order ord)
        {
            bool resultOrderType = FilterOrderType(ord);
            bool resultOrderPriority = FilterOrderPriority(ord);
            bool resultOrderStatus = FilterOrderStatus(ord);

            return resultOrderType || 
                   resultOrderPriority ||
                   resultOrderStatus;
        }
        private bool AndCondition(Order ord)
        {
            bool resultOrderType = (!FilterTypeInfeedActive && !FilterTypeOutfeedActive) || 
                                    FilterOrderType(ord);
            bool resultOrderPriority = (!FilterPriorityLowActive && !FilterPriorityMiddleActive && !FilterPriorityHighActive) ||
                                    FilterOrderPriority(ord);
            bool resultOrderStatus = (!FilterStatusOpenActive && !FilterStatusInProgressActive && !FilterStatusDoneActive) || 
                                    FilterOrderStatus(ord);

            return resultOrderType &&
                   resultOrderPriority &&
                   resultOrderStatus;
        }
        private bool FilterOrderType(Order ord)
        {
            return ord.TypeOfOrder switch
            {
                OrderType.None => false,
                OrderType.Infeed => FilterTypeInfeedActive,
                OrderType.Outfeed => FilterTypeOutfeedActive,
                _ => false
            };
        }
        private bool FilterOrderPriority(Order ord)
        {
            return ord.Priority switch
            {
                OrderPriority.None => false,
                OrderPriority.Low => FilterPriorityLowActive,
                OrderPriority.Middle => FilterPriorityMiddleActive,
                OrderPriority.High => FilterPriorityHighActive,
                _ => false
            };
        }
        private bool FilterOrderStatus(Order ord)
        {
            return ord.Status switch
            {
                OrderStatus.Unknown => false,
                OrderStatus.Open => FilterStatusOpenActive,
                OrderStatus.InProgress => FilterStatusInProgressActive,
                OrderStatus.Done => FilterStatusDoneActive,
                _ => false
            };
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
