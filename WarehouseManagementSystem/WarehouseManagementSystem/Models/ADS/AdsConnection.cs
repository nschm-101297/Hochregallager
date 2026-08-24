using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.ADS
{
    public sealed class AdsConnection : INotifyPropertyChanged
    {
        #region Properties
        private IAdsConnectionProperty _amsNetID;

        public IAdsConnectionProperty AmsNetID
        {
            get { return _amsNetID; }
            set { _amsNetID = value; }
        }
        private IAdsConnectionProperty _adsState;

        public IAdsConnectionProperty AdsState
        {
            get { return _adsState; }
            set { _adsState = value; }
        }
        private IAdsConnectionProperty _deviceState;

        public IAdsConnectionProperty DeviceState
        {
            get { return _deviceState; }
            set { _deviceState = value; }
        }
        private IAdsConnectionProperty _channelPortType;

        public IAdsConnectionProperty ChannelPortType
        {
            get { return _channelPortType; }
            set { _channelPortType = value; }
        }
        private IAdsConnectionProperty _channelProtocol;

        public IAdsConnectionProperty ChannelProtocol
        {
            get { return _channelProtocol; }
            set { _channelProtocol = value; }
        }
        private IAdsConnectionProperty _sourceAddress;

        public IAdsConnectionProperty SourceAddress
        {
            get { return _sourceAddress; }
            set { _sourceAddress = value; }
        }
        private IAdsConnectionProperty _connectionTimeout;

        public IAdsConnectionProperty ConnectionTimeout
        {
            get { return _connectionTimeout; }
            set { _connectionTimeout = value; }
        }
        private IAdsConnectionProperty _clientIsConnected;

        public IAdsConnectionProperty ClientIsConnected
        {
            get { return _clientIsConnected; }
            set { _clientIsConnected = value; }
        }
        private IAdsConnectionProperty _clientIsDisposed;

        public IAdsConnectionProperty ClientIsDisposed
        {
            get { return _clientIsDisposed; }
            set { _clientIsDisposed = value; }
        }
        private IAdsConnectionProperty _clientIsLocal;

        public IAdsConnectionProperty ClientIsLocal
        {
            get { return _clientIsLocal; }
            set { _clientIsLocal = value; }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public AdsConnection()
        {
            AmsNetID = new AdsConnectionProperty("Ams-Net-ID:", "0.0.0.0.0.0");
            AdsState = new AdsConnectionProperty("ADS state:", "Idle");
            DeviceState = new AdsConnectionProperty("Status device:", "0");
            ChannelPortType = new AdsConnectionProperty("Type Port:", "ADS");
            ChannelProtocol = new AdsConnectionProperty("Protocol:", "ADS");
            SourceAddress = new AdsConnectionProperty("Source address:", "0.0.0.0");
            ConnectionTimeout = new AdsConnectionProperty("Timeout connection:", "0s");
            ClientIsConnected = new AdsConnectionProperty("Is connected:", "False");
            ClientIsDisposed = new AdsConnectionProperty("Is disposed:", "True");
            ClientIsLocal = new AdsConnectionProperty("Local client:", "False");
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        public ObservableCollection<IAdsConnectionProperty> GetObjectAsList()
        {
            ObservableCollection<IAdsConnectionProperty> allProperties = new ObservableCollection<IAdsConnectionProperty>();

            allProperties.Add(AmsNetID);
            allProperties.Add(AdsState);
            allProperties.Add(DeviceState);
            allProperties.Add(ChannelPortType);
            allProperties.Add(ChannelProtocol);
            allProperties.Add(SourceAddress);
            allProperties.Add(ConnectionTimeout);
            allProperties.Add(ClientIsConnected);
            allProperties.Add(ClientIsDisposed);
            allProperties.Add(ClientIsLocal);

            return allProperties;
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
