using System;
using FoolProof.Core;
using Server.Helpers;

namespace Server.Models.Survey
{
    public class ValidationConfig
    {
        public double? MinNumericValue { get; set; }
        [GreaterThanOrNull(nameof(MinNumericValue), ErrorMessage = "Max validation value must be greater than min")]
        public double? MaxNumericValue { get; set; }
        public bool? Integer { get; set; }
        public DateTime? MinDateValue { get; set; }
        [GreaterThanOrNull(nameof(MinDateValue), ErrorMessage = "Max validation value must be greater than min")]
        public DateTime? MaxDateValue { get; set; }

        /// <summary>
        /// Regex for text field validation
        /// </summary>
        public string Regex { get; set; }
    }
}