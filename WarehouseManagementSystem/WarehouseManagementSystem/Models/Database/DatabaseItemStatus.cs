using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Models.Database
{
    public enum DatabaseItemStatus
    {
        None,
        Modified,
        Unchanged,
        Added,
        Deleted,
    }
}
