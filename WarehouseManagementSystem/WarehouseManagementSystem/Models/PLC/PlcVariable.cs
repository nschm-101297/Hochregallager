using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.PLC
{
    public class PlcVariable<T> : INotifyPropertyChanged
    {
        #region Properties
        private string _variableName = String.Empty;

        public string VariableName
        {
            get { return _variableName = String.Empty; }
            private set 
            { 
                _variableName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VariableName)));
            }
        }
        private string _variablePath = String.Empty;

        public string VariablePath
        {
            get { return _variablePath; }
            private set 
            { 
                _variablePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VariablePath)));
            }
        }
        private string _nameVariableList = String.Empty;

        public string NameVariableList
        {
            get { return _nameVariableList; }
            private set 
            { 
                _nameVariableList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameVariableList)));
            }
        }
        private string _dataTypeName = typeof(T).Name;

        public string DataTypeName
        {
            get { return _dataTypeName; }
            private set 
            { 
                _dataTypeName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataTypeName)));
            }
        }
        private T? _readValue;

        public T? ReadValue
        {
            get { return _readValue; }
            private set 
            { 
                _readValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReadValue)));
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public PlcVariable(string variableName, string variablePath, string nameVariableList)
        {
            VariableName = variableName;
            VariablePath = variablePath;
            NameVariableList = nameVariableList;
        }
        public PlcVariable(string variableName, string variablePath, string nameVariableList, T readValue)
        {
            VariableName = variableName;
            VariablePath = variablePath;
            NameVariableList = nameVariableList;
            ReadValue = readValue;
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
