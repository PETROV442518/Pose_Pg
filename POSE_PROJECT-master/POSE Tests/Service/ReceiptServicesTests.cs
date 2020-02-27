namespace POSE_Tests.Service
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="ReceiptServicesTests" />
    /// </summary>
    public class ReceiptServicesTests
    {
        /// <summary>
        /// The TestReturnReceiptsByStore_WithCorrectData_ShouldReturnListOfReceiptsByuserId
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestReturnReceiptsByStore_WithCorrectData_ShouldReturnListOfReceiptsByuserId()
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
            var services = new ReceptServices(context, mapper);

            var actual = await services.ReturnReceiptsByStore(store.Id);
            var expected = receipts.Count;

            Assert.Equal(actual.Count, expected);
        }

        /// <summary>
        /// The TestReturnReceiptsByUserId_WithCorrectData_ShouldReturnListOfReceiptsByuserId
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestReturnReceiptsByUserId_WithCorrectData_ShouldReturnListOfReceiptsByuserId()
        {
            //Task<List<ReceiptDto>> ReturnReceiptsByStore(string id);
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
            var services = new ReceptServices(context, mapper);

            var actual = await services.ReturnReceiptsByUser(patient.Id);
            var expected = 4;

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
            UserGuid = Guid.NewGuid().ToString()
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
        /// Defines the receipts
        /// </summary>
        private List<Receipt> receipts = new List<Receipt>()
        {
            new Receipt
            {
                ClientId = patient.Id,
                DrugStoreId = store.Id,
                Fee = 10,
                Drugs = new List<Drug>(),
            },
            new Receipt
            {
                ClientId = patient.Id,
                DrugStoreId = store.Id,
                Fee = 12,
                Drugs = new List<Drug>(),
            },
        };

        /// <summary>
        /// The SeedTestData
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        private void SeedTestData(POSEDbContext context)
        {
            context.DrugStores.Add(store);
            context.Patients.Add(patient);
            context.Doctors.Add(doctor);
            context.DrugIngredients.Add(new DrugIngredient { Name = "Vitamin C", Description = "Vitamin C" });
            context.DrugIngredients.Add(new DrugIngredient { Name = "Vitamin B", Description = "compex Vitamin B" });
            context.DrugIngredients.Add(new DrugIngredient { Name = "Vitamin A", Description = "compex Vitamin A" });
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
            context.Receipts.AddRange(receipts);
            context.SaveChanges();
        }
    }
}
