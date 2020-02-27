namespace POSE.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="RegisterMainModel" />
    /// </summary>
    public class RegisterMainModel : PageModel
    {
        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterMainModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        public RegisterMainModel(UserManager<PoseUser> userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// The OnGet
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task OnGet()
        {
            if (!_userManager.Users.Where(a => a.Role == UserRole.Admin).Any())
            {
                var userAdmin = new PoseUser
                {
                    Address = "Sofia 1000, adresa na firmata",
                    Email = "admin11@abv.bg",
                    FullName = "POSE Admin",
                    PhoneNumber = "029311125",
                    IsDeleted = false,
                    UserName = "Admin11",
                    Role = UserRole.Admin,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(userAdmin, "Admin11Password");

                await _userManager.AddToRoleAsync(userAdmin, "Admin");
            }
        }
    }
}
