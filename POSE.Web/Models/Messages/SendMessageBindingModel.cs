namespace PROJECT_POSE.Models.Messages
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="SendMessageBindingModel" />
    /// </summary>
    public class SendMessageBindingModel
    {
        /// <summary>
        /// Defines the SubjectMinLength
        /// </summary>
        private const int SubjectMinLength = 3;

        /// <summary>
        /// Defines the SubjectErrorMessage
        /// </summary>
        private const string SubjectErrorMessage = "You must enter subject with at least 3 symbols";

        /// <summary>
        /// Defines the MessageMinLength
        /// </summary>
        private const int MessageMinLength = 10;

        /// <summary>
        /// Defines the MessageError
        /// </summary>
        private const string MessageError = "You must enter message with at least 10 symbols";

        /// <summary>
        /// Defines the NameMinLength
        /// </summary>
        private const int NameMinLength = 3;

        /// <summary>
        /// Defines the NameErrorMessage
        /// </summary>
        private const string NameErrorMessage = "You must enter subject with at least 3 symbols";

        /// <summary>
        /// Gets or sets the Subject
        /// </summary>
        [BindProperty]
        [MinLength(SubjectMinLength, ErrorMessage = SubjectErrorMessage)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        [BindProperty]
        [MinLength(MessageMinLength, ErrorMessage = MessageError)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [BindProperty]
        [MinLength(NameMinLength, ErrorMessage = NameErrorMessage)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
