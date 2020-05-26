using System;
using System.Text.RegularExpressions;
using Foolproof;

namespace Core.Models.Survey
{
    public class ValidationConfig
    {
        public double? MinNumericValue { get; set; }
        public double? MaxNumericValue { get; set; }
        public bool Integer { get; set; }

        public DateTime? MinDateValue { get; set; }
        [Greater]
        public DateTime? MaxDateValue { get; set; }

        /// <summary>
        /// Regex for text field validation
        /// </summary>
        public string Regex { get; set; }
    }
}