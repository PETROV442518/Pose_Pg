namespace POSE.Services
{
    using AutoMapper;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services.Dtos;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="AdminCreateServices" />
    /// </summary>
    public class AdminCreateServices : IAdminCreateServices
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
        /// Initializes a new instance of the <see cref="AdminCreateServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
        public AdminCreateServices(POSEDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        /// <summary>
        /// The CreateDiseaseAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="DiseaseDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> CreateDiseaseAsync(DiseaseDto dto)
        {
            var result = -1;
            if (this._context.Diseases.Select(a => a.Name.ToLower()).Contains(dto.Name.ToLower()))
            {
                return result;
            }

            var disease = _mapper.Map<Disease>(dto);
            await this._context.Diseases.AddAsync(disease);
            result = await this._context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The CreateDrugAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="DrugDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> CreateDrugAsync(DrugDto dto)
        {
            var result = -1;
            if (this._context.Drugs.Select(a => a.Name.ToLower()).Contains(dto.Name.ToLower()))
            {
                return result;
            }
            var drug = _mapper.Map<Drug>(dto);
            var drugIngredientsFromDb = this._context.DrugIngredients.ToList();
            foreach (var ingr in drugIngredientsFromDb)
            {
                if (dto.Ingredients.Contains(ingr.Name))
                {
                    drug.Ingredients.Add(ingr.Name);
                }
            }
            await this._context.Drugs.AddAsync(drug);

            foreach (var ingr in drugIngredientsFromDb)
            {
                if (drug.Ingredients.Contains(ingr.Name))
                {
                    ingr.Drugs.Add(drug.Id);
                    this._context.DrugIngredients.Update(ingr);
                }
            }

            result = await this._context.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// The CreateDrugIngredientAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="DrugIngredientDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> CreateDrugIngredientAsync(DrugIngredientDto dto)
        {

            var result = -1;
            if (this._context.DrugIngredients.Select(a => a.Name.ToLower()).Contains(dto.Name.ToLower()))
            {
                return result;
            }
            var drugIngredient = _mapper.Map<DrugIngredient>(dto);
            await this._context.DrugIngredients.AddAsync(drugIngredient);
            result = await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The CreateTestAsync
        /// </summary>
        /// <param name="dto">The dto<see cref="TestDto"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> CreateTestAsync(TestDto dto)
        {
            var result = -1;
            var test = _mapper.Map<Test>(dto);
            if (this._context.Tests.Select(a => a.Name.ToLower()).Contains(dto.Name.ToLower()))
            {
                return result;
            }
            await this._context.Tests.AddAsync(test);
            result = await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The DeleteDiseaseAsync
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> DeleteDiseaseAsync(string name)
        {
            var result = -1;
            var disease = this._context.Diseases.FirstOrDefault(a => a.Name == name);
            if (disease == null)
            {
                return result;
            }
            disease.IsDeleted = true;
            this._context.Diseases.Update(disease);
            result = await this._context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The DeleteDrugAsync
        /// </summary>
        /// <param name="drugName">The drugName<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> DeleteDrugAsync(string drugName)
        {
            var drug = this._context.Drugs.FirstOrDefault(a => a.Name == drugName);
            drug.IsDeleted = true;
            this._context.Drugs.Update(drug);
            var result = await this._context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The DeleteIngredientAsync
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> DeleteIngredientAsync(string name)
        {
            var ingredient = this._context.DrugIngredients.FirstOrDefault(a => a.Name == name);
            var result = -1;
            if (ingredient == null)
            {
                return result;
            }
            ingredient.IsDeleted = true;
            this._context.DrugIngredients.Update(ingredient);
            result = await this._context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The DeleteTestAsync
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> DeleteTestAsync(string name)
        {
            var result = -1;
            var test = this._context.Tests.FirstOrDefault(a => a.Name == name);
            if (test == null)
            {
                return result;
            }
            test.IsDeleted = true;
            this._context.Tests.Update(test);
            result = await this._context.SaveChangesAsync();
            return result;
        }
    }
}
