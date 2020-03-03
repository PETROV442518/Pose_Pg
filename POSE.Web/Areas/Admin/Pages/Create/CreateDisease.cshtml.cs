namespace POSE.Web.Areas.Admin.Pages.Create
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Services;
    using POSE.Services.Dtos;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CreateDiseaseModel" />
    /// </summary>
    public class CreateDiseaseModel : PageModel
    {
        /// <summary>
        /// Defines the _adminCreateServices
        /// </summary>
        private readonly IAdminCreateServices _adminCreateServices;

        /// <summary>
        /// Defines the _ingredientsServices
        /// </summary>
        private readonly InterfaceIngredientsServices _ingredientsServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDiseaseModel"/> class.
        /// </summary>
        /// <param name="adminCreateServices">The adminCreateServices<see cref="IAdminCreateServices"/></param>
        /// <param name="ingredientsServices">The ingredientsServices<see cref="InterfaceIngredientsServices"/></param>
        public CreateDiseaseModel(IAdminCreateServices adminCreateServices, InterfaceIngredientsServices ingredientsServices)
        {
            _adminCreateServices = adminCreateServices;
            this._ingredientsServices = ingredientsServices;
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
            private const string NameErrorMsg = "Full name must be must be at least {2} and at max {1} characters long.";

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
            /// Defines the TreatMinLength
            /// </summary>
            private const int TreatMinLength = 3;

            /// <summary>
            /// Defines the TreatMaxLength
            /// </summary>
            private const int TreatMaxLength = 800;

            /// <summary>
            /// Defines the TraatErrorMsg
            /// </summary>
            private const string TraatErrorMsg = "Treatment must be must be at least {2} and at max {1} characters long.";

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

            /// <summary>
            /// Gets or sets a value indicating whether Contagious
            /// </summary>
            [Required]
            public bool Contagious { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether Lethal
            /// </summary>
            [Required]
            public bool Lethal { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether Curable
            /// </summary>
            [Required]
            public bool Curable { get; set; }

            /// <summary>
            /// Gets or sets the Treatment
            /// </summary>
            [Required]
            [StringLength(TreatMaxLength, ErrorMessage = TraatErrorMsg, MinimumLength = TreatMinLength)]
            [DataType(DataType.MultilineText)]
            public string Treatment { get; set; }
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
            var dto = new DiseaseDto
            {
                Contagious = Input.Contagious,
                Curable = Input.Curable,
                Description = Input.Description,
                Lethal = Input.Lethal,
                Name = Input.Name,
                Treatment = Input.Treatment
            };
            var result = await this._adminCreateServices.CreateDiseaseAsync(dto);
            if (result > 0)
            {
                return LocalRedirect("/Admin/Account/AdminProfile");
            }
            if (result == -1)
            {
                return NotFound($"Unable to create drug with name '{Input.Name}', a drug with this name is already created.");
            }
            else return Page();
        }
    }
}
