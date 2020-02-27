namespace POSE.Services
{
    using AutoMapper;
    using POSE.Domain;
    using POSE.Services.Dtos;

    /// <summary>
    /// Defines the <see cref="MappingProfile" />
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();
            CreateMap<DrugStore, DrugStoreDto>();
            CreateMap<DrugStoreDto, DrugStore>();
            CreateMap<Drug, DrugDto>();
            CreateMap<DrugDto, Drug>();
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<ReceiptDto, Receipt>();
            CreateMap<DiseaseDto, Disease>();
            CreateMap<Disease, DiseaseDto>();
            CreateMap<DrugIngredient, DrugIngredientDto>();
            CreateMap<DrugIngredientDto, DrugIngredient>();
            CreateMap<Test, TestDto>();
            CreateMap<TestDto, Test>();
        }
    }
}
