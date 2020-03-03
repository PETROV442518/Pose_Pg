namespace POSE.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using POSE.Domain;
    using POSE.Services;
    using PROJECT_POSE.Models.Examination;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ExaminationController" />
    /// </summary>
    public class ExaminationController : Controller
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
        /// Defines the _examinationServices
        /// </summary>
        private readonly IExaminationServices _examinationServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExaminationController"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        /// <param name="examinationServices">The examinationServices<see cref="IExaminationServices"/></param>
        public ExaminationController(UserManager<PoseUser> userManager,
            IAccountServices accountServices,
            IExaminationServices examinationServices)
        {
            _userManager = userManager;
            _accountServices = accountServices;
            _examinationServices = examinationServices;
        }

        /// <summary>
        /// The ChoosePatientAndTests
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChoosePatientAndTests()
        {
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("~/");
            }
            var doctor = await this._userManager.GetUserAsync(User);

            var patients = this._examinationServices.ReturnAllPatientsByDoctor(doctor.UserGuid);
            ViewData["Patients"] = patients;

            var tests = this._examinationServices.ReturnAllTestsNames();
            ViewData["Tests"] = tests;

            return View();
        }

        /// <summary>
        /// The PerformExamination
        /// </summary>
        /// <param name="model">The model<see cref="PerformTestsBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> PerformExamination(PerformTestsBindingModel model)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("~/");
            }
            //Patients
            if (model.Patient == null)
            {
                return Redirect("/Examination/ChoosePatientAndTests");
            }
            var patient = this._accountServices.ReturnPatientDto(model.Patient);
            ViewData["Patient"] = patient;
            ViewData["DrugStores"] = this._accountServices.ReturnAllDrugStoreDtos();
            //TestResults
            var testsResults = await this._examinationServices.ReturnTestResultDtosByTestName(model.Tests, patient.UserGuid);
            ViewData["TestResults"] = testsResults;
            //Diseases
            var diseases = await this._examinationServices.ReturnAllDiseases();
            ViewData["Diseases"] = diseases;
            //Drugs
            var drugs = await this._examinationServices.ReturnAllDrugsForPatient(model.Patient);
            ViewData["Drugs"] = drugs;
            //DrugStore
            if (patient.PreferedDrugStoreId != null)
            {
                ViewData["DrugStore"] = this._accountServices.ReturnDrugStore(patient.PreferedDrugStore.UserGuid);
            }

            ViewBag.ReturnUrl = this.HttpContext.Request.GetDisplayUrl();
            return View();
        }

        /// <summary>
        /// The FinishExamination
        /// </summary>
        /// <param name="model">The model<see cref="PerformExaminationBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FinishExamination(PerformExaminationBindingModel model)
        {
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("~/");
            }
            if (!ModelState.IsValid)
            {
                var returnUrl = model.ReturnUrl;
                return Redirect(returnUrl);
            }
            var doctor = await this._userManager.GetUserAsync(HttpContext.User);
            var patientGuid = model.UserGuid;
            var testResultIds = model.TestResults;
            int treatmentDuration = model.TreatmentDuration;
            var description = model.Description.ToString();
            var drugIds = model.Drugs;
            var diseaseNames = model.Diseases;
            var drugStoreGuid = model.StoreId;

            var result = await this._examinationServices.PerformExaminationAsync(patientGuid, testResultIds,
                treatmentDuration, description, drugIds, diseaseNames, doctor.Id, drugStoreGuid);
            return Redirect("/Doctor/DoctorProfile");
        }
    }
}
