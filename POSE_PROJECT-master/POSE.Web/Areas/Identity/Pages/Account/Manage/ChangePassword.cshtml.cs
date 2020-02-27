namespace PROJECT_POSE.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using POSE.Domain;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ChangePasswordModel" />
    /// </summary>
    public class ChangePasswordModel : PageModel
    {
        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Defines the _signInManager
        /// </summary>
        private readonly SignInManager<PoseUser> _signInManager;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<ChangePasswordModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="logger">The logger<see cref="ILogger{ChangePasswordModel}"/></param>
        public ChangePasswordModel(
            UserManager<PoseUser> userManager,
            SignInManager<PoseUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets or sets the Input
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Gets or sets the StatusMessage
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Defines the PasswordMinLenght
            /// </summary>
            private const int PasswordMinLenght = 6;

            /// <summary>
            /// Defines the PasswordMaxLength
            /// </summary>
            private const int PasswordMaxLength = 100;

            /// <summary>
            /// Defines the PasswordErrorMessage
            /// </summary>
            private const string PasswordErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Gets or sets the OldPassword
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            /// <summary>
            /// Gets or sets the NewPassword
            /// </summary>
            [Required]
            [StringLength(PasswordMaxLength, ErrorMessage = PasswordErrorMessage, MinimumLength = PasswordMinLenght)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            /// <summary>
            /// Gets or sets the ConfirmPassword
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        /// <summary>
        /// The OnGetAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.IsDeleted == true)
            { user = null; }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        /// <summary>
        /// The OnPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user.IsDeleted == true)
            { user = null; }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return Redirect("~/");
        }
    }
}
