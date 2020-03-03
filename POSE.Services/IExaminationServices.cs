namespace POSE.Services
{
    using POSE.Services.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IExaminationServices" />
    /// </summary>
    public interface IExaminationServices
    {
        /// <summary>
        /// The ReturnAllTestsNames
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        List<string> ReturnAllTestsNames();

        /// <summary>
        /// The ReturnAllPatientsByDoctor
        /// </summary>
        /// <param name="userGuid">The userGuid<see cref="string"/></param>
        /// <returns>The <see cref="List{PatientDto}"/></returns>
        List<PatientDto> ReturnAllPatientsByDoctor(string userGuid);

        /// <summary>
        /// The ReturnAllDiseases
        /// </summary>
        /// <returns>The <see cref="Task{List{DiseaseDto}}"/></returns>
        Task<List<DiseaseDto>> ReturnAllDiseases();

        /// <summary>
        /// The ReturnAllDrugs
        /// </summary>
        /// <returns>The <see cref="Task{List{string}}"/></returns>
        Task<List<string>> ReturnAllDrugs();

        /// <summary>
        /// The ReturnAllDiseaseNames
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        List<string> ReturnAllDiseaseNames();

        /// <summary>
        /// The ReturnTestResultDtosByTestName
        /// </summary>
        /// <param name="testNames">The testNames<see cref="List{string}"/></param>
        /// <param name="patientUserGuid">The patientUserGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{TestResultDto}}"/></returns>
        Task<List<TestResultDto>> ReturnTestResultDtosByTestName(List<string> testNames, string patientUserGuid);

        /// <summary>
        /// The PerformExaminationAsync
        /// </summary>
        /// <param name="patientGuid">The patientGuid<see cref="string"/></param>
        /// <param name="testResultIds">The testResultIds<see cref="ICollection{string}"/></param>
        /// <param name="treatmentDuration">The treatmentDuration<see cref="int"/></param>
        /// <param name="description">The description<see cref="string"/></param>
        /// <param name="drugIds">The drugIds<see cref="ICollection{string}"/></param>
        /// <param name="diseaseNames">The diseaseNames<see cref="ICollection{string}"/></param>
        /// <param name="doctorId">The doctorId<see cref="string"/></param>
        /// <param name="drugStoreGuid">The drugStoreGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> PerformExaminationAsync(string patientGuid, ICollection<string> testResultIds,
            int treatmentDuration, string description, ICollection<string> drugIds,
            ICollection<string> diseaseNames, string doctorId, string drugStoreGuid);

        /// <summary>
        /// The ReturnAllDrugsForPatient
        /// </summary>
        /// <param name="patientGuid">The patientGuid<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{string}}"/></returns>
        Task<List<string>> ReturnAllDrugsForPatient(string patientGuid);
    }
}
