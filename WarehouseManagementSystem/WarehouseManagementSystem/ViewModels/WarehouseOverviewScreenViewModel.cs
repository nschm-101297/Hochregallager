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
using WarehouseManagementSystem.Services.Warehouse;

namespace WarehouseManagementSystem.ViewModels
{
    public class WarehouseOverviewScreenViewModel : INotifyPropertyChanged
    {
        #region Properties
        private DatabaseService _databaseServiceClient;
        private WarehousePlace _selectedWarehousePlace;
        private WarehouseStoragePlaceService _storageDataService;

        public WarehouseStoragePlaceService StorageDataService
        {
            get { return _storageDataService; }
            set
            {
                _storageDataService = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StorageDataService)));
            }
        }

        public WarehousePlace SelectedWarehousePlace
        {
            get { return _selectedWarehousePlace; }
            set 
            { 
                _selectedWarehousePlace = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedWarehousePlace)));
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
            _databaseServiceClient = null;
            StorageDataService = null;
            SelectedWarehousePlace = null;
            DetailedViewVisible = Visibility.Collapsed;
            ShowDetailedView = new RelayCommand(ShowDetailedViewExecute, ShowDetailedViewCanExecute);
            CloseDetailedView = new RelayCommand(CloseDetailedViewExecute, CloseDetailedViewCanExecute);
            InitializeWarehousePlaces();
        }
        public WarehouseOverviewScreenViewModel(DatabaseService databaseService, WarehouseStoragePlaceService storagePlaceService)
        {
            _databaseServiceClient = databaseService;
            StorageDataService = storagePlaceService;
            SelectedWarehousePlace = null;
            DetailedViewVisible = Visibility.Collapsed;
            ShowDetailedView = new RelayCommand(ShowDetailedViewExecute, ShowDetailedViewCanExecute);
            CloseDetailedView = new RelayCommand(CloseDetailedViewExecute, CloseDetailedViewCanExecute);
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
            await LoadStoredItemsFromDatabase();
        }
        public async Task LoadStoredItemsFromDatabase()
        {
            ObservableCollection<StoredItemDatabaseModel> loadedItems = await _databaseServiceClient.GetStoredItems();
            StorageDataService.SyncronizeStoredItemsWithDatabase(loadedItems);
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
