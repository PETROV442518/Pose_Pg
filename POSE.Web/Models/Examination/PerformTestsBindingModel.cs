namespace PROJECT_POSE.Models.Examination
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="PerformTestsBindingModel" />
    /// </summary>
    public class PerformTestsBindingModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformTestsBindingModel"/> class.
        /// </summary>
        public PerformTestsBindingModel()
        {
            this.Tests = new List<string>();
        }

        /// <summary>
        /// Gets or sets the Patient
        /// </summary>
        [BindProperty]
        [Required]
        public string Patient { get; set; }

        /// <summary>
        /// Gets or sets the Tests
        /// </summary>
        [BindProperty]
        public List<string> Tests { get; set; }
    }
}
