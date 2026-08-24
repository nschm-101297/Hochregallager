using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.ADS
{
    public interface IAdsConnectionProperty
    {
        public string Description { get; set; }
        public string CurrentValue { get; set; }
    }
}
