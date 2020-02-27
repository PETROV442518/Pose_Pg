namespace PROJECT_POSE.Models.Receipt
{
    using POSE.Services.Dtos;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ReceiptDetailsViewModel" />
    /// </summary>
    public class ReceiptDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the DrugStoreDto
        /// </summary>
        public DrugStoreDto DrugStoreDto { get; set; }

        /// <summary>
        /// Gets or sets the ReceiptId
        /// </summary>
        public string ReceiptId { get; set; }

        /// <summary>
        /// Gets or sets the Fee
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Gets or sets the IssuedOn
        /// </summary>
        public DateTime IssuedOn { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public List<DrugDto> Drugs { get; set; }
    }
}
