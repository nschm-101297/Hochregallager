using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.PLC
{
    public enum ValueChangedAction
    {
        None, 
        RequestNewOrder, 
        FinishedOrder
    }
}
