namespace POSE.Services.Dtos
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="TestDto" />
    /// </summary>
    public class TestDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestDto"/> class.
        /// </summary>
        public TestDto()
        {
            this.TestResults = new HashSet<TestResultDto>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the TestResults
        /// </summary>
        public ICollection<TestResultDto> TestResults { get; set; }
    }
}
