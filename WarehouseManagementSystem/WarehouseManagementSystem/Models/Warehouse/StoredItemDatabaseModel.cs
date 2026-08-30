using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.Warehouse
{
    public class StoredItemDatabaseModel
    {
        #region Properties
        private int _placeNumber;

        public int PlaceNumber
        {
            get { return _placeNumber; }
            set { _placeNumber = value; }
        }

        private int _serialNumber;

        public int SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; }
        }
        private DateTime? _inputTime;

        public DateTime? InputTime
        {
            get { return _inputTime; }
            set { _inputTime = value; }
        }
        private DateTime? _outputTime;

        public DateTime? OutputTime
        {
            get { return _outputTime; }
            set { _outputTime = value; }
        }

        #endregion

        #region Events

        #endregion

        #region Constructors
        public StoredItemDatabaseModel()
        {
            PlaceNumber = 0;
            SerialNumber = 0;
            InputTime = null;
            OutputTime = null;
        }
        public StoredItemDatabaseModel(int serialNumber)
        {
            PlaceNumber = 0;
            SerialNumber = serialNumber;
            InputTime = null;
            OutputTime = null;
        }
        public StoredItemDatabaseModel(int placeNumber, int serialNumber, DateTime inputTime)
        {
            PlaceNumber = placeNumber;
            SerialNumber = serialNumber;
            InputTime = inputTime;
            OutputTime = null;
        }
        public StoredItemDatabaseModel(int placeNumber, int serialNumber, DateTime inputTime, DateTime outputTime)
        {
            PlaceNumber = placeNumber;
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
