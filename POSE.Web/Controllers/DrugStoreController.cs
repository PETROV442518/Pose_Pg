namespace POSE.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using POSE.Domain;
    using POSE.Services;
    using PROJECT_POSE.Models.Account;
    using PROJECT_POSE.Models.Receipt;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DrugStoreController" />
    /// </summary>
    public class DrugStoreController : Controller
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
        /// Defines the _roleManager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Defines the _accountServices
        /// </summary>
        private readonly IAccountServices _accountServices;

        /// <summary>
        /// Defines the _receiptServices
        /// </summary>
        private readonly IReceiptServices _receiptServices;

        /// <summary>
        /// Defines the NotSelectedMessage
        /// </summary>
        private const string NotSelectedMessage = "Not selected";

        /// <summary>
        /// Initializes a new instance of the <see cref="DrugStoreController"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{PoseUser}"/></param>
        /// <param name="roleManager">The roleManager<see cref="RoleManager{IdentityRole}"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        /// <param name="receiptServices">The receiptServices<see cref="IReceiptServices"/></param>
        public DrugStoreController(
            UserManager<PoseUser> userManager,
            SignInManager<PoseUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAccountServices accountServices,
            IReceiptServices receiptServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accountServices = accountServices;
            _receiptServices = receiptServices;
        }

        /// <summary>
        /// The DrugStoreProfile
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> DrugStoreProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user.Role == UserRole.DrugStore)
            {
                var userDrugStore = _accountServices.ReturnDrugStore(user.UserGuid);

                var output = new DrugStoreProfileViewModel
                {
                    Address = user.Address,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    PrescriptionsForToday = userDrugStore.Receipts.Where(a => a.IssuedOn.DayOfYear == DateTime.Now.DayOfYear).Count()
                };
                return View(output);
            }
            else
            {
                return this.Redirect("/");
            }
        }

        /// <summary>
        /// The Receipts
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> Receipts()
        {
            if (!User.IsInRole("DrugStore"))
            {
                return this.Redirect("~/");
            }
            var model = new AllReceiptsViewModel
            {
                Receipts = await this._receiptServices.ReturnReceiptsByStore(_userManager.GetUserId(User))
            };
            return View(model);
        }
    }
}
