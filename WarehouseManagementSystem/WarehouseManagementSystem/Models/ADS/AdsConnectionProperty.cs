using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.ADS
{
    public sealed class AdsConnectionProperty : INotifyPropertyChanged, IAdsConnectionProperty
    {
        #region Properties
        private string _description;

        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }
        private string _currentValue;

        public string CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentValue)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public AdsConnectionProperty()
        {
            Description = "-";
            CurrentValue = "-";
        }
        public AdsConnectionProperty(string description, string currentValue)
        {
            Description = description;
            CurrentValue = currentValue;
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
