namespace POSE.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="DrugStore" />
    /// </summary>
    public class DrugStore : PoseUser
    {
        /// <summary>
        /// Defines the CINLength
        /// </summary>
        private const int CINLength = 10;

        /// <summary>
        /// Defines the CINErrorMEssage
        /// </summary>
        private const string CINErrorMEssage = "The {0} must be {1} characters long.";

        /// <summary>
        /// Initializes a new instance of the <see cref="DrugStore"/> class.
        /// </summary>
        public DrugStore()
        {
            this.Prescriptions = new HashSet<Prescription>();
            this.Drugs = new HashSet<Drug>();
            this.Clients = new HashSet<Patient>();
            this.Receipts = new HashSet<Receipt>();
        }

        /// <summary>
        /// Gets or sets the CIN
        /// </summary>
        [Required]
        [StringLength(CINLength, ErrorMessage = CINErrorMEssage, MinimumLength = CINLength)]
        public string CIN { get; set; }

        /// <summary>
        /// Gets or sets the Prescriptions
        /// </summary>
        public ICollection<Prescription> Prescriptions { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public ICollection<Drug> Drugs { get; set; }

        /// <summary>
        /// Gets or sets the Clients
        /// </summary>
        public ICollection<Patient> Clients { get; set; }

        /// <summary>
        /// Gets or sets the Receipts
        /// </summary>
        public ICollection<Receipt> Receipts { get; set; }

        /// <summary>
        /// Gets or sets the DrugStoreScore
        /// </summary>
        [Range(2.00, 6.00)]
        public decimal DrugStoreScore { get; set; } = 4.00M;
    }
}
