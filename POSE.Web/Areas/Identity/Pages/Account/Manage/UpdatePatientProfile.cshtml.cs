namespace PROJECT_POSE.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using IEmailSender = POSE.Services.IEmailSender;

    /// <summary>
    /// Defines the <see cref="PatientProfileModel" />
    /// </summary>
    public partial class PatientProfileModel : PageModel
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
        /// Defines the _ingredientsServices
        /// </summary>
        private readonly InterfaceIngredientsServices _ingredientsServices;

        /// <summary>
        /// Defines the _accountServices
        /// </summary>
        private readonly IAccountServices _accountServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientProfileModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        /// <param name="ingredientsServices">The ingredientsServices<see cref="InterfaceIngredientsServices"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        public PatientProfileModel(
            UserManager<PoseUser> userManager,
            SignInManager<PoseUser> signInManager,
            IEmailSender emailSender,
            InterfaceIngredientsServices ingredientsServices,
            IAccountServices accountServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _ingredientsServices = ingredientsServices;
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
        /// Gets or sets the DoctorName
        /// </summary>
        [BindProperty]
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets the DrugStore
        /// </summary>
        [BindProperty]
        public string DrugStore { get; set; }

        /// <summary>
        /// Gets or sets the DrugIngredients
        /// </summary>
        [BindProperty]
        public List<string> DrugIngredients { get; set; }

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
            private const string FullNameErrorMessage = "FullName must be must be at least {2} and at max {1} characters long.";

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
            private const string AddressErrorMessage = "Username must be must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the MinAge
            /// </summary>
            private const int MinAge = 16;

            /// <summary>
            /// Defines the MaxAge
            /// </summary>
            private const int MaxAge = 100;

            /// <summary>
            /// Defines the AgeErroMsg
            /// </summary>
            private const string AgeErroMsg = "Age must be must be at least {2} and at max {1}.";

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
            [Range(MinAge, MaxAge, ErrorMessage = AgeErroMsg)]
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
        }

        /// <summary>
        /// The OnGetAsync
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGetAsync()
        {

            if (!User.IsInRole("Patient"))
            {
                return Redirect("/");
            }
            var patient = await _userManager.GetUserAsync(User);
            if (patient == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var user = _accountServices.ReturnPatient(patient.UserGuid);

            var fullName = user.FullName;
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var doctors = _userManager.Users.Where(a => a.Role == UserRole.Doctor).ToList();
            List<string> outputDoctors = new List<string>();
            for (int i = 0; i < doctors.Count; i++)
            {
                outputDoctors.Add($"{doctors[i].FullName} --- {doctors[i].Address} --- {doctors[i].UserName}");
            }
            var drugStores = _accountServices.ReturnAllDrugStoreDtos();

            //InputModel
            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Address = user.Address,
                Age = user.Age,
                FullName = user.FullName,
            };
            //Properties
            DrugIngredients = _ingredientsServices.IngredientNames().ToList();
            if (user.DoctorId == null)
            {
                DoctorName = "None";
            }
            else
            {
                DoctorName = user.Doctor.FullName;
            }
            if (user.PreferedDrugStoreId == null)
            {
                DrugStore = "None";
            }
            else
            {
                DrugStore = user.PreferedDrugStore.FullName;
            }

            //ViewData
            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            ViewData["doctors"] = outputDoctors;
            ViewData["drugStores"] = drugStores;
            ViewData["doctor"] = DoctorName;
            ViewData["drugStore"] = DrugStore;
            ViewData["ingredients"] = DrugIngredients;
            ViewData["alergens"] = _ingredientsServices.IngredientNames();
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

            var patient = await _userManager.GetUserAsync(User);
            if (patient.IsDeleted == true)
            { patient = null; }
            if (patient == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            //Email
            var user = _accountServices.ReturnPatient(patient.UserGuid);
            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }
            //Email
            //PhoneNumber
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
            //PhoneNumber
            //Age
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
            //Age
            //Address
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
            //Address
            //FullName
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
            //FullName
            //DrugStore
            if (DrugStore != "None" && DrugStore != null)
            {

                if (user.PreferedDrugStoreId == null)
                {
                    var drugStoreDto = this._accountServices.ReturnDrugStore(DrugStore);
                    if (drugStoreDto != null)
                    {
                        var drugStoreId = await this._accountServices.ReturnId(drugStoreDto.UserGuid);
                        var store = this._userManager.FindByIdAsync(drugStoreId);
                        // user.PreferedDrugStore = _accountServices.ReturnDrugStore(drugStore.UserGuid);
                        user.PreferedDrugStoreId = drugStoreId;
                        if (user.PreferedDrugStoreId != drugStoreId)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting prefered drug store for user with ID '{userId}'.");
                        }
                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        throw new InvalidOperationException($"Unexpected error occurred setting prefered drug store for user with ID '{userId}'.");
                    }
                }
                else
                {
                    var drugStoreDto = this._accountServices.ReturnDrugStore(DrugStore);
                    if (drugStoreDto != null)
                    {
                        var drugStoreId = await this._accountServices.ReturnId(drugStoreDto.UserGuid);

                        if (user.PreferedDrugStoreId != drugStoreId)
                        {
                            user.PreferedDrugStoreId = drugStoreId;
                            if (user.PreferedDrugStoreId != drugStoreId)
                            {
                                var userId = await _userManager.GetUserIdAsync(user);
                                throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                            }
                            await _userManager.UpdateAsync(user);
                        }
                    }
                    else
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        throw new InvalidOperationException($"Unexpected error occurred setting prefered drug store for user with ID '{userId}'.");
                    }
                }
            }
            //DrugStore
            //Doctor
            if (DoctorName != "None registered" && DoctorName != null)
            {
                var InputDoctorArray = DoctorName.Split(" --- ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                var inputDoctorName = InputDoctorArray[InputDoctorArray.Count() - 1];
                if (user.DoctorId != null)
                {
                    var doctorName = _userManager.Users.FirstOrDefault(a => a.Id == user.DoctorId).UserName;
                    if (doctorName != inputDoctorName)
                    {
                        var doctor = _userManager.Users.FirstOrDefault
                    (a => a.UserName == InputDoctorArray[2] && a.Role == UserRole.Doctor);
                        if (doctor != null)
                        {
                            var userDoctor = _accountServices.ReturnDoctor(doctor.UserGuid);
                            var doctorId = await _userManager.GetUserIdAsync(doctor);
                            user.DoctorId = doctorId;
                            user.Doctor = userDoctor;
                            if (user.DoctorId != doctorId)
                            {
                                var userId = await _userManager.GetUserIdAsync(user);
                                throw new InvalidOperationException($"Unexpected error occurred setting Doctor for user with ID '{doctorId}'.");
                            }
                            await _userManager.UpdateAsync(user);
                        }

                    }
                }
                else
                {
                    var doctor = _userManager.Users.FirstOrDefault
                    (a => a.UserName == inputDoctorName && a.Role == UserRole.Doctor);
                    if (doctor != null)
                    {
                        var userDoctor = _accountServices.ReturnDoctor(doctor.UserGuid);
                        var doctorId = await _userManager.GetUserIdAsync(doctor);
                        user.DoctorId = doctorId;
                        user.Doctor = userDoctor;
                        if (user.DoctorId != doctorId)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting Doctor for user with ID '{doctorId}'.");
                        }
                        await _userManager.UpdateAsync(user);
                    }
                }
            }
            //Doctor
            //Allergens
            if (DrugIngredients != null && DrugIngredients.Count != 0)
            {
                if (user.AllergicTo != null && user.AllergicTo != "None" && user.AllergicTo != "")
                {
                    var userAllergens = user.AllergicTo.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!DrugIngredients.Contains("None"))
                    {
                        foreach (var name in DrugIngredients)
                        {
                            if (!userAllergens.Contains(name))
                            {
                                userAllergens.Add(name);
                                user.AllergicTo = string.Join(", ", userAllergens);
                                var result = await _userManager.UpdateAsync(user);
                                if (!result.Succeeded)
                                {
                                    var userId = await _userManager.GetUserIdAsync(user);
                                    throw new InvalidOperationException($"Unexpected error occurred setting allergens for user with ID '{userId}'.");
                                }
                            }
                        }
                    }
                    else
                    {
                        user.AllergicTo = "None";
                        var result = await _userManager.UpdateAsync(user);
                        if (!result.Succeeded)
                        {
                            var userId = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Unexpected error occurred setting allergens for user with ID '{userId}'.");
                        }
                    }
                }
                else if (user.AllergicTo == null)
                {
                    user.AllergicTo = string.Join(", ", DrugIngredients);
                    var result = await _userManager.UpdateAsync(user);
                }
                else if (user.AllergicTo == "None")
                {
                    if (!DrugIngredients.Contains("None"))
                    {
                        user.AllergicTo = user.AllergicTo = string.Join(", ", DrugIngredients);
                        var result = await _userManager.UpdateAsync(user);
                    }
                }
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return Redirect("/Patient/PatientProfile");
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
