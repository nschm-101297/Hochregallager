using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarehouseManagementSystem.Models.ADS;
using WarehouseManagementSystem.Services.ADS;

namespace WarehouseManagementSystem.ViewModels
{
    public class ADSConnectionScreenViewModel : INotifyPropertyChanged
    {
        #region Properties
        private AdsClientService _clientService;
        private AdsClientConnection _adsClientConnection;

        public AdsClientConnection AdsClientConnection
        {
            get { return _adsClientConnection; }
            set 
            { 
                _adsClientConnection = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdsClientConnection)));
            }
        }
        private ObservableCollection<IAdsConnectionProperty> _adsClientConnectionProperties;

        public ObservableCollection<IAdsConnectionProperty> AdsClientConnectionProperties
        {
            get { return _adsClientConnectionProperties; }
            set 
            { 
                _adsClientConnectionProperties = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdsClientConnectionProperties)));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public ADSConnectionScreenViewModel()
        {
            App app = (App)Application.Current;
            _clientService = app?.AdsClient;
            InitializeConnectionData();
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        private async Task InitializeConnectionData()
        {
            await GetAdsConnectionClient();
            GetClientConnectionAsList();
        }
        private async Task GetAdsConnectionClient()
        {
            if (_clientService == null)
            {
                AdsClientConnection = new AdsClientConnection();
                return;
            }
            AdsClientConnection = await _clientService.GetAdsConnection();
        }
        private void GetClientConnectionAsList()
        {
            if (AdsClientConnection == null)
            {
                AdsClientConnectionProperties = new ObservableCollection<IAdsConnectionProperty>();
                return;
            }
            AdsClientConnectionProperties = AdsClientConnection.GetObjectAsList();
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
