using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Models.Warehouse;
using WarehouseManagementSystem.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WarehouseManagementSystem.Services.Database;

namespace WarehouseManagementSystem.ViewModels
{
    public class WarehouseOverviewScreenViewModel : INotifyPropertyChanged
    {
        #region Properties
        private DatabaseService _databaseServiceClient;
        private WarehousePlace _selectedWarehousePlace;

        public WarehousePlace SelectedWarehousePlace
        {
            get { return _selectedWarehousePlace; }
            set 
            { 
                _selectedWarehousePlace = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWarehousePlace)));
            }
        }
        private ObservableCollection<WarehousePlace> _warehousePlaces;

        public ObservableCollection<WarehousePlace> WarehousePlaces
        {
            get { return _warehousePlaces; }
            set 
            { 
                _warehousePlaces = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehousePlaces)));
            }
        }
        private Visibility _detailedViewVisible;

        public Visibility DetailedViewVisible
        {
            get { return _detailedViewVisible; }
            set 
            { 
                _detailedViewVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DetailedViewVisible)));
            }
        }

        public ICommand ShowDetailedView { get; set; }
        public ICommand CloseDetailedView { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public WarehouseOverviewScreenViewModel()
        {
            SelectedWarehousePlace = null;
            WarehousePlaces = new ObservableCollection<WarehousePlace>();
            DetailedViewVisible = Visibility.Collapsed;
            ShowDetailedView = new RelayCommand(ShowDetailedViewExecute, ShowDetailedViewCanExecute);
            CloseDetailedView = new RelayCommand(CloseDetailedViewExecute, CloseDetailedViewCanExecute);
            App app = (App)Application.Current;
            _databaseServiceClient = app?.DatabaseClient;
            InitializeWarehousePlaces();
        }
        #endregion

        #region Command-Methods
        public void ShowDetailedViewExecute(object par)
        {
            DetailedViewVisible = Visibility.Visible;
            SelectedWarehousePlace = (WarehousePlace)par;
        }
        public bool ShowDetailedViewCanExecute(object par)
        {
            return true;
        }
        public void CloseDetailedViewExecute(object par)
        {
            DetailedViewVisible = Visibility.Collapsed;
        }
        public bool CloseDetailedViewCanExecute(object par)
        {
            return true;
        }
        #endregion

        #region Methods
        public async Task InitializeWarehousePlaces()
        {
            GetDefaultConfigurationWarehousePlaces();
            await LoadStoredItemsFromDatabase();
        }
        public void GetDefaultConfigurationWarehousePlaces()
        {
            for (int placeNumber = 1; placeNumber < 55; placeNumber++)
            {
                WarehousePlace place = new WarehousePlace(placeNumber, WarehouseStatePlace.Free);
                WarehousePlaces.Add(place);
            }

            WarehousePlaces = new ObservableCollection<WarehousePlace>(WarehousePlaces.OrderByDescending(wp => wp.PlaceNumber));
        }
        public async Task LoadStoredItemsFromDatabase()
        {
            if(_databaseServiceClient == null)
            {
                return;
            }

            ObservableCollection<StoredItemDatabaseModel> loadedItems = await _databaseServiceClient.GetStoredItems();
            if(loadedItems == null)
            {
                return;
            }

            foreach(StoredItemDatabaseModel item in loadedItems)
            {
                WarehousePlace searchedWarehousePlace = WarehousePlaces.Where(sp => sp.PlaceNumber == item.PlaceNumber).FirstOrDefault();
                if(searchedWarehousePlace == null)
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
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
