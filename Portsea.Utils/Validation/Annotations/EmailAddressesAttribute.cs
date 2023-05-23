using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portsea.Utils.Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EmailAddressesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.ErrorMessage = string.Empty;

            // Email Address cannot be null
            if (value != null)
            {
                if (value is IEnumerable<string>)
                {
                    IEnumerable<string> emailAddresses = value as IEnumerable<string>;
                    foreach (string emailAddress in emailAddresses)
                    {
                        if (!EmailValidator.IsValid(emailAddress))
                        {
                            this.ErrorMessage += $"{emailAddress} is not a valid email address.\r\n";
                        }
                    }
                }
                else
                {
                    this.ErrorMessage =
                        $"Email Addresses should be specified as IEnumerable<string>.  Object passed is of type {value.GetType().FullName}";
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
