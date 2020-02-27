namespace POSE.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using POSE.Domain;
    using POSE.Services;
    using PROJECT_POSE.Models.Account;
    using PROJECT_POSE.Models.Doctor;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DoctorController" />
    /// </summary>
    public class DoctorController : Controller
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
        /// Defines the _emailSender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Defines the NotSelectedMessage
        /// </summary>
        private const string NotSelectedMessage = "Not selected";

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorController"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        /// <param name="accountServices">The accountServices<see cref="IAccountServices"/></param>
        /// <param name="examinationServices">The examinationServices<see cref="IExaminationServices"/></param>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        public DoctorController(
            UserManager<PoseUser> userManager,
            IAccountServices accountServices,
            IExaminationServices examinationServices,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _accountServices = accountServices;
            _examinationServices = examinationServices;
            _emailSender = emailSender;
        }

        /// <summary>
        /// The CheckPatientHistory
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> CheckPatientHistory()
        {
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("/");
            }
            var user = await _userManager.GetUserAsync(User);
            var patients = this._examinationServices.ReturnAllPatientsByDoctor(user.UserGuid);
            ViewData["Patients"] = patients;
            return View();
        }

        /// <summary>
        /// The DoctorProfile
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> DoctorProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("/");
            }

            var userDoctor = _accountServices.ReturnDoctor(user.UserGuid);
            var score = 4.00M;
            if (userDoctor.Patients.Count != 0)
            {
                score = userDoctor.Patients.Select(a => a.DoctorScore).Average();
            }
            var output = new DoctorProfileViewModel
            {
                Address = user.Address,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Age = userDoctor.Age,
                Specialty = userDoctor.Specialty.ToString(),
                PatientsCount = _accountServices.PatientsCountByDoctor(userDoctor.Id),
                Score = score,
            };
            return View(output);
        }

        /// <summary>
        /// The ContactPatient
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        public async Task<IActionResult> ContactPatient()
        {
            if (!User.IsInRole("Doctor"))
            {
                return Redirect("~/");
            }
            var user = await _userManager.GetUserAsync(User);
            var patients = this._examinationServices.ReturnAllPatientsByDoctor(user.UserGuid);
            ViewData["Patients"] = patients;
            return View();
        }

        /// <summary>
        /// The ContactPatient
        /// </summary>
        /// <param name="model">The model<see cref="ContactPatientBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ContactPatient(ContactPatientBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ContactPatient");
            }
            if (model.AntiSpam == null)
            {
                var user = await _userManager.GetUserAsync(User);
                var userDto = this._accountServices.ReturnDoctorDto(user.UserGuid);
                var patient = this._accountServices.ReturnPatientDto(model.Patient);
                var subject = model.Subject.ToString();
                var message = $"From: Dr. {userDto.FullName}" + Environment.NewLine + model.Message.ToString();
                await _emailSender.SendEmailAsync(patient.Email, subject, message);
                return Redirect("~/");
            }
            return View();
        }
    }
}
