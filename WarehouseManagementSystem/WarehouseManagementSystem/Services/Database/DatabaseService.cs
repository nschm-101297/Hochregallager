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
using WarehouseManagementSystem.Models.Orders;
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
        public async Task<ObservableCollection<Order>?> GetAllOrders()
        {
            if (_databaseConfiguration == null)
            {
                return null;
            }

            ObservableCollection<Order> storedItems = new ObservableCollection<Order>();
            await using SqlConnection databaseConnection = new SqlConnection(_databaseConfiguration.ConnectionString);
            await databaseConnection.OpenAsync();

            string queryString = "SELECT * FROM WarehouseManagement.ord.Orders";

            SqlCommand storedItemsCommand = new SqlCommand(queryString, databaseConnection);

            SqlDataReader readData = await storedItemsCommand.ExecuteReaderAsync();

            DataTable dataTableStoredItems = new DataTable();
            dataTableStoredItems.Load(readData);

            foreach (DataRow row in dataTableStoredItems.Rows)
            {
                Order readItem = new Order(
                    (int)row["OrderId"],
                    (OrderType)row["OrderType"],
                    (OrderPriority)row["OrderPriority"],
                    (OrderStatus)row["OrderStatus"],
                    (DateTime?)row["CreationDate"],
                    (DateTime?)row["DoneDate"]
                    );
                storedItems.Add(readItem);
            }

            return storedItems;
        }
        public async Task<int?> WriteOrder(Order addingOrder)
        {
            if (_databaseConfiguration == null)
            {
                return null;
            }

            await using SqlConnection databaseConnection = new SqlConnection(_databaseConfiguration.ConnectionString);
            await databaseConnection.OpenAsync();

            string writeInstruction = "INSERT INTO WarehouseManagement.ord.Orders" +
                                      "(OrderType,OrderPriority, OrderStatus, CreationDate)" +
                                      "VALUES (@OrderType, @OrderPriority, @OrderStatus, @CreationDate)" +
                                      "SELECT SCOPE_IDENTITY()";

            SqlCommand writeCommand = new SqlCommand(writeInstruction, databaseConnection);
            writeCommand.Parameters.AddWithValue("@OrderType", addingOrder.TypeOfOrder);
            writeCommand.Parameters.AddWithValue("@OrderPriority", addingOrder.Priority);
            writeCommand.Parameters.AddWithValue("@OrderStatus", addingOrder.Status);
            writeCommand.Parameters.AddWithValue("@CreationDate", addingOrder.CreationTime);
            object writeResult = await writeCommand.ExecuteScalarAsync(CancellationToken.None);

            if (writeResult == null || writeResult == DBNull.Value)
            {
                return -1;
            }
            return (int)(decimal)writeResult;
        }
        public async Task<bool?> DeleteOrder(Order deletingOrder)
        {
            if (_databaseConfiguration == null)
            {
                return null;
            }
            if (deletingOrder.ItemStatus != DatabaseItemStatus.Deleted)
            {
                return false;
            }

            await using SqlConnection databaseConnection = new SqlConnection(_databaseConfiguration.ConnectionString);
            await databaseConnection.OpenAsync();

            string writeInstruction = "Delete From WarehouseManagement.ord.Orders Where" +
                                      "(OrderId)" +
                                      "VALUES (@OrderId)";

            SqlCommand writeCommand = new SqlCommand(writeInstruction, databaseConnection);
            writeCommand.Parameters.AddWithValue("@OrderId", deletingOrder.OrderID);

            int affectedLines = await writeCommand.ExecuteNonQueryAsync(CancellationToken.None);

            return affectedLines > 0;
        }
        public async Task<bool?> ModifyOrder(Order modifingOrder)
        {
            if (_databaseConfiguration == null)
            {
                return null;
            }
            if(modifingOrder.ItemStatus != DatabaseItemStatus.Modified)
            {
                return false;
            }

            await using SqlConnection databaseConnection = new SqlConnection(_databaseConfiguration.ConnectionString);
            await databaseConnection.OpenAsync();

            string writeInstruction = "Update WarehouseManagement.ord.Orders" +
                                      "SET OrderType = @OrderType" +
                                      "OrderPriority = @OrderPriority" +
                                      "OrderStatus = @OrderStatus" +
                                      "CreationDate = @CreationDate" +
                                      "DoneDate = @DoneDate" +
                                      "WHERE OrderId = @OrderId";

            SqlCommand writeCommand = new SqlCommand(writeInstruction, databaseConnection);
            writeCommand.Parameters.AddWithValue("@OrderType", modifingOrder.TypeOfOrder);
            writeCommand.Parameters.AddWithValue("@OrderPriority", modifingOrder.Priority);
            writeCommand.Parameters.AddWithValue("@OrderStatus", modifingOrder.Status);
            writeCommand.Parameters.AddWithValue("@CreationDate", modifingOrder.CreationTime);
            if(modifingOrder.DoneTime != null)
            {
                writeCommand.Parameters.AddWithValue("@DoneDate", modifingOrder.DoneTime);
            }
            writeCommand.Parameters.AddWithValue("@OrderId", modifingOrder.OrderID);
            int affectedRows = await writeCommand.ExecuteNonQueryAsync(CancellationToken.None);

            return affectedRows > 0;
        }
        #endregion

        #region Interface-Methods

        #endregion
    }
}
