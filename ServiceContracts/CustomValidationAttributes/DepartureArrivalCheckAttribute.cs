using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CustomValidationAttributes
{
    public class DepartureArrivalCheckAttribute : ValidationAttribute
    {
        private readonly string arrivalPropertyName ;

        public DepartureArrivalCheckAttribute(string arrivalPropertyName)
        {
            this.arrivalPropertyName = arrivalPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return null;

            if (value is DateTime departureValue)
            {
                PropertyInfo? pi = validationContext.ObjectType.GetProperty(arrivalPropertyName);
                if (pi == null) return null;

                DateTime? arrivalValue = (DateTime?)pi.GetValue(validationContext.ObjectInstance);

                if (!arrivalValue.HasValue) { return null; }

                if (departureValue.CompareTo(arrivalValue.Value) < 0) { return ValidationResult.Success; }
                else { return new ValidationResult("Departure time must be earlier than arrival time."); }
            } else
            {
                return null;
            }
        }
    }
}
