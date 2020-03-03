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

            if (!_userManager.Users.Where(a => a.Role == UserRole.Doctor).Any())
            {
                var doctor = new Doctor
                {
                    UserName = "doctor1",
                    Email = "doc1@asdasd.com",
                    EmailConfirmed = true,
                    FullName = "Doctor Dolittle",
                    Age = 43,
                    Address = "Sofia, 1000, Pirogov",
                    UDN = "1234567890",
                    PhoneNumber = "1231231234",
                    Specialty = Specialty.GP,
                    Role = UserRole.Doctor
                };
                await _userManager.CreateAsync(doctor, "Doctor1Password");

                await _userManager.AddToRoleAsync(doctor, "Doctor");
            };
            if (!_userManager.Users.Where(a => a.Role == UserRole.Patient).Any())
            {
                var patient = new Patient
                {
                    UserName = "patient1",
                    Email = "pat1@asdasd.com",
                    EmailConfirmed = true,
                    FullName = "Ivan Ivanov",
                    Age = 34,
                    Address = "Sofia, 1000, Blok 33",
                    PIN = "1111111111",
                    PhoneNumber = "0878212121",
                    Role = UserRole.Patient
                };
                await _userManager.CreateAsync(patient, "Patient1Password");

                await _userManager.AddToRoleAsync(patient, "Patient");
            };
            if (!_userManager.Users.Where(a => a.Role == UserRole.DrugStore).Any())
            {
                var store = new DrugStore
                {
                    UserName = "drugStore1",
                    Email = "store1@asdasd.com",
                    EmailConfirmed = true,
                    FullName = "Apteka Mareshki 23",
                    Address = "Sofia, 1000, Bolnichna apteka Pirogov",
                    PhoneNumber = "0878212121",
                    Role = UserRole.DrugStore,
                    CIN = "2000000000",
                };
                await _userManager.CreateAsync(store, "Store1Password");

                await _userManager.AddToRoleAsync(store, "DrugStore");
            }
        }
    }
}
