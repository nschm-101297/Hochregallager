using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WarehouseManagementSystem.Models.Filter
{
    public class Filtergroup : INotifyPropertyChanged
    {
        #region Properties
        private bool _filterTypeInfeedActive;

        public bool FilterTypeInfeedActive
        {
            get { return _filterTypeInfeedActive; }
            set
            {
                _filterTypeInfeedActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterTypeInfeedActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterTypeOutfeedActive;

        public bool FilterTypeOutfeedActive
        {
            get { return _filterTypeOutfeedActive; }
            set
            {
                _filterTypeOutfeedActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterTypeOutfeedActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterPriorityLowActive;

        public bool FilterPriorityLowActive
        {
            get { return _filterPriorityLowActive; }
            set
            {
                _filterPriorityLowActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterPriorityLowActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterPriorityMiddleActive;

        public bool FilterPriorityMiddleActive
        {
            get { return _filterPriorityMiddleActive; }
            set
            {
                _filterPriorityMiddleActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterPriorityMiddleActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterPriorityHighActive;

        public bool FilterPriorityHighActive
        {
            get { return _filterPriorityHighActive; }
            set
            {
                _filterPriorityHighActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterPriorityHighActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterStatusOpenActive;

        public bool FilterStatusOpenActive
        {
            get { return _filterStatusOpenActive; }
            set
            {
                _filterStatusOpenActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterStatusOpenActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterStatusInProgressActive;

        public bool FilterStatusInProgressActive
        {
            get { return _filterStatusInProgressActive; }
            set
            {
                _filterStatusInProgressActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterStatusInProgressActive)));
                ShownItemCollection?.Refresh();
            }
        }
        private bool _filterStatusDoneActive;

        public bool FilterStatusDoneActive
        {
            get { return _filterStatusDoneActive; }
            set
            {
                _filterStatusDoneActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilterStatusDoneActive)));
                ShownItemCollection?.Refresh();
            }
        }
        public ListCollectionView ShownItemCollection { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public Filtergroup()
        {
            FilterTypeInfeedActive = false;
            FilterTypeOutfeedActive = false;
            FilterPriorityLowActive = false;
            FilterPriorityMiddleActive = false;
            FilterPriorityHighActive = false;
            FilterStatusOpenActive = false;
            FilterStatusInProgressActive = false;
            FilterStatusDoneActive = false;
        }
        public Filtergroup(ListCollectionView shownView)
        {
            FilterTypeInfeedActive = false;
            FilterTypeOutfeedActive = false;
            FilterPriorityLowActive = false;
            FilterPriorityMiddleActive = false;
            FilterPriorityHighActive = false;
            FilterStatusOpenActive = false;
            FilterStatusInProgressActive = false;
            FilterStatusDoneActive = false;
            ShownItemCollection = shownView;
        }
        #endregion

        #region Command-Methods
        public void InitializeFiltergroup()
        {
            FilterTypeInfeedActive = true;
            FilterTypeOutfeedActive = false;
            FilterPriorityLowActive = true;
            FilterPriorityMiddleActive = false;
            FilterPriorityHighActive = false;
            FilterStatusOpenActive = true;
            FilterStatusInProgressActive = false;
            FilterStatusDoneActive = false;
        }
        public void InitializeFiltergroup(bool filterTypeInfeedActive, bool filterTypeOutfeedActive,
            bool filterPriorityLowActive, bool filterPriorityMiddleActive, bool filterPriorityHighActive,
            bool filterStatusOpenActive, bool filterStatusInProgressActive, bool filterStatusDoneActive)
        {
            FilterTypeInfeedActive = filterTypeInfeedActive;
            FilterTypeOutfeedActive = filterTypeOutfeedActive;
            FilterPriorityLowActive = filterPriorityLowActive;
            FilterPriorityMiddleActive = filterPriorityMiddleActive;
            FilterPriorityHighActive = filterPriorityHighActive;
            FilterStatusOpenActive = filterStatusOpenActive;
            FilterStatusInProgressActive = filterStatusInProgressActive;
            FilterStatusDoneActive = filterStatusDoneActive;
        }
        #endregion

        #region Methods

        #endregion

        #region Interface-Methods

        #endregion
    }
}
