namespace POSE.Services
{
    using POSE.Domain;
    using POSE.Services.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IAccountServices" />
    /// </summary>
    public interface IAccountServices
    {
        /// <summary>
        /// The ReturnPatient
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="Patient"/></returns>
        Patient ReturnPatient(string userGuid);

        /// <summary>
        /// The ReturnDoctor
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="Doctor"/></returns>
        Doctor ReturnDoctor(string userGuid);

        /// <summary>
        /// The ReturnDrugStore
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="DrugStoreDto"/></returns>
        DrugStoreDto ReturnDrugStore(string userGuid);

        /// <summary>
        /// The UpdatePatient
        /// </summary>
        /// <param name="patient">The patient<see cref="Patient"/></param>
        //void UpdatePatient(Patient patient);

        /// <summary>
        /// The PatientsCountByDoctor
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="int"/></returns>
        int PatientsCountByDoctor(string id);

        /// <summary>
        /// The ReturnPatientDto
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="PatientDto"/></returns>
        PatientDto ReturnPatientDto(string userGuid);

        /// <summary>
        /// The ReturnAllDrugStoreDtos
        /// </summary>
        /// <returns>The <see cref="List{DrugStoreDto}"/></returns>
        List<DrugStoreDto> ReturnAllDrugStoreDtos();

        /// <summary>
        /// The ReturnId
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        Task<string> ReturnId(string userGuid);

        /// <summary>
        /// The ReturnDoctorDto
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="DoctorDto"/></returns>
        DoctorDto ReturnDoctorDto(string userGuid);
    }
}
