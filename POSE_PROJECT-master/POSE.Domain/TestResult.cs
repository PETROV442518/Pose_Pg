namespace POSE.Domain
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="TestResult" />
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the TestId
        /// </summary>
        [Required]
        public string TestId { get; set; }

        /// <summary>
        /// Gets or sets the Test
        /// </summary>
        public Test Test { get; set; }

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
        /// Gets or sets the Result
        /// </summary>
        public Result Result { get; set; }
    }
}
