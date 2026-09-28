using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagementSystem.Models.Database;
using WarehouseManagementSystem.Models.Orders;

namespace WarehouseManagementSystem.Services.Orders
{
    public sealed class OrderManagementService : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Order>? _orders;

        public ObservableCollection<Order>? Orders
        {
            get { return _orders; }
            private set
            {
                _orders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Orders)));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public OrderManagementService()
        {
            Orders = new ObservableCollection<Order>();
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        public bool SyncronizeOrdersWithDatabase(ObservableCollection<Order> loadedOrders)
        {
            if (loadedOrders == null)
            {
                return false;
            }

            foreach (Order order in loadedOrders)
            {
                Orders.Add(order);
            }

            return true;
        }
        public bool AddNewOrder(Order orderToAdd)
        {
            if(orderToAdd.OrderID == 0)
            {
                return false;
            }
            orderToAdd.ItemStatus = DatabaseItemStatus.Unchanged;
            Orders?.Add(orderToAdd);
            return true;
        }
        public bool DeleteOrder(Order orderToRemove)
        {
            if(orderToRemove.ItemStatus != DatabaseItemStatus.Deleted)
            {
                return false;
            }
            Orders.Remove(orderToRemove);
            return true;
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
