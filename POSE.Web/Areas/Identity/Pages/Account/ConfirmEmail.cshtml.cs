namespace PROJECT_POSE.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using POSE.Domain;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ConfirmEmailModel" />
    /// </summary>
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        /// <summary>
        /// Defines the _userManager
        /// </summary>
        private readonly UserManager<PoseUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmEmailModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{PoseUser}"/></param>
        public ConfirmEmailModel(UserManager<PoseUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// The OnGetAsync
        /// </summary>
        /// <param name="userId">The userId<see cref="string"/></param>
        /// <param name="code">The code<see cref="string"/></param>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Redirect("~/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user.IsDeleted == true)
            { user = null; }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Page();
        }
    }
}
