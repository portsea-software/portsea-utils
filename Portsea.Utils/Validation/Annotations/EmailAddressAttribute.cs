using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EmailAddressAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.ErrorMessage = string.Empty;

            if (value != null)
            {
                if (value is string)
                {
                    string emailAddress = value as string;
                    if (!EmailValidator.IsValid(emailAddress))
                    {
                        this.ErrorMessage += $"{emailAddress} is not a valid email address.";
                    }
                }
                else
                {
                    this.ErrorMessage = $"Email Addresses should be specified as string.  Object passed is of type {value.GetType().FullName}";
                }
            }

            if (!string.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
