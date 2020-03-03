namespace POSE.Web.Areas.Admin.Pages.Delete
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DeleteDrugModel" />
    /// </summary>
    public class DeleteDrugModel : PageModel
    {
        /// <summary>
        /// Defines the _adminCreateServices
        /// </summary>
        private readonly IAdminCreateServices _adminCreateServices;

        /// <summary>
        /// Defines the _examinationServices
        /// </summary>
        private readonly IExaminationServices _examinationServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDrugModel"/> class.
        /// </summary>
        /// <param name="adminCreateServices">The adminCreateServices<see cref="IAdminCreateServices"/></param>
        /// <param name="examinationServices">The examinationServices<see cref="IExaminationServices"/></param>
        public DeleteDrugModel(IAdminCreateServices adminCreateServices, IExaminationServices examinationServices)
        {
            _adminCreateServices = adminCreateServices;
            _examinationServices = examinationServices;
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
        /// Gets or sets the DrugName
        /// </summary>
        [BindProperty]
        public string DrugName { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets the DrugName
            /// </summary>
            public string DrugName { get; set; }
        }

        /// <summary>
        /// The OnGet
        /// </summary>
        /// <param name="returnUrl">The returnUrl<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            if (!User.IsInRole("Admin"))
            {
                return Redirect("~/");
            }
            var drugNames = await this._examinationServices.ReturnAllDrugs();
            ViewData["DrugNames"] = drugNames;
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
            var result = await this._adminCreateServices.DeleteDrugAsync(Input.DrugName);
            if (result > 0)
            {
                return LocalRedirect("/Admin/Account/AdminProfile");
            }
            if (result == -1)
            {
                return NotFound($"Unable find drug with name '{Input.DrugName}'.");
            }
            else return Page();
        }
    }
}
