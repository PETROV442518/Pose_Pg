namespace PROJECT_POSE.Models.Account
{
    /// <summary>
    /// Defines the <see cref="DrugStoreProfileViewModel" />
    /// </summary>
    public class DrugStoreProfileViewModel
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
        /// Gets or sets the PrescriptionsForToday
        /// </summary>
        public int PrescriptionsForToday { get; set; }
    }
}
