namespace POSE.Services.Dtos
{
    using POSE.Domain;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DrugStoreDto" />
    /// </summary>
    public class DrugStoreDto
    {
        /// <summary>
        /// Gets or sets the UserGuid
        /// </summary>
        public string UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the FullName
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the CIN
        /// </summary>
        public string CIN { get; set; }

        /// <summary>
        /// Gets or sets the Prescriptions
        /// </summary>
        public List<Prescription> Prescriptions { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public List<Drug> Drugs { get; set; }

        /// <summary>
        /// Gets or sets the Clients
        /// </summary>
        public List<Patient> Clients { get; set; }

        /// <summary>
        /// Gets or sets the Receipts
        /// </summary>
        public List<Receipt> Receipts { get; set; }

        /// <summary>
        /// Gets or sets the DrugStoreScore
        /// </summary>
        public decimal DrugStoreScore { get; set; }
    }
}
