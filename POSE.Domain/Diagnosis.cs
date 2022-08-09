namespace POSE.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the <see cref="Diagnosis" />
    /// </summary>
    public class Diagnosis
    {
        //Description
        /// <summary>
        /// Defines the DescriptionMinLength
        /// </summary>
        private const int DescriptionMinLength = 3;

        /// <summary>
        /// Defines the DescriptionMaxLength
        /// </summary>
        private const int DescriptionMaxLength = 800;

        /// <summary>
        /// Defines the DescriptionErrorMsg
        /// </summary>
        private const string DescriptionErrorMsg = "You must enter description with at least {2} symbols and maximum {1}";

        /// <summary>
        /// Initializes a new instance of the <see cref="Diagnosis"/> class.
        /// </summary>
        public Diagnosis()
        {
            this.Diseases = new HashSet<Disease>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        /// 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(DescriptionMaxLength, ErrorMessage = DescriptionErrorMsg, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

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
        /// Gets or sets the Diseases
        /// </summary>
        public ICollection<Disease> Diseases { get; set; }

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
