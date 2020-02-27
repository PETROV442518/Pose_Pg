namespace POSE.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services.Dtos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="AccountServices" />
    /// </summary>
    public class AccountServices : IAccountServices
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly POSEDbContext _context;

        /// <summary>
        /// Defines the _mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
        public AccountServices(POSEDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        public AccountServices(POSEDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// The ReturnPatient
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="Patient"/></returns>
        public Patient ReturnPatient(string userGuid)
        {
            return _context.Patients.Where(a => a.UserGuid == userGuid)
                .Include(a => a.Doctor)
                .Include(a => a.PreferedDrugStore)
                .SingleOrDefault();
        }

        /// <summary>
        /// The ReturnPatientDto
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="PatientDto"/></returns>
        public PatientDto ReturnPatientDto(string userGuid)
        {
            var patient = _context.Patients.Where(a => a.UserGuid == userGuid)
                .Include(a => a.Doctor)
                .Include(a => a.PreferedDrugStore)
                .Include(a => a.Diagnoses)
                .Include(a => a.Examinations)
                .Include(a => a.Prescriptions)
                .Include(a => a.Receipts)
                .Include(a => a.SpentDiseases)
                .Include(a => a.TakenDrugs)
                .SingleOrDefault();
            var dto = _mapper.Map<PatientDto>(patient);
            return dto;
        }

        /// <summary>
        /// The PatientsCountByDoctor
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int PatientsCountByDoctor(string id)
        {
            int result = this._context.Patients.Where(a => a.DoctorId == id && a.IsDeleted == false)
                .Count();
            return result;
        }

        /// <summary>
        /// The ReturnDoctorDto
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="DoctorDto"/></returns>
        public DoctorDto ReturnDoctorDto(string userGuid)
        {
            var doctor = _context.Doctors.Where(a => a.UserGuid == userGuid)
                 .Include(a => a.Patients)
                 .Include(a => a.Diagnoses)
                 .Include(a => a.Examinations)
                 .Include(a => a.Prescriptions)
                 .SingleOrDefault();

            var dto = new DoctorDto
            {
                Address = doctor.Address,
                Age = doctor.Age,
                Diagnoses = doctor.Diagnoses,
                Email = doctor.Email,
                Examinations = doctor.Examinations,
                FullName = doctor.FullName,
                IsDeleted = doctor.IsDeleted,
                PatientsScore = doctor.PatientsScore,
                Prescriptions = doctor.Prescriptions,
                Role = doctor.Role,
                Specialty = doctor.Specialty,
                UserGuid = doctor.UserGuid
            };
            var patientsDto = new List<PatientDto>();
            if (doctor.Patients.Count > 0)
            {
                foreach (var pat in doctor.Patients)
                {
                    var patDto = _mapper.Map<PatientDto>(pat);
                    patientsDto.Add(patDto);
                }
                dto.Patients = patientsDto;
            }

            return dto;
        }

        /// <summary>
        /// The ReturnDoctor
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="Doctor"/></returns>
        public Doctor ReturnDoctor(string userGuid)
        {
            return _context.Doctors.Where(a => a.UserGuid == userGuid).Include(a => a.Patients).SingleOrDefault();
        }

        /// <summary>
        /// The ReturnDrugStore
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="DrugStoreDto"/></returns>
        public DrugStoreDto ReturnDrugStore(string userGuid)
        {
            var store = _context.DrugStores
                .Where(a => a.UserGuid == userGuid)
                .Include(a => a.Clients)
                .Include(a => a.Prescriptions)
                .Include(a => a.Receipts)
                .SingleOrDefault();

            var storeDto = _mapper.Map<DrugStoreDto>(store);

            return storeDto;
        }

        /// <summary>
        /// The ReturnAllDrugStoreDtos
        /// </summary>
        /// <returns>The <see cref="List{DrugStoreDto}"/></returns>
        public List<DrugStoreDto> ReturnAllDrugStoreDtos()
        {
            var result = new List<DrugStoreDto>();

            var stores = this._context.DrugStores.Where(a => a.IsDeleted == false).ToList();
            foreach (var store in stores)
            {
                var storeDto = _mapper.Map<DrugStoreDto>(store);
                result.Add(storeDto);
            }

            return result;
        }

        /// <summary>
        /// The UpdatePatient
        /// </summary>
        /// <param name="patient">The patient<see cref="Patient"/></param>
        //public void UpdatePatient(Patient patient)
        //{
        //this._context.Patients.Update(patient);
        //}

        /// <summary>
        /// The ReturnId
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        public async Task<string> ReturnId(string userGuid)
        {
            var id = "";
            await Task.Run(() =>
            {
                id = this._context.Users.FirstOrDefault(a => a.UserGuid == userGuid && a.IsDeleted == false).Id;
            });
            return id;
        }
    }
}
