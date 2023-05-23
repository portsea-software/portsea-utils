using System.IO;
using System.Linq;
using NUnit.Framework;

using MimeKit;
using Portsea.Utils.Net.Smtp;

namespace Portsea.Utils.Tests.Net.Smtp
{
    public class Tests
    {
        [Test]
        public void Build_Valid_Html_Mime_Message()
        {
            // Arrange
            BuildMessageRequest request = new BuildMessageRequest()
            {
                Email = "from@example.com",
                To = new string[] { "to@example.com" },
                HtmlBody = "<html><body><h1>Hello!</h1></body></html>",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.AreEqual(1, message.From.Count);
            Assert.AreEqual(1, message.To.Count);
            Assert.IsFalse(string.IsNullOrWhiteSpace(message.HtmlBody));
            Assert.IsTrue(string.IsNullOrWhiteSpace(message.TextBody));
        }

        [Test]
        public void Build_Valid_Text_Mime_Message()
        {
            // Arrange
            BuildMessageRequest request = new BuildMessageRequest()
            {
                Email = "from@example.com",
                To = new string[] { "to@example.com" },
                TextBody = "Hello!",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.AreEqual(1, message.From.Count);
            Assert.AreEqual(1, message.To.Count);
            Assert.IsTrue(string.IsNullOrWhiteSpace(message.HtmlBody));
            Assert.IsFalse(string.IsNullOrWhiteSpace(message.TextBody));
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Only_Cc_Recipients()
        {
            // Arrange
            BuildMessageRequest request = new BuildMessageRequest()
            {
                Email = "from@example.com",
                Cc = new string[] { "to@example.com" },
                TextBody = "Hello!",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.AreEqual(1, message.From.Count);
            Assert.AreEqual(1, message.Cc.Count);
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Only_Bcc_Recipients()
        {
            // Arrange
            BuildMessageRequest request = new BuildMessageRequest()
            {
                Email = "from@example.com",
                Bcc = new string[] { "to@example.com" },
                TextBody = "Hello!",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.AreEqual(1, message.From.Count);
            Assert.AreEqual(1, message.Bcc.Count);
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Attachments()
        {
            // Arrange
            string attachmentPath = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(attachmentPath, "Example attachment");
            BuildMessageRequest request = new BuildMessageRequest()
            {
                Email = "from@example.com",
                To = new string[] { "to@example.com" },
                HtmlBody = "<html><body><h1>Hello!</h1></body></html>",
                Attachments = new string[] { attachmentPath }
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.AreEqual(1, message.Attachments.Count());
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Explicit_ReplyTo_Address()
        {
            // Arrange
            BuildMessageRequest request = new BuildMessageRequest()
            {
                Email = "from@example.com",
                To = new string[] { "to@example.com" },
                ReplyTo = new string[] { "user1@example.com", "user2@example.com" },
                HtmlBody = "<html><body><h1>Hello!</h1></body></html>"
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.AreEqual(2, message.ReplyTo.Count);
        }
    }
}