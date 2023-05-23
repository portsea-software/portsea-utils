using System.Collections.Generic;
using System.Linq;

using MimeKit;

namespace Portsea.Utils.Net.Smtp
{
    internal static class MimeMessageExtensions
    {
        internal static string GetFromEmailAddress(this MimeMessage message)
        {
            if (!message.From.Mailboxes.Any())
            {
                return string.Empty;
            }

            return message.From.Mailboxes.First().Address;
        }

        internal static void AddFromAddress(this MimeMessage message, MailboxAddress address)
        {
            message.From.Add(address);
        }

        internal static void AddFromAddresses(this MimeMessage message, IEnumerable<string> addresses)
        {
            foreach (string address in addresses)
            {
                message.From.Add(address.ToMailboxAddress());
            }
        }

        internal static void AddReplyToAddress(this MimeMessage message, MailboxAddress address)
        {
            message.ReplyTo.Add(address);
        }

        internal static void AddReplyToAddresses(this MimeMessage message, IEnumerable<string> addresses)
        {
            foreach (string address in addresses)
            {
                message.ReplyTo.Add(address.ToMailboxAddress());
            }
        }

        internal static void AddToAddress(this MimeMessage message, MailboxAddress address)
        {
            message.To.Add(address);
        }

        internal static void AddToAddresses(this MimeMessage message, IEnumerable<string> addresses)
        {
            foreach (string address in addresses)
            {
                message.To.Add(address.ToMailboxAddress());
            }
        }

        internal static void AddCcAddress(this MimeMessage message, MailboxAddress address)
        {
            message.Cc.Add(address);
        }

        internal static void AddCcAddresses(this MimeMessage message, IEnumerable<string> addresses)
        {
            foreach (string address in addresses)
            {
                message.Cc.Add(address.ToMailboxAddress());
            }
        }

        internal static void AddBccAddress(this MimeMessage message, MailboxAddress address)
        {
            message.Bcc.Add(address);
        }

        internal static void AddBccAddresses(this MimeMessage message, IEnumerable<string> addresses)
        {
            foreach (string address in addresses)
            {
                message.Bcc.Add(address.ToMailboxAddress());
            }
        }

        internal static void AddRecipients(this MimeMessage message, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc)
        {
            message.AddToAddresses(to);
            message.AddCcAddresses(cc);
            message.AddBccAddresses(bcc);
        }

        internal static void AddRecipients(this MimeMessage message, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, IEnumerable<string> replyTo)
        {
            message.AddRecipients(to, cc, bcc);
            message.AddReplyToAddresses(replyTo);
        }

        internal static MailboxAddress ToMailboxAddress(this string address)
        {
            return new MailboxAddress(string.Empty, address);
        }
    }
}
