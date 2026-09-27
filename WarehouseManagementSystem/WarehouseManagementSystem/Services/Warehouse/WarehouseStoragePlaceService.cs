using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Models.Warehouse;

namespace WarehouseManagementSystem.Services.Warehouse
{
    public sealed class WarehouseStoragePlaceService : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<WarehousePlace> _warehousePlaces;

        public ObservableCollection<WarehousePlace> WarehousePlaces
        {
            get { return _warehousePlaces; }
            private set
            {
                _warehousePlaces = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehousePlaces)));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public WarehouseStoragePlaceService()
        {
           GetDefaultConfigurationWarehousePlaces();
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        public void GetDefaultConfigurationWarehousePlaces()
        {
            ObservableCollection<WarehousePlace> initializedList = new ObservableCollection<WarehousePlace>();
            for (int placeNumber = 1; placeNumber < 55; placeNumber++)
            {
                WarehousePlace place = new WarehousePlace(placeNumber, WarehouseStatePlace.Free);
                initializedList.Add(place);
            }

            WarehousePlaces = new ObservableCollection<WarehousePlace>(initializedList.OrderByDescending(wp => wp.PlaceNumber));
        }
        public bool SyncronizeStoredItemsWithDatabase(ObservableCollection<StoredItemDatabaseModel> loadedItems)
        {
            if (loadedItems == null)
            {
                return false;
            }

            foreach (StoredItemDatabaseModel item in loadedItems)
            {
                WarehousePlace searchedWarehousePlace = WarehousePlaces.Where(sp => sp.PlaceNumber == item.PlaceNumber).FirstOrDefault();
                if (searchedWarehousePlace == null)
                {
                    continue;
                }
                searchedWarehousePlace.PlaceNumber = item.PlaceNumber;
                searchedWarehousePlace.Status = WarehouseStatePlace.Occupied;
                if (item.InputTime.HasValue)
                {
                    searchedWarehousePlace.StoredPlaceItem = new StoredItem(item.SerialNumber, item.InputTime.Value);
                }
                else
                {
                    searchedWarehousePlace.StoredPlaceItem = new StoredItem(item.SerialNumber);
                }
            }
            return true;
        }
        public int GetNextFreeStoragePlace()
        {
            var freePlaces = WarehousePlaces.Where(wp => wp.Status == WarehouseStatePlace.Free).ToList();
            if(freePlaces.Count == 0)
            {
                return -1;
            }

            Random randomWarehousePlace = new Random();
            int indexWarehousePlace = randomWarehousePlace.Next(freePlaces.Count);
            int warehouseStoragePlace = freePlaces[indexWarehousePlace].PlaceNumber;

            return warehouseStoragePlace;
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
