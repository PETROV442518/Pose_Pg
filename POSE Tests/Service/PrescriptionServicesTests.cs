namespace POSE_Tests.Service
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    //Task<List<PrescriptionDto>> ReturnPrescriptionsByStoreId(string id);


    //Task<PrescriptionDto> GetPrescriptionById(string id);
    /// <summary>
    /// Defines the <see cref="PrescriptionServicesTests" />
    /// </summary>
    public class PrescriptionServicesTests
    {
        /// <summary>
        /// The TestReturnDrugsById_WithCorrectData_ShouldReturnListOfDrugsByuserId
        /// </summary>
        [Fact]
        public void TestReturnDrugsById_WithCorrectData_ShouldReturnListOfDrugsByuserId()
        {

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var options = new DbContextOptionsBuilder<POSEDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new POSEDbContext(options);
            SeedTestData(context);
            var ids = string.Join(", ", context.Drugs.Select(a => a.Id));
            var services = new PrescriptionServices(context, mapper);

            var actual = services.ReturnDrugsById(ids);
            var expected = context.Drugs.Count();

            Assert.Equal(actual.Count, expected);
        }

        /// <summary>
        /// The TestReturnDrugById_WithCorrectData_ShouldReturnDrugsByuserId
        /// </summary>
        [Fact]
        public void TestReturnDrugById_WithCorrectData_ShouldReturnDrugsByuserId()
        {

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            var options = new DbContextOptionsBuilder<POSEDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new POSEDbContext(options);
            SeedTestData(context);
            var ids = string.Join(", ", context.Drugs.Select(a => a.Id));
            var services = new PrescriptionServices(context, mapper);

            var actual = services.ReturnDrugsById(context.Drugs.First().Id);
            var expected = 1;

            Assert.Equal(actual.Count, expected);
        }

        /// <summary>
        /// Defines the doctor
        /// </summary>
        private static Doctor doctor = new Doctor
        {
            Age = 35,
            Address = "sofia",
            Email = "doc1@abv.bg",
            FullName = "Ivan33",
            PasswordHash = "PasswordDoc",
            PhoneNumber = "0878655471",
            UDN = "1234567890",
            UserName = "Doctor",
            UserGuid = Guid.NewGuid().ToString()
        };

        /// <summary>
        /// Defines the store
        /// </summary>
        private static DrugStore store = new DrugStore
        {
            Address = "sofia11",
            Email = "store1@abv.bg",
            FullName = "store1",
            PasswordHash = "PasswordStore",
            PhoneNumber = "08786554711",
            CIN = "1234567890",
            UserName = "Store",
            UserGuid = Guid.NewGuid().ToString(),
            Role = UserRole.DrugStore,
        };

        /// <summary>
        /// Defines the patient
        /// </summary>
        private static Patient patient = new Patient
        {
            Age = 35,
            Address = "sofia",
            Email = "pesho@abv.bg",
            FullName = "Ivan",
            PasswordHash = "Password",
            PhoneNumber = "0878655471",
            PIN = Guid.NewGuid().ToString(),
            UserName = "Patient1",
            UserGuid = Guid.NewGuid().ToString()
        };

        /// <summary>
        /// The SeedTestData
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        private void SeedTestData(POSEDbContext context)
        {
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
            context.DrugStores.Add(store);
            context.Patients.Add(patient);
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }
    }
}
