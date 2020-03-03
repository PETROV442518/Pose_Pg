namespace PROJECT_POSE.Models.Receipt
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="CreateReceiptBindingModel" />
    /// </summary>
    public class CreateReceiptBindingModel
    {
        /// <summary>
        /// Gets or sets the PrescriptionId
        /// </summary>
        [BindProperty]
        [Required]
        public string PrescriptionId { get; set; }
    }
}
