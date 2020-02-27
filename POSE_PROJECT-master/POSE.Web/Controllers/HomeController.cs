namespace POSE.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using POSE.Web.Models;
    using PROJECT_POSE.Models.Messages;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using IEmailSender = POSE.Services.IEmailSender;

    /// <summary>
    /// Defines the <see cref="HomeController" />
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Defines the _emailSender
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="emailSender">The emailSender<see cref="IEmailSender"/></param>
        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        /// <summary>
        /// The IndexSlash
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        public IActionResult IndexSlash()
        {
            return Index();
        }

        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Privacy
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// The Error
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// The SendMessageAsync
        /// </summary>
        /// <param name="model">The model<see cref="SendMessageBindingModel"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> SendMessageAsync(SendMessageBindingModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync("tsvetanpetrov20@gmail.com", model.Subject, model.Message);
                return Redirect("~/");
            }
            return PartialView("_ContactsPartial");
        }

        /// <summary>
        /// The Chat
        /// </summary>
        /// <returns>The <see cref="IActionResult"/></returns>
        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }
    }
}
