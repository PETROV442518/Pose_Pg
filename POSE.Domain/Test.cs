namespace POSE.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Test" />
    /// </summary>
    public class Test
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        public Test()
        {
            this.TestResults = new HashSet<TestResult>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
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
        /// Gets or sets the TestResults
        /// </summary>
        public ICollection<TestResult> TestResults { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
