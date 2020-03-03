namespace POSE.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="PrescriptionServices" />
    /// </summary>
    public class PrescriptionServices : IPrescriptionServices
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
        /// Initializes a new instance of the <see cref="PrescriptionServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
        public PrescriptionServices(POSEDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <summary>
        /// The ReturnPrescriptionsByStoreId
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{PrescriptionDto}}"/></returns>
        public async Task<List<PrescriptionDto>> ReturnPrescriptionsByStoreId(string id)
        {
            var prescriptionsDto = new List<PrescriptionDto>();
            await Task.Run(() =>
            {
                var prescriptions = this._context.Prescriptions
                .Where(a => a.DrugStoreId == id && a.IsExecuted == false)
                .Include(a => a.Patient)
                .ToList();

                foreach (var prescr in prescriptions)
                {
                    var patient = this._context.Patients
                    .FirstOrDefault(a => a.UserGuid == prescr.Patient.UserGuid);
                    var drugIds = prescr.DrugIds.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
                    var drugs = this._context.Drugs.Where(a => drugIds.Contains(a.Id)).ToList();
                    var patientDto = _mapper.Map<PatientDto>(patient);


                    var drugsDto = new List<DrugDto>();
                    foreach (var drug in drugs)
                    {
                        drugsDto.Add(_mapper.Map<DrugDto>(drug));
                    }
                    prescriptionsDto.Add(new PrescriptionDto
                    {
                        DiagnosisId = prescr.DiagnosisId,
                        DoctorId = prescr.DoctorId,
                        Drugs = drugsDto,
                        DrugStoreId = prescr.DrugStoreId,
                        PatientId = patient.Id,
                        TreatmentDuration = prescr.TreatmentDuration,
                        Id = prescr.Id,
                        Patient = patientDto
                    });
                }
            });
            return prescriptionsDto;
        }

        /// <summary>
        /// The ReturnDrugById
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{DrugDto}"/></returns>
        public async Task<DrugDto> ReturnDrugById(string id)
        {
            var drug = await this._context.Drugs.FirstOrDefaultAsync(a => a.Id == id);
            var dto = _mapper.Map<DrugDto>(drug);
            return dto;
        }

        /// <summary>
        /// The ReturnDrugsById
        /// </summary>
        /// <param name="DrugId">The DrugId<see cref="string"/></param>
        /// <returns>The <see cref="ICollection{DrugDto}"/></returns>
        public ICollection<DrugDto> ReturnDrugsById(string DrugId)
        {
            var result = new List<DrugDto>();

            var ids = DrugId.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var id in ids)
            {
                var drug = this._context.Drugs.FirstOrDefault(a => a.Id == id);
                var dto = _mapper.Map<DrugDto>(drug);
                result.Add(dto);
            }

            return result;
        }

        /// <summary>
        /// The GetPrescriptionById
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{PrescriptionDto}"/></returns>
        public async Task<PrescriptionDto> GetPrescriptionById(string id)
        {
            var prescriptionDto = new PrescriptionDto();
            await Task.Run(() =>
            {
                var prescription = this._context.Prescriptions.Where(a => a.Id == id)
                .Include(a => a.Patient)
                .Include(a => a.DrugStore)
                .SingleOrDefault();
                var drugIds = prescription.DrugIds.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();
                var drugs = this._context.Drugs.Where(a => drugIds.Contains(a.Id)).ToList();

                //DrugsDto
                var drugsDto = new List<DrugDto>();
                foreach (var drug in drugs)
                {
                    drugsDto.Add(_mapper.Map<DrugDto>(drug));
                }

                //PatientDto
                var patient = prescription.Patient;
                var patientDto = _mapper.Map<PatientDto>(patient);

                //PrescriptionDto
                prescriptionDto.DiagnosisId = prescription.DiagnosisId;
                prescriptionDto.DoctorId = prescription.DoctorId;
                prescriptionDto.Drugs = drugsDto;
                prescriptionDto.DrugStoreId = prescription.DrugStoreId;
                prescriptionDto.Patient = patientDto;
                prescriptionDto.Id = prescription.Id;
                prescriptionDto.IssuedOn = prescription.IssuedOn;
                prescriptionDto.PatientId = prescription.PatientId;
                prescriptionDto.TreatmentDuration = prescription.TreatmentDuration;
                prescriptionDto.IsExecuted = prescription.IsExecuted;
            });
            return prescriptionDto;
        }

        /// <summary>
        /// The CreateReceipt
        /// </summary>
        /// <param name="prescription">The prescription<see cref="PrescriptionDto"/></param>
        /// <returns>The <see cref="Task{ReceiptDto}"/></returns>
        public async Task<ReceiptDto> CreateReceipt(PrescriptionDto prescription)
        {
            var receiptDto = new ReceiptDto();
            await Task.Run(() =>
            {
                var drugs = this._context.Drugs.Where(a => prescription.Drugs.Select(d => d.Name).Contains(a.Name)).ToList();
                decimal fee = 0M;
                var drugsDto = new List<DrugDto>();
                foreach (var drug in drugs)
                {

                    drugsDto.Add(_mapper.Map<DrugDto>(drug));
                    fee += drug.Price;
                }
                var receipt = new Receipt
                {
                    ClientId = prescription.PatientId,
                    DrugStoreId = prescription.DrugStoreId,
                    Drugs = drugs,
                    Fee = fee,
                };
                this._context.Receipts.Add(receipt);

                var patient = this._context.Patients.FirstOrDefault(a => a.Id == prescription.PatientId);
                patient.SumForDrugs += fee;
                this._context.Patients.Update(patient);
                var store = this._context.DrugStores.FirstOrDefault(a => a.Id == prescription.DrugStoreId);
                var storeDto = _mapper.Map<DrugStoreDto>(store);
                //Prescription Update
                var prescriptionFromDb = this._context.Prescriptions.FirstOrDefault(a => a.Id == prescription.Id);
                prescriptionFromDb.IsExecuted = true;
                this._context.Prescriptions.Update(prescriptionFromDb);
                this._context.SaveChanges();
                //ReceiptDto
                receiptDto.ClientId = receipt.ClientId;
                receiptDto.DrugStoreId = receipt.DrugStoreId;
                receiptDto.Fee = receipt.Fee;
                receiptDto.IssuedOn = receipt.IssuedOn;
                receiptDto.Id = receipt.Id;
                receiptDto.DrugStore = storeDto;
                receiptDto.Drugs = drugsDto;
            });

            return receiptDto;
        }
    }
}
