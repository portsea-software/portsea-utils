using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation
{
    public class ValidationResultsException : Exception
    {
        private const string DefaultErrorMessage = "Validation error.";

        public ValidationResultsException(IEnumerable<ValidationResult> validationErrors)
        {
            this.ErrorMessage = DefaultErrorMessage;
            this.ValidationErrors = validationErrors;
        }

        public ValidationResultsException(string errorMessage, IEnumerable<ValidationResult> validationErrors)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationErrors = validationErrors;
        }

        public string ErrorMessage { get; set; }

        public IEnumerable<ValidationResult> ValidationErrors { get; set; }
    }
}
