using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CopyDirectory.ConsoleApp.Extension
{
    public static class ValidatorExtension
    {
        public static bool ValidateObject<T>(this T obj, out ICollection<ValidationResult> results) where T : IValidatableObject
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
