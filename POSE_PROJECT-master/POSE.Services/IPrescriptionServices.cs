namespace POSE.Services
{
    using POSE.Services.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IPrescriptionServices" />
    /// </summary>
    public interface IPrescriptionServices
    {
        /// <summary>
        /// The ReturnPrescriptionsByStoreId
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{PrescriptionDto}}"/></returns>
        Task<List<PrescriptionDto>> ReturnPrescriptionsByStoreId(string id);

        /// <summary>
        /// The GetPrescriptionById
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{PrescriptionDto}"/></returns>
        Task<PrescriptionDto> GetPrescriptionById(string id);

        /// <summary>
        /// The CreateReceipt
        /// </summary>
        /// <param name="prescription">The prescription<see cref="PrescriptionDto"/></param>
        /// <returns>The <see cref="Task{ReceiptDto}"/></returns>
        Task<ReceiptDto> CreateReceipt(PrescriptionDto prescription);

        /// <summary>
        /// The ReturnDrugById
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{DrugDto}"/></returns>
        Task<DrugDto> ReturnDrugById(string id);

        /// <summary>
        /// The ReturnDrugsById
        /// </summary>
        /// <param name="DrugId">The DrugId<see cref="string"/></param>
        /// <returns>The <see cref="ICollection{DrugDto}"/></returns>
        ICollection<DrugDto> ReturnDrugsById(string DrugId);
    }
}
