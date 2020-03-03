namespace POSE_Tests.Service
{
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Domain;
    using POSE.Services;
    using System;
    using System.Collections.Generic;
    using Xunit;

    //DrugIngredient ReturnIngredient(string name);
    /// <summary>
    /// Defines the <see cref="IngredientServicesTests" />
    /// </summary>
    public class IngredientServicesTests
    {
        /// <summary>
        /// Defines the ingredientsList
        /// </summary>
        private List<DrugIngredient> ingredientsList =
            new List<DrugIngredient>()
        {
            new DrugIngredient
            {
                Name = "Natrii",
                Description = "Pure natrii"
            },
            new DrugIngredient
            {
                Name = "Magnesii",
                Description = "Pure magnesii"
            }
        };

        /// <summary>
        /// The TestReturnIngredient_WithCorrectData_ShouldReturnDrugIngredientByName
        /// </summary>
        [Fact]
        public void TestReturnIngredient_WithCorrectData_ShouldReturnDrugIngredientByName()
        {

            var options = new DbContextOptionsBuilder<POSEDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new POSEDbContext(options);
            SeedTestData(context);
            var services = new IngredientsServices(context);
            var actual = services.ReturnIngredient(ingredientsList[0].Name);
            var expected = ingredientsList[0];
            Assert.Equal(actual, expected);
        }

        /// <summary>
        /// The TestIngredientNames_WithCorrectData_ShouldReturnListOfString
        /// </summary>
        [Fact]
        public void TestIngredientNames_WithCorrectData_ShouldReturnListOfString()
        {

            var options = new DbContextOptionsBuilder<POSEDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new POSEDbContext(options);
            SeedTestData(context);
            var services = new IngredientsServices(context);
            var actual = services.IngredientNames();
            var expected = ingredientsList.Count;
            Assert.Equal(actual.Count, expected);
        }

        /// <summary>
        /// The SeedTestData
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        private void SeedTestData(POSEDbContext context)
        {
            context.DrugIngredients.AddRange(ingredientsList);
            context.SaveChanges();
        }
    }
}
