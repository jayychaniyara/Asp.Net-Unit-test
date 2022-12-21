using System;

namespace Helper
{
    public class ValidationHelper : IValidationHelper
    {
        private const int minAge = 18;
        private const int maxAge = 100;

        public bool IsValidAge(int age, ref string validationMessage)
        {
            if (age < minAge || age > maxAge)
            {
                validationMessage = $"Age must be within {minAge} to {maxAge}";
                return false;
            }
            return true;
        }

        public bool IsValidDateOfBirth(DateTime dateOfBirth, ref string validationMessage)
        {
            var minDate = CalculateMinDateOfBirthAllowed(maxAge);
            var maxDate = CalculateMaxDateOfBirthAllowed(minAge);
            if (dateOfBirth >= minDate && dateOfBirth <= maxDate)
            {
                return true;
            }
            if (dateOfBirth < minDate)
            {
                validationMessage = $"Max age should be {maxAge}";
            }
            if (dateOfBirth > maxDate)
            {
                validationMessage += $"Min age should be {minAge}";
            }
            return false;
        }

        public bool IsAgeOrDobSupplied(int? age, DateTime? dateOfBirth, ref string validationMessage)
        {
            if (!age.HasValue && !dateOfBirth.HasValue)
            {
                validationMessage = "Either Age or Date Of Birth is required.";
                return false;
            }

            return true;
        }

        // Make this method testable
        private DateTime CalculateMaxDateOfBirthAllowed(int minAge)
        {
            return DateTime.Today.AddYears(-minAge);
        }

        // Make this method testable
        private DateTime CalculateMinDateOfBirthAllowed(int maxAge)
        {
            return DateTime.Today.AddYears(-maxAge);
        }
    }
}
