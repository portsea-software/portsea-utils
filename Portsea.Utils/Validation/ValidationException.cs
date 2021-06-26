using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation
{
    public class ValidationException : Exception
    {
        private const string DefaultErrorMessage = "Validation error.";

        public ValidationException(IEnumerable<ValidationResult> validationErrors)
        {
            this.ErrorMessage = DefaultErrorMessage;
            this.ValidationErrors = validationErrors;
        }

        public ValidationException(string errorMessage, IEnumerable<ValidationResult> validationErrors)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationErrors = validationErrors;
        }

        public string ErrorMessage { get; set; }

        public IEnumerable<ValidationResult> ValidationErrors { get; set; }
    }
}
