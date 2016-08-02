// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SalesItem.cs" company="Dextech">
//   Copyright (C) 2014 Dextech.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Models
{
    /// <summary>
    /// The sales item.
    /// </summary>
    public class SalesItem : Microsoft.Practices.Prism.ViewModel.NotificationObject, ISalesItem
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
        /// private variable for total Amount
        /// </summary>
        private double totalAmount;

        /// <summary>
        /// private variable for Selling Price for an Item
        /// </summary>
        private double sellingPrice;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesItem"/> class.
        /// </summary>
        /// <param name="actualSellingPrice">
        /// The actual selling price.
        /// </param>
        public SalesItem(double actualSellingPrice)
        {
            this.ActualSellingPrice = actualSellingPrice;

            // this.brandName = string.Empty;
            // this.styleCode = string.Empty;
            // this.noofPieces = 0;
            // this.purchasePrice = 0;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the price.
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
        /// Gets the actual selling price.
        /// </summary>
        public double ActualSellingPrice { get; private set; }

        /// <summary>
        /// Gets or sets Brand Name for particular item
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Gets or sets Style Code for particular item
        /// </summary>
        public string StyleCode { get; set; }

        /// <summary>
        /// Gets or sets value indicating No. of Pieces for particular item
        /// </summary>
        public int NoofPiecesSold { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        public double TotalAmount
        {
            get
            {
                return this.totalAmount;
            }

            set
            {
                this.totalAmount = value;
                this.RaisePropertyChanged("TotalAmount");
            }
        }

        /// <summary>
        /// Gets or sets Total Amount for All the Items
        /// </summary>
        public double PurchasePrice { get; set; }
    }
}
