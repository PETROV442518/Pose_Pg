namespace POSE.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using POSE.Domain;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="POSEDbContext" />
    /// </summary>
    public class POSEDbContext : IdentityDbContext<PoseUser, IdentityRole, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="POSEDbContext"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{POSEDbContext}"/></param>
        public POSEDbContext(DbContextOptions<POSEDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="POSEDbContext"/> class.
        /// </summary>
        protected POSEDbContext()
        {
        }

        /// <summary>
        /// Gets or sets the PoseUsers
        /// </summary>
        public DbSet<PoseUser> PoseUsers { get; set; }

        /// <summary>
        /// Gets or sets the Doctors
        /// </summary>
        public DbSet<Doctor> Doctors { get; set; }

        /// <summary>
        /// Gets or sets the Patients
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// Gets or sets the DrugStores
        /// </summary>
        public DbSet<DrugStore> DrugStores { get; set; }

        /// <summary>
        /// Gets or sets the Diagnoses
        /// </summary>
        public DbSet<Diagnosis> Diagnoses { get; set; }

        /// <summary>
        /// Gets or sets the Diseases
        /// </summary>
        public DbSet<Disease> Diseases { get; set; }

        /// <summary>
        /// Gets or sets the Drugs
        /// </summary>
        public DbSet<Drug> Drugs { get; set; }

        /// <summary>
        /// Gets or sets the DrugIngredients
        /// </summary>
        public DbSet<DrugIngredient> DrugIngredients { get; set; }

        /// <summary>
        /// Gets or sets the Examinations
        /// </summary>
        public DbSet<Examination> Examinations { get; set; }

        /// <summary>
        /// Gets or sets the Prescriptions
        /// </summary>
        public DbSet<Prescription> Prescriptions { get; set; }

        /// <summary>
        /// Gets or sets the Receipts
        /// </summary>
        public DbSet<Receipt> Receipts { get; set; }

        /// <summary>
        /// Gets or sets the Tests
        /// </summary>
        public DbSet<Test> Tests { get; set; }

        /// <summary>
        /// Gets or sets the TestResults
        /// </summary>
        public DbSet<TestResult> TestResults { get; set; }

        /// <summary>
        /// The OnModelCreating
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Doctor
            builder.Entity<Doctor>().HasMany(a => a.Diagnoses).WithOne(a => a.Doctor).HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Doctor>().HasMany(a => a.Examinations).WithOne(a => a.Doctor).HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Doctor>().HasMany(a => a.Patients).WithOne(a => a.Doctor).HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Doctor>().HasMany(a => a.Prescriptions).WithOne(a => a.Doctor).HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            //Patient
            builder.Entity<Patient>().HasMany(a => a.Diagnoses).WithOne(a => a.Patient).HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Patient>().HasMany(a => a.Examinations).WithOne(a => a.Patient).HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Patient>().HasMany(a => a.Prescriptions).WithOne(a => a.Patient).HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Patient>().HasMany(a => a.Receipts).WithOne(a => a.Client).HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Patient>().HasMany(a => a.TestResults).WithOne(a => a.Patient).HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            //DrugStore
            builder.Entity<DrugStore>().HasMany(a => a.Clients).WithOne(a => a.PreferedDrugStore)
                .HasForeignKey(a => a.PreferedDrugStoreId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<DrugStore>().HasMany(a => a.Prescriptions).WithOne(a => a.DrugStore)
                .HasForeignKey(a => a.DrugStoreId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<DrugStore>().HasMany(a => a.Receipts).WithOne(a => a.DrugStore)
                .HasForeignKey(a => a.DrugStoreId).OnDelete(DeleteBehavior.Restrict);

            //Diagnosis
            builder.Entity<Diagnosis>().HasOne(a => a.Prescription).WithOne(a => a.Diagnosis)
                .HasForeignKey<Diagnosis>(b => b.PrescriptionId);

            //builder.Entity<Drug>().HasOne(a => a.PrescriptionIds);
            //builder.Entity<Prescription>().HasOne(a => a.DrugIds);
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// The SaveChanges
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <summary>
        /// The SaveChangesAsync
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
