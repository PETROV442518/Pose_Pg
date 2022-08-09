namespace POSE.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Receipt" />
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Receipt"/> class.
        /// </summary>
        public Receipt()
        {
            this.Drugs = new HashSet<Drug>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        /// 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Fee
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Fee { get; set; }

        /// <summary>
        /// Gets or sets the IssuedOn
        /// </summary>
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the DrugStoreId
        /// </summary>
        [Required]
        public string DrugStoreId { get; set; }

        /// <summary>
        /// Gets or sets the DrugStore
        /// </summary>
        public DrugStore DrugStore { get; set; }

        /// <summary>
        /// Gets or sets the ClientId
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the Client
        /// </summary>
        public Patient Client { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public ICollection<Drug> Drugs { get; set; }
    }
}
