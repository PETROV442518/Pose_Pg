namespace POSE.Domain
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Patient" />
    /// </summary>
    public class Patient : PoseUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        public Patient()
        {
            this.Prescriptions = new HashSet<Prescription>();
            this.Receipts = new HashSet<Receipt>();
            this.Examinations = new HashSet<Examination>();
            this.Diagnoses = new HashSet<Diagnosis>();
            this.SpentDiseases = new HashSet<Disease>();
            this.TakenDrugs = new HashSet<Drug>();
            this.TestResults = new HashSet<TestResult>();
        }

        /// <summary>
        /// Gets or sets the Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the PIN
        /// </summary>
        public string PIN { get; set; }

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
        /// Gets or sets the TestResults
        /// </summary>
        public ICollection<TestResult> TestResults { get; set; }

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
        public decimal DoctorScore { get; set; } = 4.00M;

        /// <summary>
        /// Gets or sets the StoreScore
        /// </summary>
        public decimal StoreScore { get; set; } = 4.00M;
    }
}
