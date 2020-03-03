namespace POSE.Services.Dtos
{
    using POSE.Domain;
    using System;

    /// <summary>
    /// Defines the <see cref="TestResultDto" />
    /// </summary>
    public class TestResultDto
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the TestName
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the PatientId
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// Gets or sets the Result
        /// </summary>
        public Result Result { get; set; } = ReturnRandomTestResult();

        /// <summary>
        /// The ReturnRandomTestResult
        /// </summary>
        /// <returns>The <see cref="Result"/></returns>
        private static Result ReturnRandomTestResult()
        {
            Random r = new Random();
            Array values = Enum.GetValues(typeof(Result));
            return (Result)values.GetValue(r.Next(values.Length));
        }
    }
}
