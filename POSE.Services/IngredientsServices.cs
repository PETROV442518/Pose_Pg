namespace POSE.Services
{
    using POSE.Data;
    using POSE.Domain;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="IngredientsServices" />
    /// </summary>
    public class IngredientsServices : InterfaceIngredientsServices
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly POSEDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="IngredientsServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        public IngredientsServices(POSEDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// The IngredientNames
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        public List<string> IngredientNames()
        {
            List<string> Names = _context.DrugIngredients.Where(a => a.IsDeleted == false).Select(a => a.Name).ToList();

            return Names;
        }

        /// <summary>
        /// The ReturnIngredient
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="DrugIngredient"/></returns>
        public DrugIngredient ReturnIngredient(string name)
        {
            return _context.DrugIngredients.Where(a => a.IsDeleted == false).FirstOrDefault(a => a.Name == name);
        }
    }
}
