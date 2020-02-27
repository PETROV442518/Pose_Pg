namespace POSE.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Examination" />
    /// </summary>
    public class Examination
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Examination"/> class.
        /// </summary>
        public Examination()
        {
            this.Tests = new HashSet<TestResult>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        [Required]
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the Patient
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the DoctorId
        /// </summary>
        [Required]
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the Doctor
        /// </summary>
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the DiagnosisId
        /// </summary>
        [Required]
        public string DiagnosisId { get; set; }

        /// <summary>
        /// Gets or sets the Diagnosis
        /// </summary>
        public Diagnosis Diagnosis { get; set; }

        /// <summary>
        /// Gets or sets the Tests
        /// </summary>
        public ICollection<TestResult> Tests { get; set; }

        /// <summary>
        /// Gets or sets the PrescriptionId
        /// </summary>
        public string PrescriptionId { get; set; }

        /// <summary>
        /// Gets or sets the Prescription
        /// </summary>
        public Prescription Prescription { get; set; }
    }
}
