namespace POSE.Web.Areas.Admin.Pages.Create
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Services;
    using POSE.Services.Dtos;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CreateTestModel" />
    /// </summary>
    public class CreateTestModel : PageModel
    {
        /// <summary>
        /// Defines the _adminCreateServices
        /// </summary>
        private readonly IAdminCreateServices _adminCreateServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTestModel"/> class.
        /// </summary>
        /// <param name="adminCreateServices">The adminCreateServices<see cref="IAdminCreateServices"/></param>
        public CreateTestModel(IAdminCreateServices adminCreateServices)
        {
            _adminCreateServices = adminCreateServices;
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
            /// Defines the NameMinLength
            /// </summary>
            private const int NameMinLength = 3;

            /// <summary>
            /// Defines the NameMaxLength
            /// </summary>
            private const int NameMaxLength = 30;

            /// <summary>
            /// Defines the NameErrorMsg
            /// </summary>
            private const string NameErrorMsg = "Full name must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the DescrMinLength
            /// </summary>
            private const int DescrMinLength = 3;

            /// <summary>
            /// Defines the DescrMaxLength
            /// </summary>
            private const int DescrMaxLength = 800;

            /// <summary>
            /// Defines the DescrErrorMsg
            /// </summary>
            private const string DescrErrorMsg = "Description must be must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Gets or sets the Name
            /// </summary>
            [Required]
            [StringLength(NameMaxLength, ErrorMessage = NameErrorMsg, MinimumLength = NameMinLength)]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the Description
            /// </summary>
            [Required]
            [StringLength(DescrMaxLength, ErrorMessage = DescrErrorMsg, MinimumLength = DescrMinLength)]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }
        }

        /// <summary>
        /// The OnGet
        /// </summary>
        /// <param name="returnUrl">The returnUrl<see cref="string"/></param>
        /// <returns>The <see cref="IActionResult"/></returns>
        public IActionResult OnGet(string returnUrl = null)
        {

            if (!User.IsInRole("Admin"))
            {
                return Redirect("~/");
            }

            ReturnUrl = returnUrl;
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
            var dto = new TestDto
            {
                Name = Input.Name,
                Description = Input.Description
            };

            var result = await this._adminCreateServices.CreateTestAsync(dto);
            if (result > 0)
            {
                return LocalRedirect("/Admin/Account/AdminProfile");

            }
            if (result == -1)
            {
                return NotFound($"Unable to create test with name '{dto.Name}', a test with this name is already created.");
            }
            else return Page();
        }
    }
}
