// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryItem.cs" company="XYZ">
//   Copyright (C) 2014 Motorola Solutions, Inc.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Models
{
    using Microsoft.Practices.Prism.ViewModel;

    /// <summary>
    /// The inventory item.
    /// </summary>
    public class InventoryItem : NotificationObject, IInventoryItem
    {
        /// <summary>
        /// private variable for brand Name
        /// </summary>
        private string brandName;

        /// <summary>
        /// private variable for style code
        /// </summary>
        private string styleCode;

        /// <summary>
        /// private variable for no. of pieces
        /// </summary>
        private int noofPieces;

        /// <summary>
        /// private variable for Total Amount for All the Items
        /// </summary>
        private double purchasePrice;

        /// <summary>
        /// The Selling price for an Item.
        /// </summary>
        private double sellingPrice;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItem"/> class.
        /// </summary>
        public InventoryItem()
        {
            this.brandName = string.Empty;
            this.styleCode = string.Empty;
            this.noofPieces = 0;
            this.purchasePrice = 0;
            this.sellingPrice = 0;
        }

        /// <summary>
        /// Gets or sets Brand Name for particular item
        /// </summary>
        public string BrandName
        {
            get
            {
                return this.brandName;
            }

            set
            {
                this.brandName = value;
                this.RaisePropertyChanged("BrandName");
            }
        }

        /// <summary>
        /// Gets or sets Style Code for particular item
        /// </summary>
        public string StyleCode
        {
            get
            {
                return this.styleCode;
            }

            set
            {
                this.styleCode = value;
                this.RaisePropertyChanged("StyleCode");
            }
        }

        /// <summary>
        /// Gets or sets value indicating No. of Pieces for particular item
        /// </summary>
        public int NoofPieces
        {
            get
            {
                return this.noofPieces;
            }

            set
            {
                this.noofPieces = value;
                this.RaisePropertyChanged("NoofPieces");
            }
        }

        /// <summary>
        /// Gets or sets Total Amount for All the Items
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
        /// Gets or sets Total Amount Field for particular item in Inventory Tab
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
    }
}
