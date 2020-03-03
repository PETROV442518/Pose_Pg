namespace PROJECT_POSE.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using POSE.Domain;
    using POSE.Services;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="RegisterDoctorModel" />
    /// </summary>
    [AllowAnonymous]
    public class RegisterDoctorModel : PageModel
    {
        /// <summary>
        /// Defines the _signInManager
        /// </summary>
        private readonly SignInManager<PoseUser> _signInManager;

        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Defines the _logger
        /// </summary>
        private readonly ILogger<RegisterPatientModel> _logger;

        /// <summary>
        /// Defines the _emailSender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterDoctorModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="logger">The logger<see cref="ILogger{RegisterPatientModel}"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        public RegisterDoctorModel(
            UserManager<PoseUser> userManager,
            SignInManager<PoseUser> signInManager,
            ILogger<RegisterPatientModel> logger,
            IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Gets or sets the Input
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Gets or sets the ReturnUrl
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Defines the UserNameMaxLength
            /// </summary>
            private const int UserNameMaxLength = 20;

            /// <summary>
            /// Defines the UsernameMinLength
            /// </summary>
            private const int UsernameMinLength = 3;

            /// <summary>
            /// Defines the UsernameErrorMessage
            /// </summary>
            private const string UsernameErrorMessage = "Username must be must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the PasswordMinLength
            /// </summary>
            private const int PasswordMinLength = 6;

            /// <summary>
            /// Defines the PasswordMaxLength
            /// </summary>
            private const int PasswordMaxLength = 100;

            /// <summary>
            /// Defines the PasswordErrorMessage
            /// </summary>
            private const string PasswordErrorMessage = "Password must be must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the MinAge
            /// </summary>
            private const int MinAge = 25;

            /// <summary>
            /// Defines the MaxAge
            /// </summary>
            private const int MaxAge = 80;

            /// <summary>
            /// Defines the AgeErrorMessage
            /// </summary>
            private const string AgeErrorMessage = "Doctor's age must be between {2} and {1} years!";

            /// <summary>
            /// Defines the UDNLength
            /// </summary>
            private const int UDNLength = 10;

            /// <summary>
            /// Defines the UDNErrorMEssage
            /// </summary>
            private const string UDNErrorMEssage = "The {0} must be {1} characters long.";

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
            /// Gets or sets the UserName
            /// </summary>
            [Required]
            [Display(Name = "Username")]
            [StringLength(UserNameMaxLength, ErrorMessage = UsernameErrorMessage, MinimumLength = UsernameMinLength)]
            public string UserName { get; set; }

            /// <summary>
            /// Gets or sets the Email
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the Password
            /// </summary>
            [Required]
            [StringLength(PasswordMaxLength, ErrorMessage = PasswordErrorMessage, MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            /// Gets or sets the ConfirmPassword
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            /// <summary>
            /// Gets or sets the Age
            /// </summary>
            [Range(MinAge, MaxAge, ErrorMessage = AgeErrorMessage)]
            public int Age { get; set; }

            /// <summary>
            /// Gets or sets the UDN
            /// </summary>
            [Required]
            [StringLength(UDNLength, ErrorMessage = UDNErrorMEssage, MinimumLength = UDNLength)]
            public string UDN { get; set; }

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

            /// <summary>
            /// Gets or sets the PhoneNumber
            /// </summary>
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
        }

        /// <summary>
        /// The OnGet
        /// </summary>
        /// <param name="returnUrl">The returnUrl<see cref="string"/></param>
        public void OnGet(string returnUrl = null)
        {

            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// The OnPostAsync
        /// </summary>
        /// <param name="returnUrl">The returnUrl<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Doctor();
                user.UserName = Input.UserName;
                user.Email = Input.Email;
                user.Role = UserRole.Doctor;
                user.Age = Input.Age;
                user.Address = Input.Address;
                user.FullName = Input.FullName;
                user.PhoneNumber = Input.PhoneNumber;
                user.UDN = Sha256(Input.UDN);
                user.Specialty = Specialty.Unknown;

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                       $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    await _userManager.AddToRoleAsync(user, "Doctor");

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

        /// <summary>
        /// The Sha256
        /// </summary>
        /// <param name="rawData">The rawData<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string Sha256(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
