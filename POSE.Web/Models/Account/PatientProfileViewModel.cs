namespace PROJECT_POSE.Models.Account
{
    /// <summary>
    /// Defines the <see cref="PatientProfileViewModel" />
    /// </summary>
    public class PatientProfileViewModel
    {
        /// <summary>
        /// Gets or sets the ImageUrl
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the FullName
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the FeeForMedicaments
        /// </summary>
        public decimal FeeForMedicaments { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the DoctorName
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets the DrugStoreName
        /// </summary>
        public string DrugStoreName { get; set; }

        /// <summary>
        /// Gets or sets the AlergicToString
        /// </summary>
        public string AlergicToString { get; set; }

        /// <summary>
        /// Gets or sets the DrugStoreAddress
        /// </summary>
        public string DrugStoreAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether HasADoctor
        /// </summary>
        public bool HasADoctor { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether HasAStore
        /// </summary>
        public bool HasAStore { get; set; } = false;
    }
}
