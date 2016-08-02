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
    using System.Linq;
    using System.Printing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    using FH.Models;
    using FH.ReportForms;

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
        /// The master user name.
        /// </summary>
        private const string MasterUserName = "Administrator";

        /// <summary>
        /// The master password.
        /// </summary>
        private const string MasterPassword = "vasoya321";

        /// <summary>
        /// The height offset value.
        /// </summary>
        private const long HeightOffsetValue = 10;

        /// <summary>
        /// The style codes for brands.
        /// </summary>
        private readonly Dictionary<string, ObservableCollection<string>> styleCodesforBrands;

        /// <summary>
        /// The style codes for brands.
        /// </summary>
        private readonly Dictionary<string, double> pricePerPieceforStyleCodes;

        /// <summary>
        /// Instance of Database factory
        /// </summary>
        private readonly DataBaseFactory dataBaseFactory;

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
        /// The purchase price for an Item.
        /// </summary>
        private double purchasePrice;

        /// <summary>
        /// The Selling price for an Item.
        /// </summary>
        private double sellingPriceForInventory;

        /// <summary>
        /// selected item for selling.
        /// </summary>
        private string selectedBrandforSelling;

        /// <summary>
        /// Selected item type for selling.
        /// </summary>
        private string selectedItemType;

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
        private double actualSPTotal;

        /// <summary>
        /// Private variable for price per piece.
        /// </summary>
        private double pricePerPiece;

        /// <summary>
        /// Private variable for Discount Rate.
        /// </summary>
        private double discountRate;

        /// <summary>
        /// Private variable for Discount Rate for Selected Item.
        /// </summary>
        private double discountRateForItem;

        /// <summary>
        /// Private variable for Discount Amount.
        /// </summary>
        private double discountedAmount;

        /// <summary>
        /// Private variable for Maximum Discount Amount for selected sales Item.
        /// </summary>
        private double maximumItemDiscount;

        /// <summary>
        /// The selected sales item.
        /// </summary>
        private ISalesItem selectedSalesItem;

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
        /// Private variable for available style code collection.
        /// </summary>
        private ObservableCollection<string> itemTypeCollection;

        /// <summary>
        /// The is logged in.
        /// </summary>
        private bool isloggedin;

        /// <summary>
        /// The is login failed.
        /// </summary>
        private bool isLoginFailed;

        /// <summary>
        /// The user name.
        /// </summary>
        private string userName = string.Empty;
        /*private string password = string.Empty;*/

        /// <summary>
        /// The from date.
        /// </summary>
        private DateTime fromDate = DateTime.Now;

        /// <summary>
        /// The to date.
        /// </summary>
        private DateTime toDate = DateTime.Now;

        /// <summary>
        /// The current date.
        /// </summary>
        private string currentDate = DateTime.Now.ToShortDateString();

        /// <summary>
        /// The invoice Id number.
        /// </summary>
        private long invoiceIdnumber = 1;

        /// <summary>
        /// The invoice id.
        /// </summary>
        private string invoiceID;

        /// <summary>
        /// The customer name.
        /// </summary>
        private string customerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        public ApplicationViewModel()
        {
            this.IsLoggingButtonEnabled = true;

            this.StockItems = new ObservableCollection<IInventoryItem>();
            this.AvailableBrands = new ObservableCollection<string>();
            this.SalesItems = new ObservableCollection<ISalesItem>();
            this.dataBaseFactory = new DataBaseFactory();
            this.styleCodesforBrands = new Dictionary<string, ObservableCollection<string>>();
            this.StockItems.CollectionChanged += this.OnStockItemsCollectionChanged;
            this.pricePerPieceforStyleCodes = new Dictionary<string, double>();
            this.AddItemtoInventoryCommand = new DelegateCommand(this.OnAddItemtoInventoryCommandExecuted);
            this.AddItemtoInvoiceCommand = new DelegateCommand(this.OnAddItemtoInvoiceCommandExecuted);
            this.RemoveItemFromInvoice = new DelegateCommand<ISalesItem>(this.OnRemoveItemFromInvoiceCommandExecuted);
            this.AddDiscountForItem = new DelegateCommand(this.OnAddDiscountForItemCommandExecuted);
            this.LoginCommand = new DelegateCommand<object>(this.OnLoginCommandExecuted);
            this.LockApplicationCommand = new DelegateCommand(this.OnLockApplicationCommandExecuted);
            this.RefreshApplicationCommand = new DelegateCommand(this.OnRefreshApplicationCommandExecuted);
            this.GenerateSalesReportCommand = new DelegateCommand(this.OnGenerateSalesReportCommandExecuted);
            this.GenerateInventoryReportCommand = new DelegateCommand(this.OnGenerateInventoryReportCommandExecuted);
            this.PrintInvoiceCommand = new DelegateCommand<object>(this.OnPrintInvoiceCommandExecuted);
            this.ClearInvoiceCommand = new DelegateCommand(this.OnClearInvoiceommandExecuted);
            this.ItemTypeCollection = new ObservableCollection<string> { "SHIRT", "T-SHIRT", "JEANS", "TROUSER" };
            this.NoofPiecesSold = 1;
            this.invoiceID = string.Format(Globalization.INVOICEID_FORMAT, this.invoiceIdnumber);

            // Database Connection
            this.dataBaseFactory.SetupDataBase();
            Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                    {
                        this.invoiceIdnumber = this.dataBaseFactory.GetLastInvoiceId();
                        this.invoiceIdnumber++;
                        this.InvoiceID = string.Format(Globalization.INVOICEID_FORMAT, this.invoiceIdnumber);
                    }));
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
        public double PurchasePrice
        {
            get
            {
                return this.purchasePrice;
            }

            set
            {
                this.purchasePrice = value;
                this.RaisePropertyChanged("PurchasePrice");
            }
        }

        /// <summary>
        /// Gets or sets Selling Price Field for particular item in Inventory Tab
        /// </summary>
        public double SellingPriceForInventory
        {
            get
            {
                return this.sellingPriceForInventory;
            }

            set
            {
                this.sellingPriceForInventory = value;
                this.RaisePropertyChanged("SellingPriceForInventory");
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

                Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() =>
                {
                    this.AvailableStyleCodes = this.GetStyleCodes(this.selectedBrandforSelling);
                }));
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

                // TODO: Dispatcher
                // TODO: check for max no. of pieces
                this.PricePerPiece = this.GetPricePerPiece(this.SelectedStyleCodeforSelling);
                this.SellingPrice = this.GetSellingPrice(this.SelectedBrandforSelling, this.SelectedStyleCodeforSelling);
            }
        }

        /// <summary>
        /// Gets or sets the selected item while selling
        /// </summary>
        public string SelectedItemType
        {
            get
            {
                return this.selectedItemType;
            }

            set
            {
                this.selectedItemType = value;
                this.RaisePropertyChanged("SelectedItemType");
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
        /// Gets or sets collection of Item Types
        /// </summary>
        public ObservableCollection<string> ItemTypeCollection
        {
            get
            {
                return this.itemTypeCollection;
            }

            set
            {
                this.itemTypeCollection = value;
                this.RaisePropertyChanged("ItemTypeCollection");
            }
        }

        /// <summary>
        /// Gets or sets Price per Piece for a particular style code
        /// </summary>
        public double DiscountRate
        {
            get
            {
                return this.discountRate;
            }

            set
            {
                this.discountRate = value;
                this.RaisePropertyChanged("DiscountRate");
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
        /// Gets or sets Discount rate for an item
        /// </summary>
        public double DiscountRateForItem
        {
            get
            {
                return this.discountRateForItem;
            }

            set
            {
                this.discountRateForItem = value;
                this.RaisePropertyChanged("DiscountRateForItem");
            }
        }

        /// <summary>
        /// Gets or sets Price per Piece for a particular style code
        /// </summary>
        public double MaximumItemDiscount
        {
            get
            {
                return this.maximumItemDiscount;
            }

            set
            {
                this.maximumItemDiscount = value;
                this.RaisePropertyChanged("MaximumItemDiscount");
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
        /// Gets or sets Print Invoice Command
        /// </summary>
        public ICommand PrintInvoiceCommand { get; set; }

        /// <summary>
        /// Gets or sets Print Invoice Command
        /// </summary>
        public ICommand ClearInvoiceCommand { get; set; }

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
        /// Gets or sets theCommand to remove item from Invoice.
        /// </summary>
        public ICommand AddDiscountForItem { get; set; }

        /// <summary>
        /// Gets or sets the Command to generate sales report
        /// </summary>
        public ICommand GenerateSalesReportCommand { get; set; }

        /// <summary>
        /// Gets or sets the Command to generate sales report
        /// </summary>
        public ICommand GenerateInventoryReportCommand { get; set; }

        /// <summary>
        /// Gets or sets collection having all Sales Items of Inventory
        /// </summary>
        public ObservableCollection<ISalesItem> SalesItems { get; set; }

        /// <summary>
        /// Gets or sets the selected sales item.
        /// </summary>
        public ISalesItem SelectedSalesItem
        {
            get
            {
                return this.selectedSalesItem;
            }

            set
            {
                this.selectedSalesItem = value;
                this.RaisePropertyChanged("SelectedSalesItem");
                if (value != null)
                {
                    this.MaximumItemDiscount = Math.Round(((value.ActualSellingPrice - value.PurchasePrice) * 100) / value.ActualSellingPrice, 2);
                    this.DiscountRateForItem = Math.Round(((value.ActualSellingPrice - value.SellingPrice) * 100) / value.ActualSellingPrice, 2);
                }
                else
                {
                    this.MaximumItemDiscount = 0;
                    this.discountRateForItem = 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
                this.RaisePropertyChanged("UserName");
            }
        }

        /*        /// <summary>
                /// Gets or sets Password
                /// </summary>
                public string Password
                {
                    get
                    {
                        return this.password;
                    }

                    set
                    {
                        this.password = value;
                        this.RaisePropertyChanged("Password");
                    }
                }*/

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
        /// Gets or sets the from date.
        /// </summary>
        public DateTime FromDate
        {
            get
            {
                return this.fromDate;
            }

            set
            {
                this.fromDate = value;
                this.RaisePropertyChanged("FromDate");
            }
        }

        /// <summary>
        /// Gets or sets the to date.
        /// </summary>
        public DateTime ToDate
        {
            get
            {
                return this.toDate;
            }

            set
            {
                this.toDate = value;
                this.RaisePropertyChanged("ToDate");
            }
        }

        /// <summary>
        /// Gets or sets the current date.
        /// </summary>
        public string CurrentDate
        {
            get
            {
                return this.currentDate;
            }

            set
            {
                this.currentDate = value;
                this.RaisePropertyChanged("CurrentDate");
            }
        }

        /// <summary>
        /// Gets or sets the invoice id.
        /// </summary>
        public string InvoiceID
        {
            get
            {
                return this.invoiceID;
            }

            set
            {
                this.invoiceID = value;
                this.RaisePropertyChanged("InvoiceID");
            }
        }

        /// <summary>
        /// Gets or sets the Customer Name.
        /// </summary>
        public string CustomerName
        {
            get
            {
                return this.customerName;
            }

            set
            {
                this.customerName = value;
                this.RaisePropertyChanged("CustomerName");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Logging is is progress or not.
        /// </summary>
        public bool IsLoggingButtonEnabled { get; set; }

        /// <summary>
        /// Removes Sold Items from Data Base.
        /// </summary>
        private void RemoveSoldItems()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        this.dataBaseFactory.UpdateNoofPiecesInInventory(this.SalesItems.ToList());
                        this.SalesItems.Clear();
                        this.GetAllStockItems();

                        // Reset Values
                        this.actualSPTotal = 0;
                        this.SalesTotal = 0;
                        this.DiscountRate = 0;
                    }));
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

            if (this.PurchasePrice < 0)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_PRICE);
                return;
            }

            if (this.SellingPriceForInventory < this.PurchasePrice)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_SELLING_PRICE);
                return;
            }

            var existingItem =
                this.StockItems.FirstOrDefault(item => string.Equals(item.BrandName, this.BrandNameForInventory, StringComparison.InvariantCultureIgnoreCase));
            if (existingItem != null)
            {
                // TODO : To update an item with same brand and style Code
                /*  existingItem =
                 this.StockItems.FirstOrDefault(item => String.Equals(item.BrandName, this.BrandNameForInventory, StringComparison.InvariantCultureIgnoreCase) && String.Equals(item.StyleCode, this.StyleCodeForInventory, StringComparison.InvariantCultureIgnoreCase));
                  var existingStyleCode = string.Empty;
                  if (String.Equals(this.StyleCodeForInventory, existingItem.StyleCode,
                      StringComparison.InvariantCultureIgnoreCase))
                  {
                      this.dataBaseFactory.UpdateNoofPiecesInInventory(new ObservableCollection<ISalesItem>
                      {
                          new SalesItem
                          {
                              BrandName = existingItem.BrandName,
                              StyleCode = existingItem.StyleCode,
                              PurchasePrice = this.PurchasePrice // this need to be taken care of in case of updating
                          }
                      }, existingItem.NoofPieces + this.NoofPiecesForInventory);
                  }
                  else
                  {*/
                this.dataBaseFactory.AddToInventoryTable(
                    new InventoryItem
                    {
                        BrandName = existingItem.BrandName,
                        StyleCode = this.StyleCodeForInventory.ToUpper(),
                        PurchasePrice = this.PurchasePrice,
                        NoofPieces = this.NoofPiecesForInventory,
                        SellingPrice = this.SellingPriceForInventory
                    });

                // }
            }
            else
            {
                this.dataBaseFactory.AddToInventoryTable(
               new InventoryItem
               {
                   BrandName = this.BrandNameForInventory.ToUpper(),
                   StyleCode = this.StyleCodeForInventory.ToUpper(),
                   PurchasePrice = this.PurchasePrice,
                   NoofPieces = this.NoofPiecesForInventory,
                   SellingPrice = this.SellingPriceForInventory
               });
            }

            // Reset values
            this.BrandNameForInventory = string.Empty;
            this.StyleCodeForInventory = string.Empty;
            this.NoofPiecesForInventory = 0;
            this.PurchasePrice = 0;

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(this.GetAllStockItems));
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

            if (this.SelectedItemType == null)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_TYPE);
                return;
            }

            if (this.SellingPrice < this.PricePerPiece)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_SELLING_PRICE);
                return;
            }

            var firstOrDefault = this.StockItems.FirstOrDefault(
                stockitem =>
                    stockitem.BrandName == this.SelectedBrandforSelling &&
                    stockitem.StyleCode == this.SelectedStyleCodeforSelling);

            if (firstOrDefault != null)
            {
                var maxNoOfPieces =
                    firstOrDefault.NoofPieces;
                if (maxNoOfPieces < this.NoofPiecesSold)
                {
                    MessageBox.Show(Application.Current.MainWindow, Globalization.PIECES_NOT_AVAILABLE);
                    return;
                }
            }

            var item =
                new SalesItem(this.GetSellingPrice(this.SelectedBrandforSelling, this.SelectedStyleCodeforSelling))
            {
                DisplayName = this.SelectedBrandforSelling + " - " + this.SelectedItemType + " - " + this.SelectedStyleCodeforSelling,
                SellingPrice = this.SellingPrice,
                TotalAmount = this.SellingPrice * this.NoofPiecesSold,
                BrandName = this.SelectedBrandforSelling,
                StyleCode = this.SelectedStyleCodeforSelling,
                NoofPiecesSold = this.NoofPiecesSold,
                PurchasePrice = this.PricePerPiece
            };

            this.SalesItems.Add(item);
            this.actualSPTotal = this.actualSPTotal + (item.ActualSellingPrice * item.NoofPiecesSold);
            this.SalesTotal = this.SalesTotal + item.TotalAmount;
            this.DiscountedAmount = this.actualSPTotal - this.SalesTotal;
            this.DiscountRate = this.actualSPTotal > 0 ? Math.Round(this.DiscountedAmount * 100 / this.actualSPTotal, 2) : 0;

            this.CurrentDate = DateTime.Now.ToShortDateString();

            // Reset values
            this.SelectedBrandforSelling = null;
            this.SelectedStyleCodeforSelling = null;
            this.NoofPiecesSold = 1;
            this.SellingPrice = 0;
            this.SelectedItemType = null;
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
                this.actualSPTotal = this.actualSPTotal - (salesItem.ActualSellingPrice * salesItem.NoofPiecesSold);
                this.SalesTotal = this.SalesTotal - salesItem.TotalAmount;
                this.DiscountedAmount = this.actualSPTotal - this.SalesTotal;
                this.DiscountRate = this.actualSPTotal > 0 ? Math.Round(this.DiscountedAmount * 100 / this.actualSPTotal, 2) : 0;

                this.SalesItems.Remove(salesItem);
            }
        }

        /// <summary>
        /// The on Add discount for an item command executed.
        /// </summary>
        private void OnAddDiscountForItemCommandExecuted()
        {
            if (this.SelectedSalesItem == null)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.INVALID_SELECTION);
                return;
            }

            this.SalesTotal = this.SalesTotal - this.SelectedSalesItem.TotalAmount;

            this.SelectedSalesItem.SellingPrice = (this.SelectedSalesItem.ActualSellingPrice * (100 - this.DiscountRateForItem)) / 100;
            this.SelectedSalesItem.TotalAmount = this.SelectedSalesItem.SellingPrice * this.SelectedSalesItem.NoofPiecesSold;

            this.SalesTotal = this.SalesTotal + this.SelectedSalesItem.TotalAmount;
            this.DiscountedAmount = this.actualSPTotal - this.SalesTotal;
            this.DiscountRate = this.actualSPTotal > 0 ? Math.Round(this.DiscountedAmount * 100 / this.actualSPTotal, 2) : 0;
        }

        /// <summary>
        /// The on Login command executed.
        /// </summary>
        /// <param name="passwordBoxcontrol">
        /// The password Box control.
        /// </param>
        private void OnLoginCommandExecuted(object passwordBoxcontrol)
        {
            this.IsLoginFailed = false;
            this.IsLoggingButtonEnabled = false;

            if (string.IsNullOrEmpty(this.UserName))
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.EMPTY_USERNAME);
                return;
            }

            var passWordBox = passwordBoxcontrol as PasswordBox;
            var password = string.Empty;
            if (passWordBox != null)
            {
                password = passWordBox.Password;
            }

            if (string.Equals(this.UserName, MasterUserName, StringComparison.InvariantCultureIgnoreCase) && string.Equals(password, MasterPassword, StringComparison.InvariantCultureIgnoreCase))
            {
                this.IsLoggedIn = true;
                this.IsLoggingButtonEnabled = true;
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(this.GetAllStockItems));
                return;
            }

            // Get Credentials from DB
            var credentials = this.dataBaseFactory.GetCredentialsFromDb();
            if (credentials.Count >= 2)
            {
                // TODO: Credentials Check
                if (string.Equals(this.UserName, credentials[0], StringComparison.InvariantCultureIgnoreCase) && string.Equals(password, credentials[1], StringComparison.InvariantCultureIgnoreCase))
                {
                    this.IsLoggedIn = true;
                    this.IsLoggingButtonEnabled = true;
                    if (passWordBox != null)
                    {
                        passWordBox.Password = string.Empty;
                    }

                    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(this.GetAllStockItems));
                }
                else
                {
                    this.IsLoginFailed = true;
                }
            }

            // TODO : What if it fails to get credentials
        }

        /// <summary>
        /// The on Lock Application command executed.
        /// </summary>
        private void OnLockApplicationCommandExecuted()
        {
            this.IsLoggedIn = false;
            this.IsLoggingButtonEnabled = true;
            this.IsLoginFailed = false;
        }

        /// <summary>
        /// The on Lock Application command executed.
        /// </summary>
        private void OnRefreshApplicationCommandExecuted()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(this.GetAllStockItems));
        }

        /// <summary>
        /// The on Clear Invoice command executed.
        /// </summary>
        private void OnClearInvoiceommandExecuted()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        foreach (var salesItem in this.SalesItems.ToList())
                        {
                            this.OnRemoveItemFromInvoiceCommandExecuted(salesItem);
                        }
                    }));
        }

        /// <summary>
        /// The on generate sales report command executed.
        /// </summary>
        private void OnGenerateSalesReportCommandExecuted()
        {
            if (this.FromDate > this.ToDate)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.FROMISGREATERTHANTO);
                return;
            }

            var salesReport = new SalesReportForm(this.FromDate, this.ToDate);
            salesReport.Show();
        }

        /// <summary>
        /// The on generate inventory report command executed.
        /// </summary>
        private void OnGenerateInventoryReportCommandExecuted()
        {
            if (this.FromDate > this.ToDate)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.FROMISGREATERTHANTO);
                return;
            }

            var inventoryreport = new InventoryReportForm(this.FromDate, this.ToDate);
            inventoryreport.Show();
        }

        /// <summary>
        /// The on print invoice command executed.
        /// </summary>
        /// <param name="invoicegrid">
        /// The invoice grid.
        /// </param>
        private void OnPrintInvoiceCommandExecuted(object invoicegrid)
        {
            var invoiceGrid = invoicegrid as Grid;
            if (invoiceGrid == null)
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.CANNOTPRINT);
                return;
            }

            if (string.IsNullOrEmpty(this.CustomerName))
            {
                MessageBox.Show(Application.Current.MainWindow, Globalization.EMPTY_CUSTOMERNAME);
                return;
            }

            this.SelectedSalesItem = null;
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // store original scale
                var originalScale = invoiceGrid.LayoutTransform;

                // get selected printer capabilities
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

                if (capabilities.PageImageableArea == null)
                {
                    MessageBox.Show(Application.Current.MainWindow, Globalization.CANNOTPRINT);
                    return;
                }

                // Transform the Visual to scale
                invoiceGrid.LayoutTransform = new ScaleTransform(capabilities.PageImageableArea.ExtentWidth / invoiceGrid.ActualWidth, (capabilities.PageImageableArea.ExtentHeight / 2) / invoiceGrid.ActualHeight);

                // get the size of the printer page
                var customSize = new Size(
                    capabilities.PageImageableArea.ExtentWidth,
                    (capabilities.PageImageableArea.ExtentHeight / 2) - HeightOffsetValue);

                // update the layout of the visual to the printer page size.
                invoiceGrid.Measure(customSize);
                invoiceGrid.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), customSize));

                // now print the visual to printer to fit on the one page.
                printDialog.PrintVisual(invoiceGrid, "FH_Invoice");

                // apply the original transform.
                invoiceGrid.LayoutTransform = originalScale;

                this.RemoveSoldItems();
                Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    this.dataBaseFactory.UpdateLastInvoiceId(this.invoiceIdnumber);
                    this.invoiceIdnumber++;
                    this.InvoiceID = string.Format(Globalization.INVOICEID_FORMAT, this.invoiceIdnumber);
                }));

                this.CustomerName = string.Empty;
            }
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
            if (this.styleCodesforBrands != null && brandName != null && this.styleCodesforBrands.ContainsKey(brandName))
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
                    return inventoryItem.PurchasePrice;
                }
            }

            return default(double);
        }

        /// <summary>
        /// Gets the PPP for a particular style code
        /// </summary>
        /// <param name="brandName">
        /// The brand Name.
        /// </param>
        /// <param name="styleCode">
        /// The style Code.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private double GetSellingPrice(string brandName, string styleCode)
        {
            if (brandName != null && styleCode != null)
            {
                var inventoryItem = this.StockItems.FirstOrDefault(item => item.BrandName == brandName && item.StyleCode == styleCode);
                if (inventoryItem != null)
                {
                    return inventoryItem.SellingPrice;
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
                    this.pricePerPieceforStyleCodes.Add(inventoryItem.StyleCode, inventoryItem.PurchasePrice);
                }
                else
                {
                    this.pricePerPieceforStyleCodes[inventoryItem.StyleCode] = inventoryItem.PurchasePrice;
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
        /// Method Which gets all the stock items from database
        /// </summary>
        private void GetAllStockItems()
        {
            var stockItemsfromDb = this.dataBaseFactory.GetAllStockItemsFromDatabase();
            foreach (var stockItem in stockItemsfromDb)
            {
                var existingItem = this.StockItems.FirstOrDefault(item => item.StyleCode == stockItem.StyleCode && item.BrandName == stockItem.BrandName);
                if (existingItem != null)
                {
                    existingItem.PurchasePrice = stockItem.PurchasePrice;
                    existingItem.NoofPieces = stockItem.NoofPieces;
                }
                else
                {
                    this.StockItems.Add(stockItem);
                }
            }

            this.SetStyleCodesforDictionary();
            this.SetPricePerPieceForDictionary();
        }

        // TODO: Jignesh - If item is removed from stock then update available brands
    }
}
