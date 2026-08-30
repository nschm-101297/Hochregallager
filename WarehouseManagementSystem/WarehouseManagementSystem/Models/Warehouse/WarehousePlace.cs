using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.Warehouse
{
    public class WarehousePlace : INotifyPropertyChanged
    {
        #region Properties
        private int _placeNumber;

        public int PlaceNumber
        {
            get { return _placeNumber; }
            set 
            { 
                _placeNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaceNumber)));
            }
        }
        private WarehouseStatePlace _status;

        public WarehouseStatePlace Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }
        private StoredItem? _storedPlaceItem;

        public StoredItem? StoredPlaceItem
        {
            get { return _storedPlaceItem; }
            set 
            { 
                _storedPlaceItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StoredPlaceItem)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public WarehousePlace()
        {
            PlaceNumber = 0;
            Status = WarehouseStatePlace.Free;
            StoredPlaceItem = null;
        }
        public WarehousePlace(int placeNumber, WarehouseStatePlace status)
        {
            PlaceNumber = placeNumber;
            Status = status;
            StoredPlaceItem = null;
        }
        public WarehousePlace(int placeNumber, WarehouseStatePlace status, StoredItem storedPlaceItem)
        {
            PlaceNumber = placeNumber;
            Status = status;
            StoredPlaceItem = storedPlaceItem;
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
