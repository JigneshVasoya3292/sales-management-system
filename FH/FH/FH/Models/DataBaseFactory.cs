// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataBaseFactory.cs" company="Dextech">
//   Copyright (C) 2013 DexTech
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows;
    using System.Windows.Threading;

    using Globalization = FH.Resources.ResourceStrings;

    /// <summary>
    /// The data base factory.
    /// </summary>
    public class DataBaseFactory
    {
        /// <summary>
        /// Insert Inventory Stored Procedure name.
        /// </summary>
        private const string InsertInventorySPString = "InsertInventory";

        /// <summary>
        /// All Inventory Stored Procedure name.
        /// </summary>
        private const string AllInventorySPString = "AllInventory";

        /// <summary>
        /// Get Credentials Stored Procedure name.
        /// </summary>
        private const string GetCredentialsSPString = "GetCredentials";

        /// <summary>
        /// Update Inventory Stored Procedure name.
        /// </summary>
        private const string UpdateInventorySPString = "UpdateInventory";

        /// <summary>
        /// Get Pieces Stored Procedure name.
        /// </summary>
        private const string GetPiecesSPString = "GetPieces";

        /// <summary>
        /// Insert Sold Items Stored Procedure name.
        /// </summary>
        private const string InsertSoldItemsSPString = "InsertSoldItems";

        /// <summary>
        /// Insert Sold Items Stored Procedure name.
        /// </summary>
        private const string UpdateInvoiceIdSPString = "UpdateInvoiceId";

        /// <summary>
        /// Insert Sold Items Stored Procedure name.
        /// </summary>
        private const string GetInvoiceIdSPString = "GetInvoiceId";

        /// <summary>
        /// The connection string.
        /// </summary>
        private readonly string connectionString; // = @"Data Source=VJEEG-VAIO;Initial Catalog=InventoryDB;Integrated Security=True";

        /// <summary>
        /// The database connection.
        /// </summary>
        private SqlConnection databaseConnectionToInventoryDb;

        /// <summary>
        /// The insert inventory stored procedure.
        /// </summary>
        private SqlCommand insertInventoryCommand;

        /// <summary>
        /// The All inventory stored procedure.
        /// </summary>
        private SqlCommand allInventoryCommand;

        /// <summary>
        /// The update inventory stored procedure.
        /// </summary>
        private SqlCommand updateInventoryCommand;

        /// <summary>
        /// The Get Credentials stored procedure.
        /// </summary>
        private SqlCommand getCredentialsCommand;

        /// <summary>
        /// The get Pieces stored procedure.
        /// </summary>
        private SqlCommand getPiecesCommand;

        /// <summary>k
        /// The get Pieces stored procedure.
        /// </summary>
        private SqlCommand insertSoldItemsCommand;

        /// <summary>
        /// The update invoice id command.
        /// </summary>
        private SqlCommand updateInvoiceIdCommand;

        /// <summary>
        /// The get invoice id command.
        /// </summary>
        private SqlCommand getInvioceIdCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseFactory"/> class.
        /// </summary>
        public DataBaseFactory()
        {
            // Log it
            this.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Method Which connects to Database
        /// </summary>
        public void SetupDataBase()
        {
            this.databaseConnectionToInventoryDb = new SqlConnection(this.connectionString);
            this.insertInventoryCommand = new SqlCommand(InsertInventorySPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.allInventoryCommand = new SqlCommand(AllInventorySPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.updateInventoryCommand = new SqlCommand(UpdateInventorySPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.getPiecesCommand = new SqlCommand(GetPiecesSPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.insertSoldItemsCommand = new SqlCommand(InsertSoldItemsSPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.getCredentialsCommand = new SqlCommand(GetCredentialsSPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.updateInvoiceIdCommand = new SqlCommand(UpdateInvoiceIdSPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
            this.getInvioceIdCommand = new SqlCommand(GetInvoiceIdSPString, this.databaseConnectionToInventoryDb) { CommandType = CommandType.StoredProcedure };
        }

        /// <summary>
        /// The add to inventory table.
        /// </summary>
        /// <param name="inventoryItem">
        /// The inventory item.
        /// </param>
        public void AddToInventoryTable(IInventoryItem inventoryItem)
        {
            this.insertInventoryCommand.Parameters.AddWithValue("@Brand", inventoryItem.BrandName);
            this.insertInventoryCommand.Parameters.AddWithValue("@StyleCode", inventoryItem.StyleCode);
            this.insertInventoryCommand.Parameters.AddWithValue("@Price", inventoryItem.PurchasePrice);
            this.insertInventoryCommand.Parameters.AddWithValue("@Pieces", inventoryItem.NoofPieces);
            this.insertInventoryCommand.Parameters.AddWithValue("@SellingPrice", inventoryItem.SellingPrice);
            this.insertInventoryCommand.Parameters.AddWithValue("@Sold", 0);
            this.insertInventoryCommand.Parameters.AddWithValue("@Date", DateTime.Now);

            try
            {
                this.databaseConnectionToInventoryDb.Open();
                this.insertInventoryCommand.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.ADD_TO_INVENTORY_EXCEPTION);
            }
            finally
            {
                this.databaseConnectionToInventoryDb.Close();
            }

            this.insertInventoryCommand.Parameters.Clear();
        }

        /// <summary>
        /// Method Which gets all the stock items from database
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>ObservableCollection</cref>
        ///     </see>
        ///     .
        /// </returns>
        public ObservableCollection<IInventoryItem> GetAllStockItemsFromDatabase()
        {
            var dataSet = new DataSet();
            var stockItems = new ObservableCollection<IInventoryItem>();
            try
            {
                this.databaseConnectionToInventoryDb.Open();
                var dataAdapter = new SqlDataAdapter(this.allInventoryCommand);
                dataAdapter.Fill(dataSet);
                this.databaseConnectionToInventoryDb.Close();
            }
            catch (SqlException)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDb.Close();
            }

            // TODO: Do not hard coded values to iterate through Table. use Foreach or something else.
            if (dataSet.Tables.Count > 0)
            {
                var totalItems = dataSet.Tables[0].Rows.Count;
                if (totalItems > 0)
                {
                    foreach (DataRow item in dataSet.Tables[0].Rows)
                    {
                        if (item.ItemArray.Length >= 4)
                        {
                            var brandName = Convert.ToString(item.ItemArray[0]);
                            var styleCode = Convert.ToString(item.ItemArray[1]);
                            var priceperPiece = Convert.ToDouble(item.ItemArray[2]);
                            var pieces = Convert.ToInt32(item.ItemArray[3]);
                            var sellingPrice = Convert.ToDouble(item.ItemArray[4]);

                            stockItems.Add(
                       new InventoryItem
                       {
                           BrandName = brandName,
                           NoofPieces = pieces,
                           PurchasePrice = priceperPiece,
                           StyleCode = styleCode,
                           SellingPrice = sellingPrice
                       });
                        }
                    }
                }
            }

            return stockItems;
        }

        /// <summary>
        /// The update inventory data.
        /// </summary>
        /// <param name="soldItems">
        /// The sold items.
        /// </param>
        /// <param name="pieces">
        /// No of Pieces
        /// </param>
        public void UpdateNoofPiecesInInventory(IList<ISalesItem> soldItems, int pieces = 0)
        {
            // TODO : Write a SP
            // TODO : Dispatcher
            foreach (var soldItem in soldItems)
            {
                int? noOfPiecestoUpdate = pieces;
                if (pieces <= 0)
                {
                    noOfPiecestoUpdate = this.GetNoOfPieces(soldItem.BrandName, soldItem.StyleCode); // TODO: Optimization
                    if (noOfPiecestoUpdate == null)
                    {
                        // TODO : Log it
                        continue;
                    }

                    noOfPiecestoUpdate = noOfPiecestoUpdate - soldItem.NoofPiecesSold;
                }

                this.updateInventoryCommand.Parameters.AddWithValue("@Brand", soldItem.BrandName);
                this.updateInventoryCommand.Parameters.AddWithValue("@StyleCode", soldItem.StyleCode);
                this.updateInventoryCommand.Parameters.AddWithValue("@Price", soldItem.PurchasePrice); // Do not use this Purchase price. use it like Price = Pur_price/no.ofsold
                this.updateInventoryCommand.Parameters.AddWithValue("@Pieces", noOfPiecestoUpdate);
                this.updateInventoryCommand.Parameters.AddWithValue("@Sold", soldItem.NoofPiecesSold);

                // TODO : Optimization by movig it Out
                try
                {
                    this.databaseConnectionToInventoryDb.Open();
                    this.updateInventoryCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // TODO: Handle exception.
                }
                finally
                {
                    this.databaseConnectionToInventoryDb.Close();
                }

                this.updateInventoryCommand.Parameters.Clear();
            }

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.AddSoldItemsintoDb(soldItems)));
        }

        /// <summary>
        /// The get credentials from DB.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>ObservableCollection</cref>
        ///     </see>
        ///     .
        /// </returns>
        public ObservableCollection<string> GetCredentialsFromDb()
        {
            var dataSet = new DataSet();
            var credentials = new ObservableCollection<string>();
            try
            {
                this.databaseConnectionToInventoryDb.Open();
                var dataAdapter = new SqlDataAdapter(this.getCredentialsCommand);
                dataAdapter.Fill(dataSet);
                this.databaseConnectionToInventoryDb.Close();
            }
            catch (SqlException ex)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDb.Close();
            }

            // TODO: Do not hard coded values to iterate through Table. use Foreach or something else.
            if (dataSet.Tables.Count > 0)
            {
                var totalItems = dataSet.Tables[0].Rows.Count;
                if (totalItems >= 1)
                {
                    if (dataSet.Tables[0].Rows[0].ItemArray.Length > 0)
                    {
                        var userName = Convert.ToString(dataSet.Tables[0].Rows[0].ItemArray[0]);
                        userName = userName.Trim();
                        credentials.Add(userName);
                    }

                    if (dataSet.Tables[0].Rows[0].ItemArray.Length > 1)
                    {
                        var password = Convert.ToString(dataSet.Tables[0].Rows[0].ItemArray[1]);
                        password = password.Trim();
                        credentials.Add(password);
                    }
                }
            }

            return credentials;
        }

        /// <summary>
        /// The update last invoice id.
        /// </summary>
        /// <param name="invoiceId">
        /// The invoice id.
        /// </param>
        public void UpdateLastInvoiceId(long invoiceId)
        {
            this.updateInvoiceIdCommand.Parameters.AddWithValue("@InvoiceKey", "FH");
            this.updateInvoiceIdCommand.Parameters.AddWithValue("@LastInvoiceId", invoiceId);

            // TODO : Optimization by movig it Out
            try
            {
                this.databaseConnectionToInventoryDb.Open();
                this.updateInvoiceIdCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDb.Close();
            }

            this.updateInvoiceIdCommand.Parameters.Clear();
        }

        /// <summary>
        /// The get last invoice id.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long GetLastInvoiceId()
        {
            long lastInvoiceId = 1;

            var dataSet = new DataSet();
            try
            {
                this.databaseConnectionToInventoryDb.Open();
                var dataAdapter = new SqlDataAdapter(this.getInvioceIdCommand);
                dataAdapter.Fill(dataSet);
                this.databaseConnectionToInventoryDb.Close();
            }
            catch (SqlException ex)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDb.Close();
            }

            // TODO: Do not hard coded values to iterate through Table. use Foreach or something else.
            if (dataSet.Tables.Count > 0)
            {
                var totalItems = dataSet.Tables[0].Rows.Count;
                if (totalItems >= 1)
                {
                    if (dataSet.Tables[0].Rows[0].ItemArray.Length > 1)
                    {
                        lastInvoiceId = Convert.ToInt64(dataSet.Tables[0].Rows[0].ItemArray[1]);
                    }
                }
            }

            return lastInvoiceId;
        }

        /// <summary>
        /// The add sold items into DB.
        /// </summary>
        /// <param name="soldItems">
        /// The sold items.
        /// </param>
        private void AddSoldItemsintoDb(IList<ISalesItem> soldItems)
        {
            foreach (var soldItem in soldItems)
            {
                this.insertSoldItemsCommand.Parameters.AddWithValue("@Brand", soldItem.BrandName);
                this.insertSoldItemsCommand.Parameters.AddWithValue("@StyleCode", soldItem.StyleCode);
                this.insertSoldItemsCommand.Parameters.AddWithValue("@PurchasePrice", soldItem.PurchasePrice);
                this.insertSoldItemsCommand.Parameters.AddWithValue("@Price", soldItem.SellingPrice);
                this.insertSoldItemsCommand.Parameters.AddWithValue("@Pieces", soldItem.NoofPiecesSold);
                this.insertSoldItemsCommand.Parameters.AddWithValue("@Date", DateTime.Now);

                // TODO : Optimization by movig it Out
                try
                {
                    this.databaseConnectionToInventoryDb.Open();
                    this.insertSoldItemsCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    // TODO: Handle exception.
                }
                finally
                {
                    this.databaseConnectionToInventoryDb.Close();
                }

                this.insertSoldItemsCommand.Parameters.Clear();
            }
        }

        /// <summary>
        /// The get no of pieces.
        /// </summary>
        /// <param name="brandName">
        /// Brand Name
        /// </param>
        /// <param name="styleCode">
        /// The style code.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int? GetNoOfPieces(string brandName, string styleCode)
        {
            var dataSet = new DataSet();
            int? noOfPieces = null;
            try
            {
                this.databaseConnectionToInventoryDb.Open();
                this.getPiecesCommand.Parameters.AddWithValue("@Brand", brandName);
                this.getPiecesCommand.Parameters.AddWithValue("@StyleCode", styleCode);
                this.getPiecesCommand.CommandType = CommandType.StoredProcedure;

                var adp = new SqlDataAdapter(this.getPiecesCommand);

                adp.Fill(dataSet);
            }
            catch (SqlException ex)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDb.Close();
            }

            this.getPiecesCommand.Parameters.Clear();

            if (dataSet.Tables.Count > 0)
            {
                var totalItems = dataSet.Tables[0].Rows.Count;
                if (totalItems >= 1)
                {
                    if (dataSet.Tables[0].Rows[0].ItemArray.Length > 0)
                    {
                        noOfPieces = Convert.ToInt32(dataSet.Tables[0].Rows[0].ItemArray[0]);
                    }
                }
            }

            return noOfPieces;
        }
    }
}
