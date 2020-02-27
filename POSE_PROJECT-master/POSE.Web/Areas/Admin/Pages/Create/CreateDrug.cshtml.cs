namespace POSE.Web.Areas.Admin.Pages.Create
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using POSE.Services;
    using POSE.Services.Dtos;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CreateDrugModel" />
    /// </summary>
    public class CreateDrugModel : PageModel
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
        /// Initializes a new instance of the <see cref="CreateDrugModel"/> class.
        /// </summary>
        /// <param name="adminCreateServices">The adminCreateServices<see cref="IAdminCreateServices"/></param>
        /// <param name="ingredientsServices">The ingredientsServices<see cref="InterfaceIngredientsServices"/></param>
        public CreateDrugModel(IAdminCreateServices adminCreateServices, InterfaceIngredientsServices ingredientsServices)
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
        /// Gets or sets the DrugIngredients
        /// </summary>
        [BindProperty]
        public List<string> DrugIngredients { get; set; }

        /// <summary>
        /// Gets or sets the DrugType
        /// </summary>
        [Required]
        [BindProperty]
        public string DrugType { get; set; }

        /// <summary>
        /// Gets or sets the DrugForm
        /// </summary>
        [Required]
        [BindProperty]
        public string DrugForm { get; set; }

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
            /// Defines the GenericMinLength
            /// </summary>
            private const int GenericMinLength = 3;

            /// <summary>
            /// Defines the GenericthMaxLength
            /// </summary>
            private const int GenericthMaxLength = 30;

            /// <summary>
            /// Defines the GenericMinLengthErrorMsg
            /// </summary>
            private const string GenericMinLengthErrorMsg = "Generic group name must at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the ProducerMinLength
            /// </summary>
            private const int ProducerMinLength = 3;

            /// <summary>
            /// Defines the ProducerMaxLength
            /// </summary>
            private const int ProducerMaxLength = 30;

            /// <summary>
            /// Defines the ProducerMinLengthErrorMsg
            /// </summary>
            private const string ProducerMinLengthErrorMsg = "Producer name must be at least {2} and at max {1} characters long.";

            /// <summary>
            /// Defines the LeaflatMinLength
            /// </summary>
            private const int LeaflatMinLength = 3;

            /// <summary>
            /// Defines the LeaflatMaxLength
            /// </summary>
            private const int LeaflatMaxLength = 800;

            /// <summary>
            /// Defines the LeaflatErrorMsg
            /// </summary>
            private const string LeaflatErrorMsg = "Leaflat must  at least {2} and at max {1} characters long.";

            /// <summary>
            /// Gets or sets the Name
            /// </summary>
            [Required]
            [StringLength(NameMaxLength, ErrorMessage = NameErrorMsg, MinimumLength = NameMinLength)]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the GenericGroup
            /// </summary>
            [Required]
            [StringLength(GenericthMaxLength, ErrorMessage = GenericMinLengthErrorMsg, MinimumLength = GenericMinLength)]
            public string GenericGroup { get; set; }

            /// <summary>
            /// Gets or sets the Producer
            /// </summary>
            [Required]
            [StringLength(ProducerMaxLength, ErrorMessage = ProducerMinLengthErrorMsg, MinimumLength = ProducerMinLength)]
            public string Producer { get; set; }

            /// <summary>
            /// Gets or sets the Dose
            /// </summary>
            [Required]
            public decimal Dose { get; set; }

            /// <summary>
            /// Gets or sets the DosesPerDay
            /// </summary>
            [Required]
            public int DosesPerDay { get; set; }

            /// <summary>
            /// Gets or sets the Price
            /// </summary>
            [Required]
            [DataType(DataType.Currency)]
            public decimal Price { get; set; }

            /// <summary>
            /// Gets or sets the Leaflat
            /// </summary>
            [Required]
            [StringLength(LeaflatMaxLength, ErrorMessage = LeaflatErrorMsg, MinimumLength = LeaflatMinLength)]
            [DataType(DataType.MultilineText)]
            public string Leaflat { get; set; }
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
            ViewData["DrugTypes"] = Enum.GetNames(typeof(DrugType)).ToList<string>();
            ViewData["DrugForms"] = Enum.GetNames(typeof(DrugForm)).ToList<string>();
            ViewData["DrugIngredients"] = _ingredientsServices.IngredientNames();
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
            var dto = new DrugDto
            {
                Dose = Input.Dose,
                DosesPerDay = Input.DosesPerDay,
                Form = (DrugForm)Enum.Parse(typeof(DrugForm), DrugForm, true),
                Type = (DrugType)Enum.Parse(typeof(DrugType), DrugType, true),
                GenericGroup = Input.GenericGroup,
                Ingredients = DrugIngredients,
                Leaflat = Input.Leaflat,
                Name = Input.Name,
                Price = Input.Price,
                Producer = Input.Producer
            };
            var result = await this._adminCreateServices.CreateDrugAsync(dto);
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
