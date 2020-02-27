namespace POSE.Services
{
    using POSE.Domain;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="InterfaceIngredientsServices" />
    /// </summary>
    public interface InterfaceIngredientsServices
    {
        /// <summary>
        /// The IngredientNames
        /// </summary>
        /// <returns>The <see cref="List{string}"/></returns>
        List<string> IngredientNames();

        /// <summary>
        /// The ReturnIngredient
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="DrugIngredient"/></returns>
        DrugIngredient ReturnIngredient(string name);
    }
}
