namespace PROJECT_POSE.Areas.Admin.Pages.Account
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using IEmailSender = POSE.Services.IEmailSender;

    /// <summary>
    /// Defines the <see cref="UpdateAdminProfile" />
    /// </summary>
    public partial class UpdateAdminProfile : PageModel
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
        /// Defines the _emailSender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Defines the _accountServices
        /// </summary>
        private readonly IAccountServices _accountServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdminProfile"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        public UpdateAdminProfile(
            UserManager<PoseUser> userManager,
            SignInManager<PoseUser> signInManager,
            IEmailSender emailSender,
            IAccountServices accountServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _accountServices = accountServices;
        }

        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsEmailConfirmed
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the StatusMessage
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
            /// Defines the FullNameMinLength
            /// </summary>
            private const int FullNameMinLength = 3;

            /// <summary>
            /// Defines the FullNameMaxLength
            /// </summary>
            private const int FullNameMaxLength = 30;

            /// <summary>
            /// Defines the FullNameErrorMessage
            /// </summary>
            private const string FullNameErrorMessage = "Full name must be must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the AddressMinLength
            /// </summary>
            private const int AddressMinLength = 3;

            /// <summary>
            /// Defines the AddressMaxLength
            /// </summary>
            private const int AddressMaxLength = 100;

            /// <summary>
            /// Defines the AddressErrorMessage
            /// </summary>
            private const string AddressErrorMessage = "Address must be must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Gets or sets the Email
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the PhoneNumber
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            /// <summary>
            /// Gets or sets the FullName
            /// </summary>
            [Required]
            [StringLength(FullNameMaxLength, ErrorMessage = FullNameErrorMessage, MinimumLength = FullNameMinLength)]
            public string FullName { get; set; }

            /// <summary>
            /// Gets or sets the Address
            /// </summary>
            [Required]
            [StringLength(AddressMaxLength, ErrorMessage = AddressErrorMessage, MinimumLength = AddressMinLength)]
            public string Address { get; set; }
        }

        /// <summary>
        /// The OnGetAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("~/");
            }
            var admin = await _userManager.GetUserAsync(User);

            if (admin == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(admin);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(admin);
            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Address = admin.Address,
                FullName = admin.FullName,
            };
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
            var admin = await _userManager.GetUserAsync(User);
            if (admin == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(admin);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(admin, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(admin);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
                admin.EmailConfirmed = false;
                await _userManager.UpdateAsync(admin);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = admin.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(admin);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(admin, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(admin);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (Input.Address != admin.Address)
            {
                admin.Address = Input.Address;
                if (admin.Address != Input.Address)
                {
                    var userId = await _userManager.GetUserIdAsync(admin);
                    throw new InvalidOperationException($"Unexpected error occurred setting address for user with ID '{userId}'.");
                }
                await _userManager.UpdateAsync(admin);
            }

            if (Input.FullName != admin.FullName)
            {
                admin.FullName = Input.FullName;
                if (admin.FullName != Input.FullName)
                {
                    var userId = await _userManager.GetUserIdAsync(admin);
                    throw new InvalidOperationException($"Unexpected error occurred setting full name for user with ID '{userId}'.");
                }
                await _userManager.UpdateAsync(admin);
            }

            await _signInManager.RefreshSignInAsync(admin);
            StatusMessage = "Your profile has been updated";
            return LocalRedirect("/Admin/Account/AdminProfile");
        }
    }
}
