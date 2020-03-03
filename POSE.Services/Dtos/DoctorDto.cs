namespace POSE.Services.Dtos
{
    using POSE.Domain;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DoctorDto" />
    /// </summary>
    public class DoctorDto
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
        /// Gets or sets the Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Specialty
        /// </summary>
        public Specialty Specialty { get; set; }

        /// <summary>
        /// Gets or sets the PatientsScore
        /// </summary>
        public decimal PatientsScore { get; set; }

        /// <summary>
        /// Gets or sets the Patients
        /// </summary>
        public ICollection<PatientDto> Patients { get; set; }

        /// <summary>
        /// Gets or sets the Examinations
        /// </summary>
        public ICollection<Examination> Examinations { get; set; }

        /// <summary>
        /// Gets or sets the Diagnoses
        /// </summary>
        public ICollection<Diagnosis> Diagnoses { get; set; }

        /// <summary>
        /// Gets or sets the Prescriptions
        /// </summary>
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
