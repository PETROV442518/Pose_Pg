namespace POSE_Tests.Service
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="AccountServicesTests" />
    /// </summary>
    public class AccountServicesTests
    {
        /// <summary>
        /// The TestReturnDoctor_WithCorrectData_ShouldReturnUserByuserGuid
        /// </summary>
        [Fact]
        public void TestReturnDoctor_WithCorrectData_ShouldReturnUserByuserGuid()
        {
            var options = new DbContextOptionsBuilder<POSEDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new POSEDbContext(options);
            SeedTestData(context);
            var ac = new AccountServices(context);
            var actual = ac.ReturnDoctor(doctor.UserGuid);
            var expected = doctor;

            Assert.Equal(actual.UserGuid, expected.UserGuid);
            Assert.Equal(actual.UserName, expected.UserName);
        }

        /// <summary>
        /// The TestReturnPatient_WithCorrectData_ShouldReturnUserByuserGuid
        /// </summary>
        [Fact]
        public void TestReturnPatient_WithCorrectData_ShouldReturnUserByuserGuid()
        {
            var options = new DbContextOptionsBuilder<POSEDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new POSEDbContext(options);
            SeedTestData(context);
            var ac = new AccountServices(context);
            var actual = ac.ReturnPatient(patient.UserGuid);
            var expected = patient;

            Assert.Equal(actual.UserGuid, expected.UserGuid);
            Assert.Equal(actual.UserName, expected.UserName);
        }

        /// <summary>
        /// The TestReturnId_WithCorrectData_ShouldReturnIdByuserGuid
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task TestReturnId_WithCorrectData_ShouldReturnIdByuserGuid()
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
            var ac = new AccountServices(context, mapper);
            var actual = await ac.ReturnId(store.UserGuid);
            var expected = store;

            Assert.Equal(actual, expected.Id);
        }

        /// <summary>
        /// The TestReturnDrugStore_WithCorrectData_ShouldReturnUserDtoByuserGuid
        /// </summary>
        [Fact]
        public void TestReturnDrugStore_WithCorrectData_ShouldReturnUserDtoByuserGuid()
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
            var ac = new AccountServices(context, mapper);
            var actual = ac.ReturnDrugStore(store.UserGuid);
            var expected = store;

            Assert.Equal(actual.UserGuid, expected.UserGuid);
        }

        /// <summary>
        /// The TestReturnPatientDto_WithCorrectData_ShouldReturnUserDtoByuserGuid
        /// </summary>
        [Fact]
        public void TestReturnPatientDto_WithCorrectData_ShouldReturnUserDtoByuserGuid()
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
            var ac = new AccountServices(context, mapper);
            var actual = ac.ReturnPatientDto(patient.UserGuid);
            var expected = patient;

            Assert.Equal(actual.UserGuid, expected.UserGuid);
        }

        /// <summary>
        /// The TestReturnDoctorDto_WithCorrectData_ShouldReturnUserDtoByuserGuid
        /// </summary>
        [Fact]
        public void TestReturnDoctorDto_WithCorrectData_ShouldReturnUserDtoByuserGuid()
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
            var ac = new AccountServices(context, mapper);
            var actual = ac.ReturnDoctorDto(doctor.UserGuid);
            var expected = doctor;

            Assert.Equal(actual.UserGuid, expected.UserGuid);
        }

        /// <summary>
        /// The TestPatientsCountByDoctor_WithCorrectData_ShouldReturnCountByuseId
        /// </summary>
        [Fact]
        public void TestPatientsCountByDoctor_WithCorrectData_ShouldReturnCountByuseId()
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
            var ac = new AccountServices(context, mapper);

            var actual = ac.ReturnAllDrugStoreDtos();
            var expected = 1;

            Assert.Equal(actual.Count, expected);
        }

        /// <summary>
        /// The TestReturnAllDrugStoreDtos_WithCorrectData_ShouldReturnListOfDrugStoreDtoByuserGuid
        /// </summary>
        [Fact]
        public void TestReturnAllDrugStoreDtos_WithCorrectData_ShouldReturnListOfDrugStoreDtoByuserGuid()
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
            var ac = new AccountServices(context, mapper);

            var actual = ac.PatientsCountByDoctor(doctor.Id);
            var expected = 0;

            Assert.Equal(actual, expected);
        }

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
    }
}
