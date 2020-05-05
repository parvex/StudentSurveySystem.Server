using System;
using System.Text.RegularExpressions;

namespace Core.Models.Survey
{
    public class ValidationConfig
    {
        public int? MinNumericValue { get; set; }
        public int? MaxNumericValue { get; set; }

        public DateTime? MinDateValue { get; set; }
        public DateTime? MaxDateValue { get; set; }

        /// <summary>
        /// Regex for text field validation
        /// </summary>
        public string Regex { get; set; }
    }
}