using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;
using WarehouseManagementSystem.Models.ADS;

namespace WarehouseManagementSystem.Services.ADS
{
    public sealed class AdsClientService : INotifyPropertyChanged, IDisposable
    {
        #region Properties
        private bool _disposed;
        private string _amsNetId;
        private string _portNumber;
        private AdsClient _adsClient;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public EventHandler<ConnectionStateChangedEventArgs>? ConnectionStateChanged;
        #endregion

        #region Constructors
        public AdsClientService()
        {
            _adsClient = new AdsClient();
            _adsClient.ConnectionStateChanged += RuntimeClient_ConnectionStateChanged;
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        public void ClientConnect(string amsNetId, int portNumber)
        {
            _amsNetId = amsNetId;
            _portNumber = portNumber.ToString();
            AmsNetId netId = new AmsNetId(amsNetId);
            _adsClient.Connect(netId, portNumber);
        }
        public void ClientDisconnect()
        {
            _adsClient.Disconnect();
        }
        public async Task<AdsClientConnection> GetAdsConnection()
        {
            AdsClientConnection adsConncetionClient = new AdsClientConnection();

            adsConncetionClient.AmsNetID.CurrentValue = _adsClient.Address.ToString();
            var clientState = await _adsClient.ReadStateAsync(CancellationToken.None);
            if (clientState.Succeeded)
            {
                adsConncetionClient.AdsState.CurrentValue = clientState.State.AdsState.ToString();
                adsConncetionClient.DeviceState.CurrentValue = clientState.State.DeviceState.ToString();
            }
            adsConncetionClient.ChannelPortType.CurrentValue = _adsClient.ChannelPortType.ToString();
            adsConncetionClient.ChannelProtocol.CurrentValue = _adsClient.ChannelProtocol.ToString();
            adsConncetionClient.SourceAddress.CurrentValue = _adsClient.SourceAddress.ToString();
            adsConncetionClient.ConnectionTimeout.CurrentValue = _adsClient.Timeout.ToString();
            adsConncetionClient.ClientIsConnected.CurrentValue = _adsClient.IsConnected.ToString();
            adsConncetionClient.ClientIsDisposed.CurrentValue = _adsClient.IsDisposed.ToString();
            adsConncetionClient.ClientIsLocal.CurrentValue = _adsClient.IsLocal.ToString();

            return adsConncetionClient;
        }
        #endregion

        #region Interface-Methods
        public void Dispose()
        {
            if(_disposed) return;

            _adsClient.ConnectionStateChanged -= RuntimeClient_ConnectionStateChanged;
            ClientDisconnect();
            _adsClient.Dispose();

            _disposed = true;
        }
        #endregion

        #region Eventhandler
        private void RuntimeClient_ConnectionStateChanged(object? sender, ConnectionStateChangedEventArgs e)
        {
            ConnectionStateChanged?.Invoke(this, e);
        }
        #endregion
    }
}
