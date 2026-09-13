using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WarehouseManagementSystem.Commands;
using WarehouseManagementSystem.Models.Orders;
using WarehouseManagementSystem.Views;

namespace WarehouseManagementSystem.ViewModels
{
    public class OrderEditorViewModel : INotifyPropertyChanged
    {
        #region Properties
        private OrderEditorScreen _screen;
        private Order _shownOrder;

        public Order ShownOrder
        {
            get { return _shownOrder; }
            set { _shownOrder = value; }
        }
        public ObservableCollection<String> Types { get; set; }
        public ObservableCollection<String> Prioriets { get; set; }
        public ObservableCollection<String> Statuses { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Confirm { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public OrderEditorViewModel(OrderEditorScreen screen)
        {
            _screen = screen;
            ShownOrder = new Order();
            Cancel = new RelayCommand(CancelExecute, CancelCanExecute);
            Confirm = new RelayCommand(ConfirmExecute, ConfirmCanExecute);
            InitializeTypes();
            InitializePriorities();
            InitializeStatues();
        }
        public OrderEditorViewModel(OrderEditorScreen screen, Order shownOrder)
        {
            _screen = screen;
            ShownOrder = new Order(shownOrder.OrderID, shownOrder.TypeOfOrder,
                                   shownOrder.Priority, shownOrder.Status,
                                   shownOrder.CreationTime,shownOrder.DoneTime);
            Cancel = new RelayCommand(CancelExecute, CancelCanExecute);
            Confirm = new RelayCommand(ConfirmExecute, ConfirmCanExecute);
            InitializeTypes();
            InitializePriorities();
            InitializeStatues();
        }
        #endregion

        #region Command-Methods
        public void CancelExecute(object par)
        {
            ShownOrder = null;
            _screen.DialogResult = false;
        }
        public bool CancelCanExecute(object par)
        {
            return true;
        }
        public void ConfirmExecute(object par)
        {
            _screen.DialogResult = true;
        }
        public bool ConfirmCanExecute(object par)
        {
            return ShownOrder.TypeOfOrder != OrderType.None &&
                   ShownOrder.Priority != OrderPriority.None && 
                   ShownOrder.Status != OrderStatus.Unknown;
        }
        #endregion

        #region Methods
        private void InitializeTypes()
        {
            Types = new ObservableCollection<String>();
            string[] membersTypes = Enum.GetNames(typeof(OrderType));
            if (membersTypes.Length > 0)
            {
                for (int i = 1; i < membersTypes.Length; i++)
                {
                    Types.Add(membersTypes[i]);
                }
            }
        }
        private void InitializePriorities()
        {
            Types = new ObservableCollection<String>();
            string[] membersTypes = Enum.GetNames(typeof(OrderType));
            if (membersTypes.Length > 0)
            {
                for (int i = 1; i < membersTypes.Length; i++)
                {
                    Types.Add(membersTypes[i]);
                }
            }
        }
        private void InitializeStatues()
        {
            Types = new ObservableCollection<String>();
            string[] membersTypes = Enum.GetNames(typeof(OrderType));
            if (membersTypes.Length > 0)
            {
                for (int i = 1; i < membersTypes.Length; i++)
                {
                    Types.Add(membersTypes[i]);
                }
            }
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
