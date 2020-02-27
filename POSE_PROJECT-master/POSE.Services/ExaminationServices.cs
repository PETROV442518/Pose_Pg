namespace POSE.Services
{
    using AutoMapper;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ExaminationServices" />
    /// </summary>
    public class ExaminationServices : IExaminationServices
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
        /// Initializes a new instance of the <see cref="ExaminationServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
        public ExaminationServices(POSEDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <summary>
        /// The ReturnAllPatientsByDoctor
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="List{PatientDto}"/></returns>
        public List<PatientDto> ReturnAllPatientsByDoctor(string userGuid)
        {
            var patients = _context.Patients.Where(a => a.Doctor.UserGuid == userGuid
            && a.IsDeleted == false);
            var patientsDto = new List<PatientDto>();
            foreach (var pat in patients)
            {
                patientsDto.Add(_mapper.Map<PatientDto>(pat));
            }
            return patientsDto;
        }

        /// <summary>
        /// The ReturnAllDrugs
        /// </summary>
        /// <returns>The <see cref="Task{List{string}}"/></returns>
        public async Task<List<string>> ReturnAllDrugs()
        {
            var drugs = _context.Drugs.Where(a => a.IsDeleted == false).Select(a => a.Name).ToList();
            var drugNames = new List<string>();
            await Task.Run(() =>
            {
                foreach (var drug in drugs)
                {
                    drugNames.Add(drug);
                }
            });

            return drugNames;
        }

        /// <summary>
        /// The ReturnAllDrugsForPatient
        /// </summary>
        /// <param name="patientGuid">The patientGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{string}}"/></returns>
        public async Task<List<string>> ReturnAllDrugsForPatient(string patientGuid)
        {
            var drugNames = new List<string>();
            await Task.Run(() =>
            {
                var drugs = new List<string>();
                var patient = this._context.Patients.FirstOrDefault(a => a.UserGuid == patientGuid);
                if (patient.AllergicTo != null)
                {
                    var patientAlergens = patient.AllergicTo.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
                    drugs = _context.Drugs.Where(a => a.IsDeleted == false && !patientAlergens.Contains(a.Name)).Select(a => a.Name).ToList();
                }
                else
                {
                    drugs = _context.Drugs.Where(a => a.IsDeleted == false).Select(a => a.Name).ToList();
                }

                foreach (var drug in drugs)
                {
                    drugNames.Add(drug);
                }
            });
            return drugNames;
        }

        /// <summary>
        /// The ReturnAllDiseases
        /// </summary>
        /// <returns>The <see cref="Task{List{DiseaseDto}}"/></returns>
        public async Task<List<DiseaseDto>> ReturnAllDiseases()
        {
            var diseasesDto = new List<DiseaseDto>();
            await Task.Run(() =>
            {
                foreach (var item in this._context.Diseases.Where(a => a.IsDeleted == false).ToList())
                {
                    diseasesDto.Add(new DiseaseDto
                    {
                        Contagious = item.Contagious,
                        Curable = item.Curable,
                        Description = item.Description,
                        Lethal = item.Lethal,
                        Name = item.Name,
                        Treatment = item.Treatment
                    });
                }
            });
            return diseasesDto;
        }

        /// <summary>
        /// The ReturnAllTestsNames
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        public List<string> ReturnAllTestsNames()
        {
            var tests = _context.Tests.Where(a => a.IsDeleted == false).Select(a => a.Name).ToList();
            return tests;
        }

        /// <summary>
        /// The ReturnAllDiseaseNames
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        public List<string> ReturnAllDiseaseNames()
        {
            return this._context.Diseases.Where(a => a.IsDeleted == false).Select(a => a.Name).ToList();
        }

        /// <summary>
        /// The ReturnTestResultDtosByTestName
        /// </summary>
        /// <param name="testNames">The testNames<see cref="List{string}"/></param>
        /// <param name="patientUserGuid">The patientUserGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{TestResultDto}}"/></returns>
        public async Task<List<TestResultDto>> ReturnTestResultDtosByTestName(List<string> testNames, string patientUserGuid)
        {
            var testResults = new List<TestResultDto>();
            await Task.Run(() =>
            {
                var patient = this._context.Patients.FirstOrDefault(a => a.UserGuid == patientUserGuid);
                foreach (var name in testNames)
                {
                    testResults.Add(new TestResultDto
                    {
                        TestName = name,
                        PatientId = patient.Id
                    });
                }
            });
            return testResults;
        }

        /// <summary>
        /// The PerformExaminationAsync
        /// </summary>
        /// <param name="patientGuid">The patientGuid<see cref="string"/></param>
        /// <param name="testResults">The testResults<see cref="ICollection{string}"/></param>
        /// <param name="treatmentDuration">The treatmentDuration<see cref="int"/></param>
        /// <param name="description">The description<see cref="string"/></param>
        /// <param name="drugIds">The drugIds<see cref="ICollection{string}"/></param>
        /// <param name="diseaseNames">The diseaseNames<see cref="ICollection{string}"/></param>
        /// <param name="doctorId">The doctorId<see cref="string"/></param>
        /// <param name="drugStoreGuid">The drugStoreGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> PerformExaminationAsync(
            string patientGuid,
            ICollection<string> testResults,
            int treatmentDuration,
            string description,
            ICollection<string> drugIds,
            ICollection<string> diseaseNames,
            string doctorId,
            string drugStoreGuid)
        {

            Examination examination = new Examination();
            int result = -1;
            //Users
            var doctor = this._context.Doctors.FirstOrDefault(a => a.Id == doctorId
            );
            var patient = this._context.Patients.FirstOrDefault(a => a.UserGuid == patientGuid
            );
            var store = this._context.DrugStores.FirstOrDefault(a => a.UserGuid == drugStoreGuid
            );
            //Diagnosis
            Diagnosis diagnosis = new Diagnosis();
            diagnosis.Description = description;
            diagnosis.DoctorId = doctorId;
            diagnosis.PatientId = patient.Id;
            this._context.Diagnoses.Add(diagnosis);
            this._context.SaveChanges();
            patient.Diagnoses.Add(diagnosis);
            this._context.Patients.Update(patient);
            doctor.Diagnoses.Add(diagnosis);
            this._context.Doctors.Update(doctor);
            this._context.SaveChanges();

            //TestResults
            var testResultsForDb = new List<TestResult>();
            if (testResults != null)
            {
                foreach (var test in testResults.ToList())
                {
                    var data = test.Split(" --- ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var testData = data[0];
                    var tresult = data[1];
                    var testResult = new TestResult
                    {
                        PatientId = patient.Id,
                        Result = (Result)Enum.Parse(typeof(Result), tresult),
                        TestId = this._context.Tests.FirstOrDefault(a => a.Name == testData).Id
                    };
                    testResultsForDb.Add(testResult);

                }
                this._context.TestResults.AddRange(testResultsForDb);
                this._context.SaveChanges();
                foreach (var testforDb in testResultsForDb)
                {
                    patient.TestResults.Add(testforDb);
                    this._context.Patients.Update(patient);
                    this._context.SaveChanges();
                }
            }
            //Duration
            int duration = 0;
            if (treatmentDuration > 0)
            {
                duration = treatmentDuration;
            }
            //Drugs
            if (drugIds != null)
            {
                //Prescription
                Prescription prescription = new Prescription();
                var drugs = this._context.Drugs.Where(a => drugIds.ToList().Contains(a.Name)).ToList();
                var drugIdsForPrescription = String.Join(", ", drugs.Select(a => a.Id).ToList());
                prescription.PatientId = patient.Id;
                prescription.Patient = patient;
                prescription.TreatmentDuration = duration;
                prescription.DoctorId = doctorId;
                prescription.Doctor = doctor;
                prescription.DrugStoreId = store.Id;
                prescription.DrugStore = store;
                prescription.DiagnosisId = diagnosis.Id;
                prescription.Diagnosis = diagnosis;
                prescription.DrugIds = drugIdsForPrescription;
                this._context.Prescriptions.Add(prescription);
                this._context.SaveChanges();

                foreach (var drug in drugs)
                {
                    if (!patient.TakenDrugs.Contains(drug))
                    {
                        patient.TakenDrugs.Add(drug);
                        this._context.Patients.Update(patient);
                        this._context.SaveChanges();
                    }
                }

                //diagnosis
                diagnosis.PrescriptionId = prescription.Id;
                diagnosis.Prescription = prescription;
                this._context.Diagnoses.Update(diagnosis);
                this._context.Prescriptions.Update(prescription);
                this._context.SaveChanges();
                //users
                patient.Prescriptions.Add(prescription);
                this._context.Patients.Update(patient);
                this._context.SaveChanges();
                store.Prescriptions.Add(prescription);
                this._context.DrugStores.Update(store);
                this._context.SaveChanges();
                doctor.Prescriptions.Add(prescription);
                this._context.Doctors.Update(doctor);
                this._context.SaveChanges();

                examination.PrescriptionId = prescription.Id;
                this._context.SaveChanges();
            }
            //Diseases
            if (diseaseNames != null)
            {
                var diseases = this._context.Diseases.Where(a => diseaseNames.ToList().Contains(a.Name) && a.IsDeleted == false).ToList();
                foreach (var disease in diseases)
                {
                    patient.SpentDiseases.Add(disease);
                    this._context.Patients.Update(patient);

                }
                this._context.SaveChanges();
                diagnosis.Diseases = diseases;
                this._context.Diagnoses.Update(diagnosis);
                this._context.SaveChanges();
            }
            this._context.Diagnoses.Update(diagnosis);
            this._context.Patients.Update(patient);
            this._context.Doctors.Update(doctor);
            this._context.SaveChanges();
            //Examination
            examination.DiagnosisId = diagnosis.Id;
            examination.DoctorId = doctorId;
            examination.PatientId = patient.Id;
            examination.Tests = testResultsForDb;
            await this._context.Examinations.AddAsync(examination);
            this._context.SaveChanges();
            patient.Examinations.Add(examination);
            doctor.Examinations.Add(examination);
            doctor.Diagnoses.Add(diagnosis);
            this._context.Diagnoses.Update(diagnosis);
            this._context.Patients.Update(patient);
            this._context.Doctors.Update(doctor);
            this._context.SaveChanges();
            return result;
        }
    }
}
