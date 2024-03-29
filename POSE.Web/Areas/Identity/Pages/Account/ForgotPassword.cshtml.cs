﻿namespace PROJECT_POSE.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using POSE.Services;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ForgotPasswordModel" />
    /// </summary>
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Defines the _emailSender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        public ForgotPasswordModel(UserManager<PoseUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Gets or sets the Input
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets the Email
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        /// <summary>
        /// The OnPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user.IsDeleted == true)
                { user = null; }
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPassword");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
