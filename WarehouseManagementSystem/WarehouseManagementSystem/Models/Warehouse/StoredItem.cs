using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.Warehouse
{
    public class StoredItem : INotifyPropertyChanged
    {
        #region Properties
        private int _serialNumber;

        public int SerialNumber
        {
            get { return _serialNumber; }
            set 
            { 
                _serialNumber = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SerialNumber)));
            }
        }
        private DateTime? _inputTime;

        public DateTime? InputTime
        {
            get { return _inputTime; }
            set 
            { 
                _inputTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputTime)));
            }
        }
        private DateTime? _outputTime;

        public DateTime? OutputTime
        {
            get { return _outputTime; }
            set 
            { 
                _outputTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OutputTime)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public StoredItem()
        {
            SerialNumber = 0;
            InputTime = null;
            OutputTime = null;
        }
        public StoredItem(int serialNumber)
        {
            SerialNumber = serialNumber;
            InputTime = null;
            OutputTime = null;
        }
        public StoredItem(int serialNumber, DateTime inputTime)
        {
            SerialNumber = serialNumber;
            InputTime = inputTime;
            OutputTime = null;
        }
        public StoredItem(int serialNumber, DateTime inputTime, DateTime outputTime)
        {
            SerialNumber = serialNumber;
            InputTime = inputTime;
            OutputTime = outputTime;
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
