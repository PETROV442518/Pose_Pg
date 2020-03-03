namespace PROJECT_POSE.Models.Account
{
    /// <summary>
    /// Defines the <see cref="DoctorProfileViewModel" />
    /// </summary>
    public class DoctorProfileViewModel
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
        /// Gets or sets the Specialty
        /// </summary>
        public string Specialty { get; set; }

        /// <summary>
        /// Gets or sets the PatientsCount
        /// </summary>
        public int PatientsCount { get; set; }

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        public decimal Score { get; set; }
    }
}
