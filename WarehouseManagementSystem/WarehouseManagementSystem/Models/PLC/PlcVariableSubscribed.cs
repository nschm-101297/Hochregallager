using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.PLC
{
    public class PlcVariableSubscribed<T> : INotifyPropertyChanged
    {
        #region Properties
        private PlcVariable<T> _subscribedVariable;

        public PlcVariable<T> SubscribedVariable
        {
            get { return _subscribedVariable; }
            private set 
            { 
                _subscribedVariable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubscribedVariable)));
            }
        }
        private ValueChangedAction _reactionValueChanged;
        [Category("Subscription")]
        [DisplayName("Reaction value changed")]
        public ValueChangedAction ReactionValueChanged
        {
            get { return _reactionValueChanged; }
            set 
            { 
                _reactionValueChanged = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReactionValueChanged)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSubscribed)));
            }
        }
        [Category("Subscription")]
        [DisplayName("Is subscribed")]
        [ReadOnly(true)]
        public bool IsSubscribed
        {
            get { return ReactionValueChanged != ValueChangedAction.None; }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public PlcVariableSubscribed(PlcVariable<T> subscribedVariable, ValueChangedAction reactionValueChanged)
        {
            SubscribedVariable = subscribedVariable;
            ReactionValueChanged = reactionValueChanged;
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
