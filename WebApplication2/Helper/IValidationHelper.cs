using System;

namespace Helper
{
    public interface IValidationHelper
    {
        bool IsValidAge(int age, ref string validationMessage);

        bool IsValidDateOfBirth(DateTime dateOfBirth, ref string validationMessage);

        bool IsAgeOrDobSupplied(int? age, DateTime? dateOfBirth, ref string validationMessage);
    }
}