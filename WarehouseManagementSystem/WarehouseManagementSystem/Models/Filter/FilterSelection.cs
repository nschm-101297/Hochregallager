using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WarehouseManagementSystem.Models.Filter
{
    public class FilterSelection : INotifyPropertyChanged
    {
        #region Properties
        private bool _orConditionActive;

        public bool OrConditionActive
        {
            get { return _orConditionActive; }
            set
            {
                _orConditionActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrConditionActive)));
                if (value)
                {
                    AndConditionActive = false;
                    OrFilters?.InitializeFiltergroup(true, true, true,true, true,
                                                    true, true, true);
                }
                ShownItemCollection?.Refresh();
            }
        }
        private bool _andConditionActive;

        public bool AndConditionActive
        {
            get { return _andConditionActive; }
            set
            {
                _andConditionActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AndConditionActive)));
                if (value)
                {
                    OrConditionActive = false;
                    AndFilters?.InitializeFiltergroup(true, false, true, false, false,
                                                    true, false, false);
                }
                ShownItemCollection?.Refresh();
            }
        }
        private Filtergroup _orFilters;

        public Filtergroup OrFilters
        {
            get { return _orFilters; }
            set 
            { 
                _orFilters = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (nameof(OrFilters)));
            }
        }
        private Filtergroup _andFilters;

        public Filtergroup AndFilters
        {
            get { return _andFilters; }
            set
            {
                _andFilters = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AndFilters)));
            }
        }
        public ListCollectionView ShownItemCollection { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public FilterSelection()
        {
            OrConditionActive = true;
            AndConditionActive = false;
            OrFilters = new Filtergroup();
            AndFilters = new Filtergroup();
            OrFilters?.InitializeFiltergroup(true, true, true, true, true,
                                                    true, true, true);
        }
        public FilterSelection(ListCollectionView shownView)
        {
            OrConditionActive = true;
            AndConditionActive = false;
            OrFilters = new Filtergroup(shownView);
            AndFilters = new Filtergroup(shownView);
            ShownItemCollection = shownView;
            OrFilters?.InitializeFiltergroup(true, true, true, true, true,
                                                    true, true, true);
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
