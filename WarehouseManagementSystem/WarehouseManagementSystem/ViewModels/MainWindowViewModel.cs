using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WarehouseManagementSystem.Commands;

namespace WarehouseManagementSystem.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string _header;

        public string Header
        {
            get { return _header; }
            set 
            { 
                _header = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Header)));
            }
        }

        private object _subScreen;

        public object SubScreen
        {
            get { return _subScreen; }
            set 
            { 
                _subScreen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubScreen)));
            }
        }
        public ICommand GoToHome { get; set; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            Header = "Home";
            GoToHome = new RelayCommand(GoToHomeExecute, GoToHomeCanExecute);
            SubScreen = new HomeScreenViewModel(this);
        }
        #endregion

        #region Command-Methods
        public void GoToHomeExecute(object par)
        {
            SubScreen = new HomeScreenViewModel(this);
        }
        public bool GoToHomeCanExecute(object par)
        {
            return true;
        }
        #endregion

        #region Methods

        #endregion

        #region Interface-Methods

        #endregion
    }
}
