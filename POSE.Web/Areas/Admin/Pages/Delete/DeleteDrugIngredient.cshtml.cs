namespace POSE.Web.Areas.Admin.Pages.Delete
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Services;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DeleteDrugIngredientModel" />
    /// </summary>
    public class DeleteDrugIngredientModel : PageModel
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
        /// Initializes a new instance of the <see cref="DeleteDrugIngredientModel"/> class.
        /// </summary>
        /// <param name="adminCreateServices">The adminCreateServices<see cref="IAdminCreateServices"/></param>
        /// <param name="ingredientsServices">The ingredientsServices<see cref="InterfaceIngredientsServices"/></param>
        public DeleteDrugIngredientModel(IAdminCreateServices adminCreateServices, InterfaceIngredientsServices ingredientsServices)
        {
            _adminCreateServices = adminCreateServices;
            _ingredientsServices = ingredientsServices;
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
            /// Gets or sets the Name
            /// </summary>
            [Required]
            public string Name { get; set; }
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
            ViewData["DrugIngredients"] = this._ingredientsServices.IngredientNames();
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
            var result = await this._adminCreateServices.DeleteIngredientAsync(Input.Name);
            if (result > 0)
            {
                return LocalRedirect("/Admin/Account/AdminProfile");
            }
            if (result == -1)
            {
                return NotFound($"Unable to delete drug ingredient with name '{Input.Name}', an ingredient with this name does not exist.");
            }
            else return Page();
        }
    }
}
