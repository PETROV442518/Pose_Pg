namespace POSE.Services
{
    using POSE.Services.Dtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IReceiptServices" />
    /// </summary>
    public interface IReceiptServices
    {
        /// <summary>
        /// The ReturnReceiptsByUser
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{ReceiptDto}}"/></returns>
        Task<List<ReceiptDto>> ReturnReceiptsByStore(string id);

        /// <summary>
        /// The ReturnReceiptsByUser
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{ReceiptDto}}"/></returns>
        Task<List<ReceiptDto>> ReturnReceiptsByUser(string id);
    }
}
