namespace PROJECT_POSE.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;

    /// <summary>
    /// Defines the <see cref="ManageNavPages" />
    /// </summary>
    public static class ManageNavPages
    {
        /// <summary>
        /// Gets the UpdateDoctorProfile
        /// </summary>
        public static string UpdateDoctorProfile => "UpdateDoctorProfile";

        /// <summary>
        /// Gets the UpdateDrugStoreProfile
        /// </summary>
        public static string UpdateDrugStoreProfile => "UpdateDrugStoreProfile";

        /// <summary>
        /// Gets the ChangePassword
        /// </summary>
        public static string ChangePassword => "ChangePassword";

        /// <summary>
        /// Gets the ExternalLogins
        /// </summary>
        public static string ExternalLogins => "ExternalLogins";

        /// <summary>
        /// Gets the PersonalData
        /// </summary>
        public static string PersonalData => "PersonalData";

        /// <summary>
        /// Gets the TwoFactorAuthentication
        /// </summary>
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        /// <summary>
        /// The IndexNavClassDoctor
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string IndexNavClassDoctor(ViewContext viewContext) => PageNavClass(viewContext, UpdateDrugStoreProfile);

        /// <summary>
        /// The IndexNavClassDrugStore
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string IndexNavClassDrugStore(ViewContext viewContext) => PageNavClass(viewContext, UpdateDoctorProfile);

        /// <summary>
        /// The ChangePasswordNavClass
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        /// <summary>
        /// The ExternalLoginsNavClass
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        /// <summary>
        /// The PersonalDataNavClass
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        /// <summary>
        /// The TwoFactorAuthenticationNavClass
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        /// <summary>
        /// The PageNavClass
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/></param>
        /// <param name="page">The page<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
