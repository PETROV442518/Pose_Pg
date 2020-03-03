namespace POSE.Domain.Settings
{
    /// <summary>
    /// Defines the <see cref="EmailSettings" />
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the MailServer
        /// </summary>
        public string MailServer { get; set; }

        /// <summary>
        /// Gets or sets the MailPort
        /// </summary>
        public int MailPort { get; set; }

        /// <summary>
        /// Gets or sets the SenderName
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the Sender
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }
    }
}
