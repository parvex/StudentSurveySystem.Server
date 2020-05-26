using System;
using FoolProof.Core;

namespace Server.Models.Survey
{
    public class ValidationConfig
    {
        public double? MinNumericValue { get; set; }
        [GreaterThan(nameof(MinNumericValue))]
        public double? MaxNumericValue { get; set; }
        public bool Integer { get; set; }
        public DateTime? MinDateValue { get; set; }
        [GreaterThan(nameof(MinDateValue))]
        public DateTime? MaxDateValue { get; set; }

        /// <summary>
        /// Regex for text field validation
        /// </summary>
        public string Regex { get; set; }
    }
}