namespace POSE.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Disease" />
    /// </summary>
    public class Disease
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
        private const string NameErrorMsg = "Full name must be must be at least {2} and at max {1} characters long.";

        //Description
        /// <summary>
        /// Defines the DescrMinLength
        /// </summary>
        private const int DescrMinLength = 3;

        /// <summary>
        /// Defines the DescrMaxLength
        /// </summary>
        private const int DescrMaxLength = 800;

        /// <summary>
        /// Defines the DescrErrorMsg
        /// </summary>
        private const string DescrErrorMsg = "Description must be must be at least {2} and at max {1} characters long.";

        //Treatment
        /// <summary>
        /// Defines the TreatMinLength
        /// </summary>
        private const int TreatMinLength = 3;

        /// <summary>
        /// Defines the TreatMaxLength
        /// </summary>
        private const int TreatMaxLength = 800;

        /// <summary>
        /// Defines the TraatErrorMsg
        /// </summary>
        private const string TraatErrorMsg = "Treatment must be must be at least {2} and at max {1} characters long.";

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
        /// Gets or sets the Description
        /// </summary>
        [Required]
        [StringLength(DescrMaxLength, ErrorMessage = DescrErrorMsg, MinimumLength = DescrMinLength)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Contagious
        /// </summary>
        [Required]
        public bool Contagious { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Lethal
        /// </summary>
        [Required]
        public bool Lethal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Curable
        /// </summary>
        [Required]
        public bool Curable { get; set; }

        /// <summary>
        /// Gets or sets the Treatment
        /// </summary>
        [Required]
        [StringLength(TreatMaxLength, ErrorMessage = TraatErrorMsg, MinimumLength = TreatMinLength)]
        [DataType(DataType.MultilineText)]
        public string Treatment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
