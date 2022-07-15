using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileCopy.Helpers
{
    public static class ValidatorExtension
    {
        public static bool Validate<T>(this T obj, out ICollection<ValidationResult> results) where T : IValidatableObject
        {
            var context = new ValidationContext(obj, null, null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                obj, context, results,
                validateAllProperties: true
            );
        }
    }
}
