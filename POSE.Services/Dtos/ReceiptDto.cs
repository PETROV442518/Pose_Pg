namespace POSE.Services.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ReceiptDto" />
    /// </summary>
    public class ReceiptDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptDto"/> class.
        /// </summary>
        public ReceiptDto()
        {
            this.Drugs = new List<DrugDto>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Fee
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// Gets or sets the IssuedOn
        /// </summary>
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the DrugStoreId
        /// </summary>
        public string DrugStoreId { get; set; }

        /// <summary>
        /// Gets or sets the DrugStore
        /// </summary>
        public DrugStoreDto DrugStore { get; set; }

        /// <summary>
        /// Gets or sets the ClientId
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public ICollection<DrugDto> Drugs { get; set; }

        /// <summary>
        /// Gets or sets the QRCode
        /// </summary>
        public string QRCode { get; set; }
    }
}
