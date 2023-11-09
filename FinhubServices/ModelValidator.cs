using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesServices
{
    public class ModelValidatorResult
    {
        public bool Valid { get; set; }
        public IEnumerable<ValidationResult> Errors { get; set; } = Enumerable.Empty<ValidationResult>();

    }

    public static class ModelValidator
    {

        public static ModelValidatorResult ValidateModel(object? model, bool launchError = true)
        {
            if (model == null) { throw new ArgumentNullException(nameof(model)); }
            ValidationContext context = new ValidationContext(model);
            List<ValidationResult> result = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(model, context, result, true);
            if (!isValid && launchError)
            {
                throw new ArgumentException(String.Join(Environment.NewLine, result));
            }
            return new ModelValidatorResult() { Valid = isValid, Errors = result };
        }
    }
}
