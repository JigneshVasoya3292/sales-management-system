// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInventoryItem.cs" company="Dextech">
//   Copyright (C) 2014 Dextech.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Models
{
    /// <summary>
    /// The InventoryItem interface.
    /// </summary>
    public interface IInventoryItem
    {
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
        int NoofPieces { get; set; }

        /// <summary>
        /// Gets or sets Purchase Price for an Item
        /// </summary>
        double PurchasePrice { get; set; }

        /// <summary>
        /// Gets or sets Selling Price for an Item
        /// </summary>
        double SellingPrice { get; set; }
    }
}
