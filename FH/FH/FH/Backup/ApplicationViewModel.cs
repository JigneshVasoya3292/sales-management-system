// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationViewModel.cs" company="XYZ">
//   Copyright (C) 2014 Motorola Solutions, Inc.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using FH.Models;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;
    using Globalization = FH.Resources.ResourceStrings;

    /// <summary>
    /// The application view model.
    /// </summary>
    public class ApplicationViewModel : NotificationObject
    {
        /// <summary>
        /// The connection string.
        /// </summary>
        private const string ConnectionStringForInventoryDB = @"Data Source=VJEEG-VAIO;Initial Catalog=InventoryDB;Integrated Security=True;";

        /// <summary>
        /// Insert Inventory Stored Procedure name.
        /// </summary>
        private const string InsertInventorySPString = "InsertInventory";

        /// <summary>
        /// Insert Inventory Stored Procedure name.
        /// </summary>
        private const string InsertCredentialsSPString = "InsertCredentials";

        /// <summary>
        /// All Inventory Stored Procedure name.
        /// </summary>
        private const string AllInventorySPString = "AllInventory";

        /// <summary>
        /// The style codes for brands.
        /// </summary>
        private readonly Dictionary<string, ObservableCollection<string>> styleCodesforBrands;

        /// <summary>
        /// The style codes for brands.
        /// </summary>
        private readonly Dictionary<string, double> pricePerPieceforStyleCodes;

        /// <summary>
        /// The brand name for inventory.
        /// </summary>
        private string brandNameForInventory;

        /// <summary>
        /// The style code for inventory.
        /// </summary>
        private string styleCodeForInventory;

        /// <summary>
        /// The no of pieces for inventory.
        /// </summary>
        private int noofPiecesForInventory;

        /// <summary>
        /// The total amount for all items.
        /// </summary>
        private double totalAmountForAllItems;

        /// <summary>
        /// selected item for selling.
        /// </summary>
        private string selectedBrandforSelling;

        /// <summary>
        /// The selected style code for selling.
        /// </summary>
        private string selectedStyleCodeforSelling;

        /// <summary>
        /// Private variable for selling price.
        /// </summary>
        private double sellingPrice;

        /// <summary>
        /// the total amount of sales.
        /// </summary>
        private double salesTotal;

        /// <summary>
        /// the total amount of sales.
        /// </summary>
        private double salesTotalWithoutDiscount;

        /// <summary>
        /// Private variable for price per piece.
        /// </summary>
        private double pricePerPiece;

        /// <summary>
        /// Private variable for Discount Rate.
        /// </summary>
        private int discountRate;

        /// <summary>
        /// Private variable for Discount Amount.
        /// </summary>
        private double discountedAmount;

        /// <summary>
        /// Private variable for available Stock Items collection.
        /// </summary>
        private ObservableCollection<IInventoryItem> stockItems;

        /// <summary>
        /// Private variable for available Brands collection.
        /// </summary>
        private ObservableCollection<string> availableBrands;

        /// <summary>
        /// Private variable for available style code collection.
        /// </summary>
        private ObservableCollection<string> availableStyleCodes;

        /// <summary>
        /// The database connection.
        /// </summary>
        private SqlConnection databaseConnectionToInventoryDB;

        /// <summary>
        /// The database connection.
        /// </summary>
        private SqlConnection databaseConnectionToCredentialsDB;

        /// <summary>
        /// The insert inventory stored procedure.
        /// </summary>
        private SqlCommand insertInventoryCommand;

        /// <summary>
        /// The All inventory stored procedure.
        /// </summary>
        private SqlCommand allInventoryCommand;

        /// <summary>
        /// The is logged in.
        /// </summary>
        private bool isloggedin;

        /// <summary>
        /// The is login failed.
        /// </summary>
        private bool isLoginFailed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        public ApplicationViewModel()
        {
            this.IsLoggingButtonEnabled = true;

            this.StockItems = new ObservableCollection<IInventoryItem>();
            this.AvailableBrands = new ObservableCollection<string>();
            this.SalesItems = new ObservableCollection<ISalesItem>();
            this.styleCodesforBrands = new Dictionary<string, ObservableCollection<string>>();
            this.StockItems.CollectionChanged += this.OnStockItemsCollectionChanged;
            this.pricePerPieceforStyleCodes = new Dictionary<string, double>();
            this.AddItemtoInventoryCommand = new DelegateCommand(this.OnAddItemtoInventoryCommandExecuted);
            this.AddItemtoInvoiceCommand = new DelegateCommand(this.OnAddItemtoInvoiceCommandExecuted);
            this.RemoveItemFromInvoice = new DelegateCommand<ISalesItem>(this.OnRemoveItemFromInvoiceCommandExecuted);
            this.LoginCommand = new DelegateCommand(this.OnLoginCommandExecuted);
            this.LockApplicationCommand = new DelegateCommand(this.OnLockApplicationCommandExecuted);
            this.RefreshApplicationCommand = new DelegateCommand(this.OnRefreshApplicationCommandExecuted);
            this.NoofPiecesSold = 1;

            // Database Connection
            this.SetupDataBase();

            // Test Data
            this.StockItems.Add(
                new InventoryItem { BrandName = "US Polo", NoofPieces = 3, PriceperPiece = 100, StyleCode = "777" });
            this.StockItems.Add(
               new InventoryItem { BrandName = "US Polo", NoofPieces = 2, PriceperPiece = 200, StyleCode = "888" });
            this.StockItems.Add(
               new InventoryItem { BrandName = "ABC", NoofPieces = 1, PriceperPiece = 500, StyleCode = "999" });
            this.StockItems.Add(
               new InventoryItem { BrandName = "XYZ", NoofPieces = 5, PriceperPiece = 600, StyleCode = "555" });

            // this.GetAllStockItemsFromDatabase();
            /* this.GetAllStockItemsFromDatabase();

            this.SetStyleCodesforDictionary();
            this.SetPricePerPieceForDictionary(); */
        }

        /// <summary>
        /// Gets or sets Brand Name Field for particular item in Inventory Tab
        /// </summary>
        public string BrandNameForInventory
        {
            get
            {
                return this.brandNameForInventory;
            }

            set
            {
                this.brandNameForInventory = value;
                this.RaisePropertyChanged("BrandNameForInventory");
            }
        }

        /// <summary>
        /// Gets or sets Style Code Field for particular item in Inventory Tab
        /// </summary>
        public string StyleCodeForInventory
        {
            get
            {
                return this.styleCodeForInventory;
            }

            set
            {
                this.styleCodeForInventory = value;
                this.RaisePropertyChanged("StyleCodeForInventory");
            }
        }

        /// <summary>
        /// Gets or sets value indicating No. of Pieces Field for particular item in Inventory Tab
        /// </summary>
        public int NoofPiecesForInventory
        {
            get
            {
                return this.noofPiecesForInventory;
            }

            set
            {
                this.noofPiecesForInventory = value;
                this.RaisePropertyChanged("NoofPiecesForInventory");
            }
        }

        /// <summary>
        /// Gets or sets Total Amount Field for particular item in Inventory Tab
        /// </summary>
        public double TotalAmountForAllItems
        {
            get
            {
                return this.totalAmountForAllItems;
            }

            set
            {
                this.totalAmountForAllItems = value;
                this.RaisePropertyChanged("TotalAmountForAllItems");
            }
        }

        /// <summary>
        /// Gets or sets the selected item while selling
        /// </summary>
        public string SelectedBrandforSelling
        {
            get
            {
                return this.selectedBrandforSelling;
            }

            set
            {
                this.selectedBrandforSelling = value;
                this.RaisePropertyChanged("SelectedBrandforSelling");
                if (this.SelectedBrandforSelling != null)
                {
                    this.AvailableStyleCodes = this.GetStyleCodes(this.selectedBrandforSelling);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected item while selling
        /// </summary>
        public string SelectedStyleCodeforSelling
        {
            get
            {
                return this.selectedStyleCodeforSelling;
            }

            set
            {
                this.selectedStyleCodeforSelling = value;
                this.RaisePropertyChanged("SelectedStyleCodeforSelling");
                this.PricePerPiece = this.GetPricePerPiece(this.SelectedStyleCodeforSelling);
            }
        }

        /// <summary>
        /// Gets or sets Total Amount Field for particular item in Inventory Tab
        /// </summary>
        public double SalesTotal
        {
            get
            {
                return this.salesTotal;
            }

            set
            {
                this.salesTotal = value;
                this.RaisePropertyChanged("SalesTotal");
            }
        }

        /// <summary>
        /// Gets or sets value indicating No. of Pieces Field for particular item in Inventory Tab
        /// </summary>
        public int NoofPiecesSold { get; set; }

        /// <summary>
        /// Gets or sets the selected item while selling
        /// </summary>
        public double SellingPrice
        {
            get
            {
                return this.sellingPrice;
            }

            set
            {
                this.sellingPrice = value;
                this.RaisePropertyChanged("SellingPrice");
            }
        }

        /// <summary>
        /// Gets or sets Price per Piece for a particular style code
        /// </summary>
        public double PricePerPiece
        {
            get
            {
                return this.pricePerPiece;
            }

            set
            {
                this.pricePerPiece = value;
                this.RaisePropertyChanged("PricePerPiece");
            }
        }

        /// <summary>
        /// Gets or sets the style code list for selected brand.
        /// </summary>
        public ObservableCollection<string> StyleCodeListforSelectedBrand { get; set; }

        /// <summary>
        /// Gets or sets collection having all items of Inventory
        /// </summary>
        public ObservableCollection<IInventoryItem> StockItems
        {
            get
            {
                return this.stockItems;
            }

            set
            {
                this.stockItems = value;
                this.RaisePropertyChanged("StockItems");
            }
        }

        /// <summary>
        /// Gets or sets collection having all items of Inventory
        /// </summary>
        public ObservableCollection<string> AvailableBrands
        {
            get
            {
                return this.availableBrands;
            }

            set
            {
                this.availableBrands = value;
                this.RaisePropertyChanged("AvailableBrands");
            }
        }

        /// <summary>
        /// Gets or sets list of Available Style Codes
        /// </summary>
        public ObservableCollection<string> AvailableStyleCodes
        {
            get
            {
                return this.availableStyleCodes;
            }

            set
            {
                this.availableStyleCodes = value;
                this.RaisePropertyChanged("AvailableStyleCodes");
            }
        }

        /// <summary>
        /// Gets or sets Price per Piece for a particular style code
        /// </summary>
        public int DiscountRate
        {
            get
            {
                return this.discountRate;
            }

            set
            {
                this.discountRate = value;
                this.RaisePropertyChanged("DiscountRate");
                this.DiscountedAmount = (this.salesTotalWithoutDiscount * this.discountRate) / 100;
                this.SalesTotal = this.salesTotalWithoutDiscount - this.DiscountedAmount;
            }
        }

        /// <summary>
        /// Gets or sets Price per Piece for a particular style code
        /// </summary>
        public double DiscountedAmount
        {
            get
            {
                return this.discountedAmount;
            }

            set
            {
                this.discountedAmount = value;
                this.RaisePropertyChanged("DiscountedAmount");
            }
        }

        /// <summary>
        /// Gets or sets the add item to inventory command.
        /// </summary>
        public ICommand AddItemtoInventoryCommand { get; set; }

        /// <summary>
        /// Gets or sets the add item to inventory command.
        /// </summary>
        public ICommand AddItemtoInvoiceCommand { get; set; }

        /// <summary>
        /// Gets or sets Login Command
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets Login Command
        /// </summary>
        public ICommand LockApplicationCommand { get; set; }

        /// <summary>
        /// Gets or sets Refresh Command
        /// </summary>
        public ICommand RefreshApplicationCommand { get; set; }

        /// <summary>
        /// Gets or sets theCommand to remove item from Invoice.
        /// </summary>
        public ICommand RemoveItemFromInvoice { get; set; }

        /// <summary>
        /// Gets or sets collection having all Sales Items of Inventory
        /// </summary>
        public ObservableCollection<ISalesItem> SalesItems { get; set; }

        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user has logged in or not.
        /// </summary>
        public bool IsLoggedIn
        {
            get
            {
                return this.isloggedin;
            }

            set
            {
                this.isloggedin = value;
                this.RaisePropertyChanged("IsLoggedIn");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether user has logged in or not.
        /// </summary>
        public bool IsLoginFailed
        {
            get
            {
                return this.isLoginFailed;
            }

            set
            {
                this.isLoginFailed = value;
                this.RaisePropertyChanged("IsLoginFailed");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Logging is is progress or not.
        /// </summary>
        public bool IsLoggingButtonEnabled { get; set; }

        /// <summary>
        /// Removes Sold Items from Data Base.
        /// </summary>
        public void RemoveSoldItems()
        {
            foreach (var soldItems in this.SalesItems)
            {
                // TODO : Tejas - Write a logic to update the number of pieces in database for sold item
            }

            this.SalesItems.Clear();
        }

        /// <summary>
        /// The on add item to inventory.
        /// </summary>
        private void OnAddItemtoInventoryCommandExecuted()
        {
            if (string.IsNullOrEmpty(this.BrandNameForInventory))
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_BRANDNAME);
                return;
            }

            if (string.IsNullOrEmpty(this.StyleCodeForInventory))
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_STYLECODE);
                return;
            }

            if (this.NoofPiecesForInventory <= 0)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_PIECES);
                return;
            }

            if (this.TotalAmountForAllItems < 0)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_PRICE);
                return;
            }

            this.insertInventoryCommand.Parameters.AddWithValue("@Brand", this.BrandNameForInventory);
            this.insertInventoryCommand.Parameters.AddWithValue("@StyleCode", this.StyleCodeForInventory);
            this.insertInventoryCommand.Parameters.AddWithValue("@Price", this.TotalAmountForAllItems / this.NoofPiecesForInventory);
            this.insertInventoryCommand.Parameters.AddWithValue("@Pieces", this.NoofPiecesForInventory);

            try
            {
                this.databaseConnectionToInventoryDB.Open();
                this.insertInventoryCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDB.Close();
            }
        }

        /// <summary>
        /// The on add item to invoice command executed.
        /// </summary>
        private void OnAddItemtoInvoiceCommandExecuted()
        {
            if (this.SelectedBrandforSelling == null)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.NO_ITEM_SELECTED);
                return;
            }

            if (this.SelectedStyleCodeforSelling == null)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.NO_STYLECODE_SELECTED);
                return;
            }

            if (this.NoofPiecesSold <= 0)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_PIECES);
                return;
            }

            if (this.SellingPrice < this.PricePerPiece)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_SELLING_PRICE);
                return;
            }

            var item = new SalesItem
            {
                DisplayName = this.SelectedBrandforSelling + " - " + this.NoofPiecesSold,
                Price = this.SellingPrice * this.NoofPiecesSold
            };

            this.SalesItems.Add(item);
            this.salesTotalWithoutDiscount = this.salesTotalWithoutDiscount + item.Price;
            this.DiscountedAmount = (this.salesTotalWithoutDiscount * this.discountRate) / 100;
            this.SalesTotal = this.salesTotalWithoutDiscount - this.DiscountedAmount;

            // Reset values
            this.SelectedBrandforSelling = null;
            this.SelectedStyleCodeforSelling = null;
            this.NoofPiecesSold = 1;
            this.SellingPrice = 0;
        }

        /// <summary>
        /// The on remove item from invoice command executed.
        /// </summary>
        /// <param name="salesItem">
        /// The sales Item.
        /// </param>
        private void OnRemoveItemFromInvoiceCommandExecuted(ISalesItem salesItem)
        {
            if (this.SalesItems.Contains(salesItem))
            {
                this.salesTotalWithoutDiscount = this.salesTotalWithoutDiscount - salesItem.Price;
                this.DiscountedAmount = (this.salesTotalWithoutDiscount * this.discountRate) / 100;
                this.SalesTotal = this.salesTotalWithoutDiscount - this.DiscountedAmount;
                this.SalesItems.Remove(salesItem);
            }
        }

        /// <summary>
        /// The on Login command executed.
        /// </summary>
        private void OnLoginCommandExecuted()
        {
            this.IsLoggingButtonEnabled = false;

            // Get Credentials from DB
            if (string.IsNullOrEmpty(this.UserName))
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.EMPTY_USERNAME);
                return;
            }

            // Thread.Sleep(5000);
            this.IsLoggedIn = true;
            this.IsLoggingButtonEnabled = true;
        }

        /// <summary>
        /// The on Lock Application command executed.
        /// </summary>
        private void OnLockApplicationCommandExecuted()
        {
            this.IsLoggedIn = false;
        }

        /// <summary>
        /// The on Lock Application command executed.
        /// </summary>
        private void OnRefreshApplicationCommandExecuted()
        {

        }

        /// <summary>
        /// The on stock items collection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnStockItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems == null || e.NewItems.Count <= 0)
                    {
                        return;
                    }

                    foreach (var inventoryItem in e.NewItems.OfType<IInventoryItem>())
                    {
                        this.SetCollectionOfAvailableBrands(inventoryItem);
                    }

                    break;
            }
        }

        /// <summary>
        /// Gets the List of Style codes available for a brand
        /// </summary>
        /// <param name="brandName">
        /// The brand name.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>ObservableCollection</cref>
        ///     </see>
        ///     .
        /// </returns>
        private ObservableCollection<string> GetStyleCodes(string brandName)
        {
            if (this.styleCodesforBrands != null && this.styleCodesforBrands.ContainsKey(brandName))
            {
                return this.styleCodesforBrands[brandName];
            }

            return new ObservableCollection<string>();
        }

        /// <summary>
        /// Sets the List of Style codes available for a brand
        /// </summary>
        private void SetStyleCodesforDictionary()
        {
            if (this.styleCodesforBrands == null)
            {
                return;
            }

            var inventoryItems = this.StockItems.ToList();
            foreach (var inventoryItem in inventoryItems.Where(inventoryItem => inventoryItem != null))
            {
                if (!this.styleCodesforBrands.ContainsKey(inventoryItem.BrandName))
                {
                    this.styleCodesforBrands.Add(
                        inventoryItem.BrandName, new ObservableCollection<string> { inventoryItem.StyleCode });
                }
                else
                {
                    var stylecodeList = this.styleCodesforBrands[inventoryItem.BrandName];
                    if (!stylecodeList.Contains(inventoryItem.StyleCode))
                    {
                        stylecodeList.Add(inventoryItem.StyleCode);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the PPP for a particular style code
        /// </summary>
        /// <param name="styleCode">
        /// The style Code.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private double GetPricePerPiece(string styleCode)
        {
            if (this.pricePerPieceforStyleCodes != null && styleCode != null && this.pricePerPieceforStyleCodes.ContainsKey(styleCode))
            {
                var inventoryItem = this.StockItems.FirstOrDefault(item => item.StyleCode == styleCode);
                if (inventoryItem != null)
                {
                    return inventoryItem.PriceperPiece;
                }
            }

            return default(double);
        }

        /// <summary>
        /// Sets the PPP value for all items in Local Dictionary.
        /// </summary>
        private void SetPricePerPieceForDictionary()
        {
            if (this.pricePerPieceforStyleCodes == null)
            {
                return;
            }

            var inventoryItems = this.StockItems.ToList();
            foreach (var inventoryItem in inventoryItems.Where(inventoryItem => inventoryItem != null))
            {
                if (!this.pricePerPieceforStyleCodes.ContainsKey(inventoryItem.StyleCode))
                {
                    this.pricePerPieceforStyleCodes.Add(inventoryItem.StyleCode, inventoryItem.PriceperPiece);
                }
                else
                {
                    this.pricePerPieceforStyleCodes[inventoryItem.StyleCode] = inventoryItem.PriceperPiece;
                }
            }
        }

        /// <summary>
        /// Sets the collection of all available brands.
        /// </summary>
        /// <param name="inventoryItem">
        /// The inventory Item.
        /// </param>
        private void SetCollectionOfAvailableBrands(IInventoryItem inventoryItem)
        {
            if (!this.AvailableBrands.Contains(inventoryItem.BrandName))
            {
                this.AvailableBrands.Add(inventoryItem.BrandName);
            }
        }

        /// <summary>
        /// Method Which connects to Database
        /// </summary>
        private void SetupDataBase()
        {
            this.databaseConnectionToInventoryDB = new SqlConnection(ConnectionStringForInventoryDB);
            this.insertInventoryCommand = new SqlCommand(InsertInventorySPString, this.databaseConnectionToInventoryDB) { CommandType = CommandType.StoredProcedure };
            this.allInventoryCommand = new SqlCommand(AllInventorySPString, this.databaseConnectionToInventoryDB) { CommandType = CommandType.StoredProcedure };
        }

        /// <summary>
        /// Method Which gets all the stock items from database
        /// </summary>
        private void GetAllStockItemsFromDatabase()
        {
            var dataSet = new DataSet();
            try
            {
                this.databaseConnectionToInventoryDB.Open();
                var dataAdapter = new SqlDataAdapter(this.allInventoryCommand);
                dataAdapter.Fill(dataSet);
                this.databaseConnectionToInventoryDB.Close();
            }
            catch (SqlException ex)
            {
                // TODO: Handle exception.
            }
            finally
            {
                this.databaseConnectionToInventoryDB.Close();
            }

            // TODO: Do not hard coded values to iterate through Table. use Foreach or something else.
            for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                var brandName = dataSet.Tables[0].Rows[i][1].ToString();
                var styleCode = dataSet.Tables[0].Rows[i][2].ToString();
                var priceperPiece = Convert.ToDouble(dataSet.Tables[0].Rows[i][3]);
                var pieces = Convert.ToInt32(dataSet.Tables[0].Rows[i][4]);

                this.StockItems.Add(
                    new InventoryItem
                    {
                        BrandName = brandName,
                        NoofPieces = pieces,
                        PriceperPiece = priceperPiece,
                        StyleCode = styleCode
                    });
            }

            this.SetStyleCodesforDictionary();
            this.SetPricePerPieceForDictionary();
        }

        // TODO: Jignesh - Make Discount rate nullable or add a style to remove binding error red border
        // TODO: Jignesh - If item is removed from stock then update available brands
    }
}
