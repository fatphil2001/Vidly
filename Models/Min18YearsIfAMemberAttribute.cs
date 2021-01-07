using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Dtos;

namespace Vidly.Models
{
    public class Min18YearsIfAMemberAttribute : ValidationAttribute
    {

        private string _dependentProperty { get; set; }

        public Min18YearsIfAMemberAttribute(string dependentProperty)
        {
            this._dependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            System.Reflection.PropertyInfo field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = (byte?)field.GetValue(validationContext.ObjectInstance, null);
                if (dependentValue != null)
                {
                    return DoValidation(dependentValue.Value, (DateTime?)value);
                }
                return new ValidationResult("<Your message here>");
            }
            else
            {
                return new ValidationResult("<Your message here>");
            }

            //// My original validation code
            //var customer = validationContext.ObjectInstance as Customer;
            //if (customer != null)
            //    return DoValidation(customer.MembershipTypeId, customer.DateOfBirth);

            //var customerDto = validationContext.ObjectInstance as CustomerDto;
            //    return DoValidation(customerDto.MembershipTypeId, customerDto.DateOfBirth);
        }

        private ValidationResult DoValidation( int membershipTypeId, DateTime? DateOfBirth)
        {
            if (membershipTypeId == MembershipType.Unknown || membershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (DateOfBirth == null)
            {
                return new ValidationResult("Birth date is required.");
            }

            var age = CalculateAge(DateOfBirth.Value);
            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Must be at least 18 years old to become member.");

        }

        private int CalculateAge(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;

            if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day))//not had bday this year yet
                age--;

            return age;
        }
    }
}