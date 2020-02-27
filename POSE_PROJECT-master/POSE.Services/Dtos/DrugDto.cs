namespace POSE.Services.Dtos
{
    using POSE.Domain;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DrugDto" />
    /// </summary>
    public class DrugDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrugDto"/> class.
        /// </summary>
        public DrugDto()
        {
            this.Ingredients = new List<string>();
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the GenericGroup
        /// </summary>
        public string GenericGroup { get; set; }

        /// <summary>
        /// Gets or sets the Producer
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public DrugType Type { get; set; }

        /// <summary>
        /// Gets or sets the Form
        /// </summary>
        public DrugForm Form { get; set; }

        /// <summary>
        /// Gets or sets the Dose
        /// </summary>
        public decimal Dose { get; set; }

        /// <summary>
        /// Gets or sets the DosesPerDay
        /// </summary>
        public int DosesPerDay { get; set; }

        /// <summary>
        /// Gets or sets the Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the Leaflat
        /// </summary>
        public string Leaflat { get; set; }

        /// <summary>
        /// Gets or sets the Ingredients
        /// </summary>
        public ICollection<string> Ingredients { get; set; }
    }
}
