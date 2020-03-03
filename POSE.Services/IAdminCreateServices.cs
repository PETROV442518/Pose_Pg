namespace POSE.Services
{
    using POSE.Services.Dtos;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="IAdminCreateServices" />
    /// </summary>
    public interface IAdminCreateServices
    {
        /// <summary>
        /// The CreateDrugIngredientAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="DrugIngredientDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateDrugIngredientAsync(DrugIngredientDto dto);

        /// <summary>
        /// The CreateTestAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="TestDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateTestAsync(TestDto dto);

        /// <summary>
        /// The CreateDrugAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="DrugDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateDrugAsync(DrugDto dto);

        /// <summary>
        /// The DeleteDrugAsync
        /// </summary>
        /// <param name="drugName">The drugName<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> DeleteDrugAsync(string drugName);

        /// <summary>
        /// The CreateDiseaseAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="DiseaseDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateDiseaseAsync(DiseaseDto dto);

        /// <summary>
        /// The DeleteIngredientAsync
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> DeleteIngredientAsync(string name);

        /// <summary>
        /// The DeleteTestAsync
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> DeleteTestAsync(string name);

        /// <summary>
        /// The DeleteDiseaseAsync
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> DeleteDiseaseAsync(string name);
    }
}
