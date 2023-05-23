using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portsea.Utils.Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FilePathsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.ErrorMessage = string.Empty;

            if (value != null)
            {
                if (value is IEnumerable<string>)
                {
                    IEnumerable<string> files = value as IEnumerable<string>;

                    foreach (string file in files)
                    {
                        if (string.IsNullOrWhiteSpace(file) || !System.IO.File.Exists(file))
                        {
                            this.ErrorMessage += $"File does not exist: {file}\r\n";
                        }
                    }
                }
                else
                {
                    this.ErrorMessage =
                        $"File List should be specified as IEnumerable<string>.  Object passed is of type {value.GetType().FullName}";
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
