namespace POSE.Web.Areas.Admin.Pages.Account
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using POSE.Domain;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="AdminProfileModel" />
    /// </summary>
    public class AdminProfileModel : PageModel
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
        private readonly ILogger<AdminProfileModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminProfileModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="logger">The logger<see cref="ILogger{AdminProfileModel}"/></param>
        public AdminProfileModel(
            UserManager<PoseUser> userManager,
            SignInManager<PoseUser> signInManager,
            ILogger<AdminProfileModel> logger)
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
            /// Gets or sets the FullName
            /// </summary>
            public string FullName { get; set; }

            /// <summary>
            /// Gets or sets the Address
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// Gets or sets the Email
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the PhoneNumber
            /// </summary>
            public string PhoneNumber { get; set; }
        }

        /// <summary>
        /// The OnGet
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!User.IsInRole("Admin"))
            {
                return Redirect("~/");
            }

            Input = new InputModel
            {
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber

            };

            return Page();
        }
    }
}
