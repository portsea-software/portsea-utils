using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Portsea.Utils.Validation.Annotations;

namespace Portsea.Utils.Net.Smtp
{
    public class BuildMessageRequest
    {
        public string Subject { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public string HtmlBody { get; set; } = string.Empty;

        public string TextBody { get; set; } = string.Empty;

        public IEnumerable<string> To { get; set; } = Array.Empty<string>();

        public IEnumerable<string> Cc { get; set; } = Array.Empty<string>();

        public IEnumerable<string> Bcc { get; set; } = Array.Empty<string>();

        public IEnumerable<string> Attachments { get; set; } = Array.Empty<string>();

        [IsTrue]
        public bool HasBody
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.HtmlBody) ||
                       !string.IsNullOrWhiteSpace(this.TextBody);
            }
        }

        [IsTrue]
        public bool HasRecipients
        {
            get
            {
                return (this.To != null && this.To.Any()) ||
                       (this.Cc != null && this.Cc.Any()) ||
                       (this.Bcc != null && this.Bcc.Any());
            }
        }
    }
}
