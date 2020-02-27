namespace POSE.Services.Dtos
{
    /// <summary>
    /// Defines the <see cref="DiseaseDto" />
    /// </summary>
    public class DiseaseDto
    {
        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Contagious
        /// </summary>
        public bool Contagious { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Lethal
        /// </summary>
        public bool Lethal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Curable
        /// </summary>
        public bool Curable { get; set; }

        /// <summary>
        /// Gets or sets the Treatment
        /// </summary>
        public string Treatment { get; set; }
    }
}
