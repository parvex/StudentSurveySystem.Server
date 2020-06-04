using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Server.Helpers
{
    public class GreaterThanOrNullAttribute : ValidationAttribute
    {
        public GreaterThanOrNullAttribute(string otherProperty) : base("{0} must be greater than {1}")
        {
            OtherProperty = otherProperty;
        }

        public string OtherProperty { get; set; }

        public string FormatErrorMessage(string name, string otherName)
        {
            return string.Format(ErrorMessageString, name, otherName);
        }

        protected override ValidationResult
            IsValid(object firstValue, ValidationContext validationContext)
        {
            var firstComparable = firstValue as IComparable;
            var secondComparable = GetSecondComparable(validationContext);

            if (firstComparable != null && secondComparable != null)
            {
                if (firstComparable.CompareTo(secondComparable) >= 1) 
                    return ValidationResult.Success;
                var obj = validationContext.ObjectInstance;
                var thing = obj.GetType().GetProperty(OtherProperty);
                var displayName = thing.Name;
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName, displayName));
            }

            return ValidationResult.Success;
        }
        
        protected IComparable GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext
                .ObjectType
                .GetProperty(OtherProperty);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(
                    validationContext.ObjectInstance, null);
                return secondValue as IComparable;
            }

            return null;
        }
    }
}