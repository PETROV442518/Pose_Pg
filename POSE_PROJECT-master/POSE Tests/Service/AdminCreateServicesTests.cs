namespace POSE_Tests.Service
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services;
    using POSE.Services.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="AdminCreateServicesTests" />
    /// </summary>
    public class AdminCreateServicesTests
    {
        /// <summary>
        /// The TestCreateDisease_WithCorrectData_ShouldReturnChangesCountByDiseaseDto
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestCreateDisease_WithCorrectData_ShouldReturnChangesCountByDiseaseDto()
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

            var service = new AdminCreateServices(context, mapper);
            var dto = new DiseaseDto
            {
                Contagious = true,
                Curable = true,
                Description = "Disease description",
                Lethal = true,
                Name = "Name",
                Treatment = "Treatment",
            };
            var actual = await service.CreateDiseaseAsync(dto);
            Assert.True(actual > 0);
        }

        /// <summary>
        /// The TestCreateDrugIngredient_WithCorrectData_ShouldReturnChangesCountByIngredientDto
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestCreateDrugIngredient_WithCorrectData_ShouldReturnChangesCountByIngredientDto()
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

            var service = new AdminCreateServices(context, mapper);

            var dto = new DrugIngredientDto
            {
                Description = "Test description",
                Name = "Test name",
            };

            var actual = await service.CreateDrugIngredientAsync(dto);
            Assert.True(actual > 0);
        }

        /// <summary>
        /// The TestCreateDrug_WithCorrectData_ShouldReturnChangesCountByDrugDto
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestCreateDrug_WithCorrectData_ShouldReturnChangesCountByDrugDto()
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

            var service = new AdminCreateServices(context, mapper);

            var dto = new DrugDto
            {
                Dose = 1,
                DosesPerDay = 1,
                Form = DrugForm.Pill,
                GenericGroup = "Penicilin",
                Ingredients = new List<string>
               {
                   "Penicilin",
                   "Natrii"
               },
                Leaflat = "Leaflat",
                Price = 4.30M,
                Producer = "Actavis",
                Type = DrugType.Antibiotic,
                Name = "Drug name",
            };

            var actual = await service.CreateDrugAsync(dto);
            Assert.True(actual > 0);
        }

        /// <summary>
        /// Defines the doctor
        /// </summary>
        private Doctor doctor = new Doctor
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
        private DrugStore store = new DrugStore
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
        private Patient patient = new Patient
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
            context.DrugStores.Add(store);
            context.Patients.Add(patient);
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }
    }
}
