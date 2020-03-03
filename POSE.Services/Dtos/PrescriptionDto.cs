namespace POSE.Services.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PrescriptionDto" />
    /// </summary>
    public class PrescriptionDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionDto"/> class.
        /// </summary>
        public PrescriptionDto()
        {
            this.Drugs = new List<DrugDto>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the IssuedOn
        /// </summary>
        public DateTime IssuedOn { get; set; }

        /// <summary>
        /// Gets or sets the TreatmentDuration
        /// </summary>
        public int TreatmentDuration { get; set; }

        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the Patient
        /// </summary>
        public PatientDto Patient { get; set; }

        /// <summary>
        /// Gets or sets the DoctorId
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// Gets or sets the DrugStoreId
        /// </summary>
        public string DrugStoreId { get; set; }

        /// <summary>
        /// Gets or sets the DiagnosisId
        /// </summary>
        public string DiagnosisId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsExecuted
        /// </summary>
        public bool IsExecuted { get; set; } = false;

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public ICollection<DrugDto> Drugs { get; set; }
    }
}
