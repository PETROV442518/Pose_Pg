namespace POSE.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using POSE.Domain;
    using POSE.Services;
    using PROJECT_POSE.Models.Receipt;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ReceiptController" />
    /// </summary>
    public class ReceiptController : Controller
    {
        /// <summary>
        /// Defines the _prescriptionServices
        /// </summary>
        private readonly IPrescriptionServices _prescriptionServices;

        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptController"/> class.
        /// </summary>
        /// <param name="prescriptionServices">The prescriptionServices<see cref="IPrescriptionServices"/></param>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        public ReceiptController(IPrescriptionServices prescriptionServices, UserManager<PoseUser> userManager)
        {
            this._prescriptionServices = prescriptionServices;
            this._userManager = userManager;
        }

        /// <summary>
        /// The Create
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> Create()
        {
            if (!User.IsInRole("DrugStore"))
            {
                return this.Redirect("/");
            }
            var userId = this._userManager.GetUserId(User);
            var prescriptions = await this._prescriptionServices.ReturnPrescriptionsByStoreId(userId);
            ViewData["Prescriptions"] = prescriptions;
            return View();
        }

        /// <summary>
        /// The ReceiptDetails
        /// </summary>
        /// <param name="model">The model<see cref="CreateReceiptBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReceiptDetails(CreateReceiptBindingModel model)
        {
            if (!User.IsInRole("DrugStore"))
            {
                return this.Redirect("/");
            }
            //Prescription
            var prescription = await this._prescriptionServices.GetPrescriptionById(model.PrescriptionId);
            //Receipt
            var receipt = await this._prescriptionServices.CreateReceipt(prescription);
            //Output Receipt Details
            var store = this._userManager.GetUserAsync(User);
            var outputModel = new ReceiptDetailsViewModel
            {
                Drugs = receipt.Drugs.ToList(),
                DrugStoreDto = receipt.DrugStore,
                Fee = receipt.Fee,
                IssuedOn = receipt.IssuedOn,
                ReceiptId = receipt.Id,
            };
            return View(outputModel);
        }
    }
}
