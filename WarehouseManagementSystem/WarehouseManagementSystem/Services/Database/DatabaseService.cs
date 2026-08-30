using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Models.Database;
using WarehouseManagementSystem.Models.Warehouse;
using System.Collections.ObjectModel;
using System.Data;

namespace WarehouseManagementSystem.Services.Database
{
    public sealed class DatabaseService
    {
        #region Properties
        private DatabaseConfiguration _databaseConfiguration;
        #endregion

        #region Events
        
        #endregion

        #region Constructors
        public DatabaseService()
        {
            GetDatabaseConfiguration();
        }
        #endregion

        #region Command-Methods

        #endregion

        #region Methods
        private void GetDatabaseConfiguration()
        {
            _databaseConfiguration = App.Configuration
                                     .GetSection("Database")
                                     .Get<DatabaseConfiguration>()
                                     ?? throw new InvalidOperationException(
                                         "Der Konfigurationsabschnitt 'Database' wurde nicht gefunden.");
        }
        public async Task<ObservableCollection<StoredItemDatabaseModel>?> GetStoredItems()
        {
            if(_databaseConfiguration == null)
            {
                return null;
            }

            ObservableCollection<StoredItemDatabaseModel> storedItems = new ObservableCollection<StoredItemDatabaseModel>();
            await using SqlConnection databaseConnection = new SqlConnection(_databaseConfiguration.ConnectionString);
            await databaseConnection.OpenAsync();

            string queryString = "SELECT * FROM WarehouseManagement.prod.StoredItems WHERE OutputTime IS NULL";

            SqlCommand storedItemsCommand = new SqlCommand(queryString, databaseConnection);

            SqlDataReader readData = await storedItemsCommand.ExecuteReaderAsync();

            DataTable dataTableStoredItems = new DataTable();
            dataTableStoredItems.Load(readData);

            foreach(DataRow row in dataTableStoredItems.Rows)
            {
                StoredItemDatabaseModel readItem = new StoredItemDatabaseModel(
                    (int)row["PlaceNumber"],
                    (int)row["SerialNumber"],
                    (DateTime)row["InputTime"]
                    );
                storedItems.Add(readItem);
            }

            return storedItems;
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
