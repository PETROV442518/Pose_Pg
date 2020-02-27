namespace PROJECT_POSE.Models.Receipt
{
    using POSE.Services.Dtos;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AllReceiptsViewModel" />
    /// </summary>
    public class AllReceiptsViewModel
    {
        /// <summary>
        /// Gets or sets the Receipts
        /// </summary>
        public List<ReceiptDto> Receipts { get; set; }
    }
}
