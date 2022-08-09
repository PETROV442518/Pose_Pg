namespace POSE.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Prescription" />
    /// </summary>
    public class Prescription
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        /// 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the IssuedOn
        /// </summary>
        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the TreatmentDuration
        /// </summary>
        [Required]
        public int TreatmentDuration { get; set; }

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
        /// Gets or sets the DrugStoreId
        /// </summary>
        [Required]
        public string DrugStoreId { get; set; }

        /// <summary>
        /// Gets or sets the DrugStore
        /// </summary>
        public DrugStore DrugStore { get; set; }

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
        /// Gets or sets a value indicating whether IsExecuted
        /// </summary>
        public bool IsExecuted { get; set; } = false;

        /// <summary>
        /// Gets or sets the DrugIds
        /// </summary>
        public string DrugIds { get; set; }
    }
}
