using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesServiceContracts.ValidationAttributes
{
    public class MaxDateAttribute : ValidationAttribute
    {
        protected DateTime dateMax { get; set; } = DateTime.Parse("2000-01-01");
        protected string DefaultErrorMessage { get; set; } = "{0} should not be after {1}";
        protected readonly string DefaultMissingErrorMessage = "{0} can't be null or empty.";
        protected string MissingErrorMessage { get; set; }

        public MaxDateAttribute() {
            MissingErrorMessage = DefaultMissingErrorMessage;
        }

        public MaxDateAttribute(string dateMax, string? MissingErrorMsg = null) {
            this.dateMax = DateTime.Parse(dateMax);
            this.MissingErrorMessage = MissingErrorMsg ?? this.DefaultMissingErrorMessage;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult(String.Format(this.MissingErrorMessage, validationContext.DisplayName) ); }
            DateTime date = (DateTime) value;
            if (date > this.dateMax)
            {
                return new ValidationResult(
                    String.Format(ErrorMessage ?? this.DefaultErrorMessage,
                                    validationContext.DisplayName,
                                    this.dateMax) );
            }
            return ValidationResult.Success;
        }



    }
}
