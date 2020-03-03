namespace PROJECT_POSE.Models.Examination
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="PerformExaminationBindingModel" />
    /// </summary>
    public class PerformExaminationBindingModel
    {
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
        /// Defines the TreatmentMinDuration
        /// </summary>
        private const int TreatmentMinDuration = 0;

        /// <summary>
        /// Defines the TreatmentMaxDuration
        /// </summary>
        private const int TreatmentMaxDuration = 90;

        /// <summary>
        /// Defines the TreatmentDurationErrorMessage
        /// </summary>
        private const string TreatmentDurationErrorMessage = "Treatment duration must be between {1} and {2} days";

        /// <summary>
        /// Gets or sets the UserGuid
        /// </summary>
        [BindProperty]
        [Required]
        public string UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        [BindProperty]
        [Required]
        [StringLength(DescriptionMaxLength, ErrorMessage = DescriptionErrorMsg, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ReturnUrl
        /// </summary>
        [BindProperty]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the TreatmentDuration
        /// </summary>
        [Range(TreatmentMinDuration, TreatmentMaxDuration, ErrorMessage = TreatmentDurationErrorMessage)]
        [Required]
        [BindProperty]
        public int TreatmentDuration { get; set; }

        /// <summary>
        /// Gets or sets the Diseases
        /// </summary>
        [BindProperty]
        public List<string> Diseases { get; set; }

        /// <summary>
        /// Gets or sets the TestResults
        /// </summary>
        [BindProperty]
        public List<string> TestResults { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        [BindProperty]
        public List<string> Drugs { get; set; }

        /// <summary>
        /// Gets or sets the StoreId
        /// </summary>
        [BindProperty]
        [Required]
        public string StoreId { get; set; }
    }
}
