namespace POSE.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Drug" />
    /// </summary>
    public class Drug
    {
        //Name
        /// <summary>
        /// Defines the NameMinLength
        /// </summary>
        private const int NameMinLength = 3;

        /// <summary>
        /// Defines the NameMaxLength
        /// </summary>
        private const int NameMaxLength = 30;

        /// <summary>
        /// Defines the NameErrorMsg
        /// </summary>
        private const string NameErrorMsg = "Full name must be at least {2} and at max {1} characters long.";

        //GenericGroup
        /// <summary>
        /// Defines the GenericMinLength
        /// </summary>
        private const int GenericMinLength = 3;

        /// <summary>
        /// Defines the GenericthMaxLength
        /// </summary>
        private const int GenericthMaxLength = 30;

        /// <summary>
        /// Defines the GenericMinLengthErrorMsg
        /// </summary>
        private const string GenericMinLengthErrorMsg = "Generic group name must at least {2} and at max {1} characters long.";

        //Producer
        /// <summary>
        /// Defines the ProducerMinLength
        /// </summary>
        private const int ProducerMinLength = 3;

        /// <summary>
        /// Defines the ProducerMaxLength
        /// </summary>
        private const int ProducerMaxLength = 30;

        /// <summary>
        /// Defines the ProducerMinLengthErrorMsg
        /// </summary>
        private const string ProducerMinLengthErrorMsg = "Producer name must be at least {2} and at max {1} characters long.";

        //Producer
        /// <summary>
        /// Defines the LeaflatMinLength
        /// </summary>
        private const int LeaflatMinLength = 3;

        /// <summary>
        /// Defines the LeaflatMaxLength
        /// </summary>
        private const int LeaflatMaxLength = 800;

        /// <summary>
        /// Defines the LeaflatErrorMsg
        /// </summary>
        private const string LeaflatErrorMsg = "Leaflat must  at least {2} and at max {1} characters long.";

        /// <summary>
        /// Initializes a new instance of the <see cref="Drug"/> class.
        /// </summary>
        public Drug()
        {
            this.Ingredients = new List<string>();
            this.PrescriptionIds = new List<string>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        /// 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        [StringLength(NameMaxLength, ErrorMessage = NameErrorMsg, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the GenericGroup
        /// </summary>
        [Required]
        [StringLength(GenericthMaxLength, ErrorMessage = GenericMinLengthErrorMsg, MinimumLength = GenericMinLength)]
        public string GenericGroup { get; set; }

        /// <summary>
        /// Gets or sets the Producer
        /// </summary>
        [Required]
        [StringLength(ProducerMaxLength, ErrorMessage = ProducerMinLengthErrorMsg, MinimumLength = ProducerMinLength)]
        public string Producer { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        [Required]
        public DrugType Type { get; set; }

        /// <summary>
        /// Gets or sets the Form
        /// </summary>
        [Required]
        public DrugForm Form { get; set; }

        /// <summary>
        /// Gets or sets the Dose
        /// </summary>
        [Required]
        public decimal Dose { get; set; }

        /// <summary>
        /// Gets or sets the DosesPerDay
        /// </summary>
        [Required]
        public int DosesPerDay { get; set; }

        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the Leaflat
        /// </summary>
        [Required]
        [StringLength(LeaflatMaxLength, ErrorMessage = LeaflatErrorMsg, MinimumLength = LeaflatMinLength)]
        [DataType(DataType.MultilineText)]
        public string Leaflat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Gets or sets the Ingredients
        /// </summary>
        [NotMapped]
        public ICollection<string> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets the PrescriptionIds
        /// </summary>
        [NotMapped]
        public List<string> PrescriptionIds { get; set; }
    }
}
