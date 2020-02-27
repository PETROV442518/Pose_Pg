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
    /// Defines the <see cref="ExaminationServicesTests" />
    /// </summary>
    public class ExaminationServicesTests
    {
        /// <summary>
        /// The TestReturnAllTestsNames_WithCorrectData_ShouldReturnListOfString
        /// </summary>
        [Fact]
        public void TestReturnAllTestsNames_WithCorrectData_ShouldReturnListOfString()
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
            var service = new ExaminationServices(context, mapper);
            var actual = service.ReturnAllTestsNames();
            var expected = new List<string>() { "test name" };

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// The TestReturnAllDrugs_WithCorrectData_ShouldReturnListOfString
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestReturnAllDrugs_WithCorrectData_ShouldReturnListOfString()
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
            var service = new ExaminationServices(context, mapper);
            var actual = await service.ReturnAllDrugs();
            var expected = new List<string>()
            {
                "Penicilin"
            };

            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected[0], actual[0]);
        }

        /// <summary>
        /// The TestReturnAllDiseaseNames_WithCorrectData_ShouldReturnListOfString
        /// </summary>
        [Fact]
        public void TestReturnAllDiseaseNames_WithCorrectData_ShouldReturnListOfString()
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
            var service = new ExaminationServices(context, mapper);
            var actual = service.ReturnAllDiseaseNames();
            var expected = new List<string>()
            {
                "disease name"
            };

            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected[0], actual[0]);
        }

        /// <summary>
        /// The TestReturnAllDiseases_WithCorrectData_ShouldReturnListOfDiseaseDto
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestReturnAllDiseases_WithCorrectData_ShouldReturnListOfDiseaseDto()
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
            var service = new ExaminationServices(context, mapper);
            var actual = await service.ReturnAllDiseases();
            var expected = new List<DiseaseDto>()
            {
                new DiseaseDto{
                Contagious = false,
                Curable = false,
                Description = "description",
                Lethal = false,
                Name = "disease name",
                Treatment = "treatment",
                }
            };

            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected[0].Description, actual[0].Description);
        }

        /// <summary>
        /// Defines the test
        /// </summary>
        private static Test test = new Test
        {
            Description = "test descr",
            Name = "test name",

        };

        /// <summary>
        /// Defines the testResult
        /// </summary>
        private TestResult testResult = new TestResult
        {
            PatientId = patient.Id,
            TestId = test.Id,
        };

        /// <summary>
        /// Defines the disease
        /// </summary>
        private Disease disease = new Disease
        {
            Contagious = false,
            Curable = false,
            Description = "description",
            Lethal = false,
            Name = "disease name",
            Treatment = "treatment",
        };

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
        /// Defines the drug
        /// </summary>
        private static Drug drug = new Drug
        {
            Dose = 1,
            DosesPerDay = 1,
            Form = DrugForm.Ampoule,
            GenericGroup = "Penicilin",
            Ingredients = new List<string>()
            {
                "Natrii",
                "Penicilin"
            },
            Leaflat = "Leaflat",
            Name = "Penicilin",
            Price = 4.80M,
            Producer = "Actavis",
            Type = DrugType.Antibiotic
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
            context.TestResults.Add(testResult);
            context.Tests.Add(test);
            context.Drugs.Add(drug);
            context.Diseases.Add(disease);
            context.SaveChanges();
        }
    }
}
