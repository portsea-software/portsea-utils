using System.IO;
using System.Linq;

using HtmlAgilityPack;
using Portsea.Utils.Net.Smtp;
using MimeKit;
using NUnit.Framework;
using System;

namespace Portsea.Utils.Tests.Net.Smtp
{
    public class Tests
    {
        [Test]
        public void Build_Valid_Html_Mime_Message()
        {
            // Arrange
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                HtmlBody = "<html><body><h1>Hello!</h1></body></html>",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.That(message.From.Count, Is.EqualTo(1));
            Assert.That(message.To.Count, Is.EqualTo(1));
            Assert.That(string.IsNullOrWhiteSpace(message.HtmlBody), Is.False);
            Assert.That(string.IsNullOrWhiteSpace(message.TextBody), Is.True);
        }

        [Test]
        public void Build_Valid_Text_Mime_Message()
        {
            // Arrange
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                TextBody = "Hello!",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.That(message.From.Count, Is.EqualTo(1));
            Assert.That(message.To.Count, Is.EqualTo(1));
            Assert.That(string.IsNullOrWhiteSpace(message.HtmlBody), Is.True);
            Assert.That(string.IsNullOrWhiteSpace(message.TextBody), Is.False);
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Only_Cc_Recipients()
        {
            // Arrange
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                Cc = ["to@example.com"],
                TextBody = "Hello!",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.That(message.From.Count, Is.EqualTo(1));
            Assert.That(message.Cc.Count, Is.EqualTo(1));
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Only_Bcc_Recipients()
        {
            // Arrange
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                Bcc = ["to@example.com"],
                TextBody = "Hello!",
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.That(message.From.Count, Is.EqualTo(1));
            Assert.That(message.Bcc.Count, Is.EqualTo(1));
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Attachments()
        {
            // Arrange
            string attachmentPath = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(attachmentPath, "Example attachment");
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                HtmlBody = "<html><body><h1>Hello!</h1></body></html>",
                Attachments = [attachmentPath]
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.That(message.Attachments.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Explicit_ReplyTo_Address()
        {
            // Arrange
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                ReplyTo = ["user1@example.com", "user2@example.com"],
                HtmlBody = "<html><body><h1>Hello!</h1></body></html>"
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            // Assert
            Assert.That(message.ReplyTo.Count, Is.EqualTo(2));
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Image_From_File_Path()
        {
            // Arrange
            string logoPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "logo.png");

            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                ReplyTo = ["user1@example.com", "user2@example.com"],
                HtmlBody = $"<html><body><h1>Hello!</h1><p><img src=\"{logoPath}\"></body></html>"
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(message.HtmlBody);
            string imgSrc = htmlDoc.DocumentNode.SelectSingleNode("//img[1]").GetAttributeValue("src", string.Empty);

            // Assert
            Assert.That(imgSrc.StartsWith("cid:"), Is.True);
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Image_From_File_Uri()
        {
            // Arrange
            string logoPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "logo.png");
            Uri logoUri = new(logoPath);

            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                ReplyTo = ["user1@example.com", "user2@example.com"],
                HtmlBody = $"<html><body><h1>Hello!</h1><p><img src=\"{logoUri.AbsoluteUri}\"></body></html>"
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(message.HtmlBody);
            string imgSrc = htmlDoc.DocumentNode.SelectSingleNode("//img[1]").GetAttributeValue("src", string.Empty);

            // Assert
            Assert.That(imgSrc.StartsWith("cid:"), Is.True);
        }

        [Test]
        public void Build_Valid_Mime_Message_With_Image_From_File_Path_Does_Not_Exist()
        {
            // Arrange
            string logoPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "nonexistingfile.png");
            
            BuildMessageRequest request = new()
            {
                Email = "from@example.com",
                To = ["to@example.com"],
                ReplyTo = ["user1@example.com", "user2@example.com"],
                HtmlBody = $"<html><body><h1>Hello!</h1><p><img src=\"{logoPath}\"></body></html>"
            };

            // Act
            MimeMessage message = MimeMessageBuilder.BuildMessage(request);

            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(message.HtmlBody);
            string imgSrc = htmlDoc.DocumentNode.SelectSingleNode("//img[1]").GetAttributeValue("src", string.Empty);

            // Assert - when the file path cannot be found the image source cannot be embedded so is left as is
            Assert.That(imgSrc, Is.EqualTo(logoPath));
        }
    }
}