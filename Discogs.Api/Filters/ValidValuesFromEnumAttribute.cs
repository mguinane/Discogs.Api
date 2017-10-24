using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discogs.Api.Filters
{
    public class ValidValuesFromEnumAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public ValidValuesFromEnumAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueString = value.ToString().ToLower();

            if (!Enum.GetNames(_enumType).Contains(valueString))
            {
                return new ValidationResult($"Invalid {_enumType.Name} value.");
            }

            return ValidationResult.Success;
        }
    }
}
