namespace POSE.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="PoseUser" />
    /// </summary>
    public class PoseUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the UserGuid
        /// </summary>
        public string UserGuid { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the FullName
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the Address
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        [Required]
        public UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
