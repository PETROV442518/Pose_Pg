namespace POSE.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="Doctor" />
    /// </summary>
    public class Doctor : PoseUser
    {
        /// <summary>
        /// Defines the MinAge
        /// </summary>
        private const int MinAge = 25;

        /// <summary>
        /// Defines the MaxAge
        /// </summary>
        private const int MaxAge = 80;

        /// <summary>
        /// Defines the AgeErrorMessage
        /// </summary>
        private const string AgeErrorMessage = "Doctor's age must be between {2} and {1} years!";

        /// <summary>
        /// Defines the UDNLength
        /// </summary>
        private const int UDNLength = 10;

        /// <summary>
        /// Defines the UDNErrorMEssage
        /// </summary>
        private const string UDNErrorMEssage = "The {0} must be {1} characters long.";

        /// <summary>
        /// Initializes a new instance of the <see cref="Doctor"/> class.
        /// </summary>
        public Doctor()
        {
            this.Patients = new HashSet<Patient>();
            this.Examinations = new HashSet<Examination>();
            this.Diagnoses = new HashSet<Diagnosis>();
            this.Prescriptions = new HashSet<Prescription>();
        }

        /// <summary>
        /// Gets or sets the Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the UDN
        /// </summary>
        public string UDN { get; set; }

        /// <summary>
        /// Gets or sets the Specialty
        /// </summary>
        [Required]
        public Specialty Specialty { get; set; }

        /// <summary>
        /// Gets or sets the PatientsScore
        /// </summary>
        public decimal PatientsScore { get; set; }

        /// <summary>
        /// Gets or sets the Patients
        /// </summary>
        public ICollection<Patient> Patients { get; set; }

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
