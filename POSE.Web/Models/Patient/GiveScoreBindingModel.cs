namespace PROJECT_POSE.Models.Patient
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="GiveScoreBindingModel" />
    /// </summary>
    public class GiveScoreBindingModel
    {
        /// <summary>
        /// Defines the MinValue
        /// </summary>
        private const string MinValue = "2";

        /// <summary>
        /// Defines the MaxValue
        /// </summary>
        private const string MaxValue = "6";

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        [Required]
        [Range(typeof(decimal), MinValue, MaxValue)]
        public decimal Score { get; set; }
    }
}
