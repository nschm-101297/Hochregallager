using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Commands;
using WarehouseManagementSystem.Services.Database;
using WarehouseManagementSystem.Models.Orders;
using WarehouseManagementSystem.Models.Database;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;
using WarehouseManagementSystem.Commands;
using WarehouseManagementSystem.Views;
using WarehouseManagementSystem.Models.Filter;
using System.Windows;

namespace WarehouseManagementSystem.ViewModels
{
    public class OrderManagementViewModel : INotifyPropertyChanged
    {
        #region Properties
        private DatabaseService _databaseServiceClient;
        
        private FilterSelection _filterOrders;

        public FilterSelection FilterOrders
        {
            get { return _filterOrders; }
            set { _filterOrders = value; }
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
            AddNewOrder = new RelayCommand(AddNewOrderExecute, AddNewOrderCanExecute);
            OpenEditingView = new RelayCommand(OpenEditingViewExecute, OpenEditingViewCanExecute);
            DeleteOrder = new RelayCommand(DeleteOrderExecute, DeleteOrderCanExecute);
            Orders = new ObservableCollection<Order>();
            ShownOrders = new ListCollectionView(Orders);
            FilterOrders = new FilterSelection(ShownOrders);
            ShownOrders.Filter = new Predicate<object>(FilterOrder);
            _ = InitializeOrders();
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

            int? writtingDatabaseResult = await _databaseServiceClient.WriteOrder(editorViewModel.ShownOrder);
            if (!writtingDatabaseResult.HasValue || writtingDatabaseResult.Value == 0)
            {
                MessageBox.Show("Writting to database doesn't work!",
                                "Error database",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            editorViewModel.ShownOrder.OrderAddedToDatabase(writtingDatabaseResult.Value);
            editorViewModel.ShownOrder.ItemStatus = DatabaseItemStatus.Unchanged;
            Orders?.Add(editorViewModel.ShownOrder);
            ShownOrders.Refresh();
        }
        public bool AddNewOrderCanExecute(object par)
        {
            return true;
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
            
            bool? updatingDatabaseResult = await _databaseServiceClient.ModifyOrder(selectedOrder);
            if (!updatingDatabaseResult.HasValue || updatingDatabaseResult.Value == false)
            {
                MessageBox.Show("Updating database doesn't work!",
                                "Error database",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            selectedOrder.ItemStatus = DatabaseItemStatus.Unchanged;
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
            selectedOrder.ItemStatus = DatabaseItemStatus.Deleted;
            bool? deleteResult = await _databaseServiceClient.DeleteOrder(selectedOrder);
            if (!deleteResult.HasValue || deleteResult.Value==false)
            {
                MessageBox.Show("Deleting doesn't worked!",
                                "Error deleting",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            Orders.Remove(selectedOrder);
            ShownOrders.Refresh();
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
        }
        private async Task<bool?> LoadOrdersFromDatabase()
        {
            if (_databaseServiceClient == null)
            {
                return null;
            }

            ObservableCollection<Order> loadedOrders = await _databaseServiceClient.GetAllOrders();

            if (loadedOrders == null)
            {
                MessageBox.Show("Loading doesn't worked!",
                                "Loading deleting",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return false;
            }

            foreach (Order order in loadedOrders)
            {
                Orders.Add(order);
            }
            return true;
        }
        public bool FilterOrder(object ord)
        {
            Order order = ord as Order;
            if (order == null)
            {
                return false;
            }

            if (FilterOrders.OrConditionActive)
            {
                return OrCondition(order, FilterOrders.OrFilters);
            }
            else
            {
                return AndCondition(order, FilterOrders.AndFilters);
            }

        }
        private bool OrCondition(Order ord, Filtergroup filtergroup)
        {
            bool resultOrderType = FilterOrderType(ord, filtergroup);
            bool resultOrderPriority = FilterOrderPriority(ord, filtergroup);
            bool resultOrderStatus = FilterOrderStatus(ord, filtergroup);

            return resultOrderType || 
                   resultOrderPriority ||
                   resultOrderStatus;
        }
        private bool AndCondition(Order ord, Filtergroup filtergroup)
        {
            bool resultOrderType = (!filtergroup.FilterTypeInfeedActive && !filtergroup.FilterTypeOutfeedActive) || 
                                    FilterOrderType(ord, filtergroup);
            bool resultOrderPriority = (!filtergroup.FilterPriorityLowActive && !filtergroup.FilterPriorityMiddleActive && !filtergroup.FilterPriorityHighActive) ||
                                    FilterOrderPriority(ord, filtergroup);
            bool resultOrderStatus = (!filtergroup.FilterStatusOpenActive && !filtergroup.FilterStatusInProgressActive && !filtergroup.FilterStatusDoneActive) || 
                                    FilterOrderStatus(ord, filtergroup);

            return resultOrderType &&
                   resultOrderPriority &&
                   resultOrderStatus;
        }
        private bool FilterOrderType(Order ord, Filtergroup filtergroup)
        {
            return ord.TypeOfOrder switch
            {
                OrderType.None => false,
                OrderType.Infeed => filtergroup.FilterTypeInfeedActive,
                OrderType.Outfeed => filtergroup.FilterTypeOutfeedActive,
                _ => false
            };
        }
        private bool FilterOrderPriority(Order ord, Filtergroup filtergroup)
        {
            return ord.Priority switch
            {
                OrderPriority.None => false,
                OrderPriority.Low => filtergroup.FilterPriorityLowActive,
                OrderPriority.Middle => filtergroup.FilterPriorityMiddleActive,
                OrderPriority.High => filtergroup.FilterPriorityHighActive,
                _ => false
            };
        }
        private bool FilterOrderStatus(Order ord, Filtergroup filtergroup)
        {
            return ord.Status switch
            {
                OrderStatus.Unknown => false,
                OrderStatus.Open => filtergroup.FilterStatusOpenActive,
                OrderStatus.InProgress => filtergroup.FilterStatusInProgressActive,
                OrderStatus.Done => filtergroup.FilterStatusDoneActive,
                _ => false
            };
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
