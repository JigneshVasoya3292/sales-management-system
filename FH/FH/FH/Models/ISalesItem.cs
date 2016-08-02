// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISalesItem.cs" company="Dextech">
//   Copyright (C) 2014 Dextech.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Models
{
    /// <summary>
    /// The SalesItem interface.
    /// </summary>
    public interface ISalesItem
    {
        /// <summary>
        /// Gets or sets Brand Name for particular item
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets Style Code for particular item
        /// </summary>
        double SellingPrice { get; set; }

        /// <summary>
        /// Gets Style Code for particular item
        /// </summary>
        double ActualSellingPrice { get; }

        /// <summary>
        /// Gets or sets Brand Name for particular item
        /// </summary>
        string BrandName { get; set; }

        /// <summary>
        /// Gets or sets Style Code for particular item
        /// </summary>
        string StyleCode { get; set; }

        /// <summary>
        /// Gets or sets value indicating No. of Pieces for particular item
        /// </summary>
        int NoofPiecesSold { get; set; }

        /// <summary>
        /// Gets or sets value indicating No. of Pieces for particular item
        /// </summary>
        double TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets Total Amount for All the Items
        /// </summary>
        double PurchasePrice { get; set; } 
    }
}
