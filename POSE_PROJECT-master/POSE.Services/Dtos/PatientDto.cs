namespace POSE.Services.Dtos
{
    using POSE.Domain;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PatientDto" />
    /// </summary>
    public class PatientDto
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
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the DoctorId
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the Doctor
        /// </summary>
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Gets or sets the PreferedDrugStoreId
        /// </summary>
        public string PreferedDrugStoreId { get; set; }

        /// <summary>
        /// Gets or sets the PreferedDrugStore
        /// </summary>
        public DrugStore PreferedDrugStore { get; set; }

        /// <summary>
        /// Gets or sets the SumForDrugs
        /// </summary>
        public decimal SumForDrugs { get; set; } = 0;

        /// <summary>
        /// Gets or sets the AllergicTo
        /// </summary>
        public string AllergicTo { get; set; }

        /// <summary>
        /// Gets or sets the Prescriptions
        /// </summary>
        public ICollection<Prescription> Prescriptions { get; set; }

        /// <summary>
        /// Gets or sets the Receipts
        /// </summary>
        public ICollection<Receipt> Receipts { get; set; }

        /// <summary>
        /// Gets or sets the Examinations
        /// </summary>
        public ICollection<Examination> Examinations { get; set; }

        /// <summary>
        /// Gets or sets the Diagnoses
        /// </summary>
        public ICollection<Diagnosis> Diagnoses { get; set; }

        /// <summary>
        /// Gets or sets the SpentDiseases
        /// </summary>
        public ICollection<Disease> SpentDiseases { get; set; }

        /// <summary>
        /// Gets or sets the TakenDrugs
        /// </summary>
        public ICollection<Drug> TakenDrugs { get; set; }

        /// <summary>
        /// Gets or sets the DoctorScore
        /// </summary>
        public decimal DoctorScore { get; set; }

        /// <summary>
        /// Gets or sets the StoreScore
        /// </summary>
        public decimal StoreScore { get; set; }
    }
}
