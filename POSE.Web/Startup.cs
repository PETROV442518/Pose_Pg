namespace POSE.Web
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Domain.Settings;
    using POSE.Services;
    using PROJECT_POSE.Hubs;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<POSEDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<PoseUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<POSEDbContext>()
               .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<Services.IEmailSender, EmailSender>();


            services.AddScoped<InterfaceIngredientsServices, IngredientsServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IExaminationServices, ExaminationServices>();
            services.AddScoped<IPrescriptionServices, PrescriptionServices>();
            services.AddScoped<IAdminCreateServices, AdminCreateServices>();
            services.AddScoped<IReceiptServices, ReceptServices>();

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="app">The app<see cref="IApplicationBuilder"/></param>
        /// <param name="env">The env<see cref="IHostingEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("QA"))
            {
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions() { SourceCodeLineCount = 100 });
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<POSEDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new IdentityRole { Name = "Patient", NormalizedName = "PATIENT" });
                        context.Roles.Add(new IdentityRole { Name = "Doctor", NormalizedName = "DOCTOR" });
                        context.Roles.Add(new IdentityRole { Name = "DrugStore", NormalizedName = "DRUGSTORE" });
                        context.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                    }
                    if (!context.DrugIngredients.Any())
                    {
                        context.DrugIngredients.Add(new DrugIngredient { Name = "None", Description = "Alergic to nothing" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Magnesium", Description = "Magnesium is a naturally occurring mineral, and is essential for the diet." });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Paracetamol", Description = "Alergic to drugs, containing Paracetamol" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Calcius", Description = "Natural occured calcius" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Menthol", Description = "Menthol from the nature" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Natrium", Description = "Natural occured natrium" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Hidrogen", Description = "Natural occured hidrogen" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Vitamin C", Description = "Vitamin C" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Vitamin B", Description = "compex Vitamin B" });
                        context.DrugIngredients.Add(new DrugIngredient { Name = "Vitamin A", Description = "compex Vitamin A" });
                    }
                    if (!context.Tests.Any())
                    {
                        context.Tests.Add(new Test { Description = "Makes blood tests", Name = "Blood Tests" });
                        context.Tests.Add(new Test { Description = "Makes Scanner", Name = "Scanner" });
                        context.Tests.Add(new Test { Description = "Makes Rentgen", Name = "Rentgen" });
                        context.Tests.Add(new Test { Description = "Makes bronchoschopy ", Name = "bronchoschopy" });
                    }
                    if (!context.Diseases.Any())
                    {
                        context.Diseases.Add(new Disease
                        {
                            Name = "Caribean Flu",
                            Contagious = true,
                            Curable = true,
                            Description = "Ordinary caribean Flu",
                            Lethal = false,
                            Treatment = "Something for the fever and a few days of a rest"
                        });
                        context.Diseases.Add(new Disease
                        {
                            Name = "Cold",
                            Contagious = true,
                            Curable = true,
                            Description = "Ordinary cold",
                            Lethal = false,
                            Treatment = "Hot tea and a few days of a rest"
                        });
                    }
                    if (!context.Drugs.Any())
                    {
                        context.Drugs.Add(new Drug
                        {
                            Dose = 1,
                            DosesPerDay = 1,
                            Form = DrugForm.Pill,
                            GenericGroup = "Vitamins",
                            Leaflat = "Leaflat",
                            Name = "Vitamin C",
                            Price = 3.20M,
                            Producer = "Actavis",
                            Type = DrugType.Vitamins,
                            Ingredients = new List<string>
                            {
                                "Vitamin C",
                                "Hidrogen"
                            }
                        });
                        context.Drugs.Add(new Drug
                        {
                            Dose = 1,
                            DosesPerDay = 1,
                            Form = DrugForm.Pill,
                            GenericGroup = "Vitamins",
                            Leaflat = "Leaflat",
                            Name = "Vitamin B",
                            Price = 3.20M,
                            Producer = "Actavis",
                            Type = DrugType.Vitamins,
                            Ingredients = new List<string>
                            {
                                "Vitamin B",
                                "Hidrogen"
                            }
                        });
                    }
                    context.SaveChanges();
                }
            }


            app.UseHttpsRedirection();
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
                app.UseStaticFiles(new StaticFileOptions
                { FileProvider = new PhysicalFileProvider(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "wwwroot")), RequestPath = "" });
            else
                app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

        }

        /// <summary>
        /// The Sha256
        /// </summary>
        /// <param name="rawData">The rawData<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string Sha256(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
