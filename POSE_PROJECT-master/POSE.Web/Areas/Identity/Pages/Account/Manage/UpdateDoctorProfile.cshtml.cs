﻿namespace PROJECT_POSE.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using IEmailSender = POSE.Services.IEmailSender;

    /// <summary>
    /// Defines the <see cref="DoctorProfileModel" />
    /// </summary>
    public partial class DoctorProfileModel : PageModel
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
        /// Initializes a new instance of the <see cref="DoctorProfileModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        public DoctorProfileModel(
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
        /// Gets or sets the Specialty
        /// </summary>
        [BindProperty]
        public string Specialty { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />
        /// </summary>
        public class InputModel
        {
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
            private const string AgeErrorMessage = "Doctor's age must be between 25 and 80 years!";

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
            /// Gets or sets the Age
            /// </summary>
            [Range(MinAge, MaxAge, ErrorMessage = AgeErrorMessage)]
            public int Age { get; set; }

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
            /// Gets or sets the Specialty
            /// </summary>
            [BindProperty]
            public string Specialty { get; set; }
        }

        /// <summary>
        /// The OnGetAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("/");
            }
            var doctor = await _userManager.GetUserAsync(User);
            if (doctor.IsDeleted == true)
            { doctor = null; }
            if (doctor == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var user = _accountServices.ReturnDoctor(doctor.UserGuid);
            var fullName = user.FullName;
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);



            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Address = user.Address,
                Age = user.Age,
                FullName = user.FullName,
                Specialty = user.Specialty.ToString()
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            ViewData["Specialties"] = Enum.GetNames(typeof(Specialty)).ToList();
            ViewData["Specialty"] = user.Specialty.ToString();
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

            var doctor = await _userManager.GetUserAsync(User);
            if (doctor.IsDeleted == true)
            { doctor = null; }
            if (doctor == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var user = _accountServices.ReturnDoctor(doctor.UserGuid);
            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
                user.EmailConfirmed = false;
                await _userManager.UpdateAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            if (Specialty != user.Specialty.ToString())
            {
                user.Specialty = (Specialty)Enum.Parse(typeof(Specialty), Specialty, true);
                if (user.Specialty.ToString() != Specialty)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
                await _userManager.UpdateAsync(user);
            }

            if (Input.Age != user.Age)
            {
                user.Age = Input.Age;
                if (user.Age != Input.Age)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
                await _userManager.UpdateAsync(user);

            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
                if (user.Address != Input.Address)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
                await _userManager.UpdateAsync(user);
            }

            if (Input.FullName != user.FullName)
            {
                user.FullName = Input.FullName;
                if (user.FullName != Input.FullName)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return LocalRedirect("/Doctor/DoctorProfile");
        }

        /// <summary>
        /// The OnPostSendVerificationEmailAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage("/");
        }
    }
}
