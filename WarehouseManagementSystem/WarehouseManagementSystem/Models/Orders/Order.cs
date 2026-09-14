using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Models.Database;

namespace WarehouseManagementSystem.Models.Orders
{
    public class Order : INotifyPropertyChanged
    {
        #region Properties
        public int OrderID { get; private set; }
        private OrderType _typeOfOrder;

        public OrderType TypeOfOrder
        {
            get { return _typeOfOrder; }
            set 
            { 
                _typeOfOrder = value; 
                ItemStatus = DatabaseItemStatus.Modified;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TypeOfOrder)));
            }
        }
        private OrderPriority _priority;

        public OrderPriority Priority
        {
            get { return _priority; }
            set 
            { 
                _priority = value;
                ItemStatus = DatabaseItemStatus.Modified;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Priority)));
            }
        }
        private OrderStatus _status;

        public OrderStatus Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                ItemStatus = DatabaseItemStatus.Modified;
                if(value == OrderStatus.Done)
                {
                    DoneTime = DateTime.Now;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }
        public DateTime? CreationTime { get; private set; }
        public DateTime? DoneTime { get; private set; }
        public DatabaseItemStatus ItemStatus { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public Order() 
        {
            OrderID = 0;
            CreationTime = DateTime.Now;
            ItemStatus = DatabaseItemStatus.Added;
        }
        public Order(OrderType type,  OrderPriority priority, OrderStatus status)
        {
            OrderID = 0;
            TypeOfOrder = type;
            Priority = priority;
            Status = status;
            CreationTime = DateTime.Now;
            ItemStatus = DatabaseItemStatus.Added;
        }
        public Order(int orderID, OrderType type, OrderPriority priority, OrderStatus status, DateTime? creationTime, DateTime? doneTime)
        {
            OrderID = orderID;
            TypeOfOrder = type;
            Priority = priority;
            Status = status;
            if (creationTime != null)
            {
                CreationTime = creationTime;
            }
            if (doneTime != null)
            {
                DoneTime = doneTime;
            }
            ItemStatus = DatabaseItemStatus.Unchanged;
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods

        #endregion

        #region Interface-Methods

        #endregion
    }
}
