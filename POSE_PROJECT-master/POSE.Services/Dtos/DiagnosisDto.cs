namespace POSE.Services.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DiagnosisDto" />
    /// </summary>
    public class DiagnosisDto
    {
        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Patient
        /// </summary>
        public PatientDto Patient { get; set; }

        /// <summary>
        /// Gets or sets the Doctor
        /// </summary>
        public DoctorDto Doctor { get; set; }

        /// <summary>
        /// Gets or sets the Diseases
        /// </summary>
        public ICollection<DiseaseDto> Diseases { get; set; }

        /// <summary>
        /// Gets or sets the Prescription
        /// </summary>
        public PrescriptionDto Prescription { get; set; }
    }
}
