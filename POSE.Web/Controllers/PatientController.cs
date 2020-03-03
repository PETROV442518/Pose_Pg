namespace POSE.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using POSE.Domain;
    using POSE.Services;
    using PROJECT_POSE.Models.Account;
    using PROJECT_POSE.Models.Patient;
    using PROJECT_POSE.Models.Receipt;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using IEmailSender = POSE.Services.IEmailSender;

    /// <summary>
    /// Defines the <see cref="PatientController" />
    /// </summary>
    public class PatientController : Controller
    {
        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Defines the _accountServices
        /// </summary>
        private readonly IAccountServices _accountServices;

        /// <summary>
        /// Defines the _ingredientsServices
        /// </summary>
        private readonly InterfaceIngredientsServices _ingredientsServices;

        /// <summary>
        /// Defines the _receiptServices
        /// </summary>
        private readonly IReceiptServices _receiptServices;

        /// <summary>
        /// Defines the _emailSender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Defines the NotSelectedMessage
        /// </summary>
        private const string NotSelectedMessage = "Not selected";

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        /// <param name="ingredientsServices">The ingredientsServices<see cref="InterfaceIngredientsServices"/></param>
        /// <param name="receiptServices">The receiptServices<see cref="IReceiptServices"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        public PatientController(
            UserManager<PoseUser> userManager,
            IAccountServices accountServices,
            InterfaceIngredientsServices ingredientsServices,
            IReceiptServices receiptServices,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _accountServices = accountServices;
            _ingredientsServices = ingredientsServices;
            _receiptServices = receiptServices;
            _emailSender = emailSender;
        }

        /// <summary>
        /// The PatientProfile
        /// </summary>
        /// <returns>The <see cref="Task{ActionResult}"/></returns>
        [Authorize]
        public async Task<ActionResult> PatientProfile()
        {
            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            var user = await _userManager.GetUserAsync(User);

            var userPatient = _accountServices.ReturnPatientDto(user.UserGuid);

            var outputPatient = new PatientProfileViewModel
            {
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Age = userPatient.Age,
                FeeForMedicaments = userPatient.SumForDrugs
            };
            //checks if doctor is selected
            if (userPatient.DoctorId != null)
            {
                var doctor = userPatient.Doctor;
                outputPatient.DoctorName = "Dr. " + doctor.FullName;
                outputPatient.HasADoctor = true;
            }
            else
            {
                outputPatient.DoctorName = NotSelectedMessage;
            }
            //checks if drugstore is selected
            if (userPatient.PreferedDrugStoreId != null)
            {
                var drugStore = userPatient.PreferedDrugStore;
                outputPatient.DrugStoreName = drugStore.FullName;
                outputPatient.DrugStoreAddress = drugStore.Address;
                outputPatient.HasAStore = true;
            }
            else
            {
                outputPatient.DrugStoreName = NotSelectedMessage;
            }
            //check if patient has alergens is selected
            if (userPatient.AllergicTo != null)
            {
                var allergens = userPatient.AllergicTo.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

                outputPatient.AlergicToString = string.Join(", ", allergens);

            }
            else
            {
                outputPatient.AlergicToString = NotSelectedMessage;
            }
            return View(outputPatient);
        }

        /// <summary>
        /// The GiveDoctorScore
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        [Authorize]
        public IActionResult GiveDoctorScore()
        {
            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            return View();
        }

        /// <summary>
        /// The GiveDoctorScore
        /// </summary>
        /// <param name="model">The model<see cref="GiveScoreBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GiveDoctorScore(GiveScoreBindingModel model)
        {
            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("GiveDoctorScore");
            }
            var user = await _userManager.GetUserAsync(User);
            var patient = this._accountServices.ReturnPatient(user.UserGuid);
            patient.DoctorScore = model.Score;
            await _userManager.UpdateAsync(patient);

            return Redirect("/Patient/PatientProfile");
        }

        /// <summary>
        /// The GiveDrugStoreScore
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        [Authorize]
        public IActionResult GiveDrugStoreScore()
        {
            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            return View();
        }

        /// <summary>
        /// The GiveDrugStoreScore
        /// </summary>
        /// <param name="model">The model<see cref="GiveScoreBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GiveDrugStoreScore(GiveScoreBindingModel model)
        {
            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("GiveDrugStoreScore");
            }
            var user = await _userManager.GetUserAsync(User);
            var patient = this._accountServices.ReturnPatient(user.UserGuid);
            patient.StoreScore = model.Score;
            await _userManager.UpdateAsync(patient);

            return Redirect("/Patient/PatientProfile");
        }

        /// <summary>
        /// The PatientDetails
        /// </summary>
        /// <param name="Id">The Id<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> PatientDetails(string Id)
        {
            if (User.IsInRole("Doctor"))
            {
                var patient = this._accountServices.ReturnPatientDto(Id);
                var model = new PatientDetailsViewModel
                {
                    Patient = patient
                };
                return View(model);
            }
            else if (User.IsInRole("Patient"))
            {
                var user = await this._userManager.FindByIdAsync(Id);
                var patient = this._accountServices.ReturnPatientDto(user.UserGuid);

                var model = new PatientDetailsViewModel
                {
                    Patient = patient

                };

                return View(model);
            }
            else
            {
                return this.Redirect("~/");
            }
        }

        /// <summary>
        /// The Receipts
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> Receipts()
        {
            if (!User.IsInRole("Patient"))
            {
                return this.Redirect("~/");
            }
            var model = new AllReceiptsViewModel
            {
                Receipts = await this._receiptServices.ReturnReceiptsByUser(_userManager.GetUserId(User))
            };
            return View(model);
        }

        /// <summary>
        /// The ContactDoctor
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> ContactDoctor()
        {
            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            var user = await _userManager.GetUserAsync(User);
            return View();
        }

        /// <summary>
        /// The ContactDoctor
        /// </summary>
        /// <param name="model">The model<see cref="ContactBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ContactDoctor(ContactBindingModel model)
        {
            if (!User.IsInRole("Patient") || !ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.GetUserAsync(User);
            var userDto = this._accountServices.ReturnPatientDto(user.UserGuid);
            if (model.AntiSpam == null)
            {
                var subject = model.Subject.ToString();
                var message = $"From: {userDto.FullName}" + Environment.NewLine + model.Message.ToString();
                await _emailSender.SendEmailAsync(userDto.Doctor.Email, subject, message);
                return Redirect("~/");
            }
            return View();
        }

        /// <summary>
        /// The ContactStore
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> ContactStore()
        {

            if (!User.IsInRole("Patient"))
            {
                return Redirect("~/");
            }
            var user = await _userManager.GetUserAsync(User);
            return View();
        }

        /// <summary>
        /// The ContactStore
        /// </summary>
        /// <param name="model">The model<see cref="ContactBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ContactStore(ContactBindingModel model)
        {
            if (!User.IsInRole("Patient") || !ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.GetUserAsync(User);
            var userDto = this._accountServices.ReturnPatientDto(user.UserGuid);
            if (model.AntiSpam == null)
            {
                var subject = model.Subject.ToString();
                var message = $"From: {userDto.FullName}" + Environment.NewLine + model.Message.ToString();
                await _emailSender.SendEmailAsync(userDto.PreferedDrugStore.Email, subject, message);
                return Redirect("~/");
            }
            return View();
        }
    }
}
