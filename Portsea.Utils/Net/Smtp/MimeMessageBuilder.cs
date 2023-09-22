using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using MimeKit;
using MimeKit.Utils;

using Portsea.Utils.Validation;

namespace Portsea.Utils.Net.Smtp
{
    public static class MimeMessageBuilder
    {
        private static readonly Regex Base64EncodedImages = new Regex("(?:data:image)/(?<subMediaType>png|jpeg|gif)(?:;base64,)(?<base64>.*)");

        private static readonly IImageDownloader ImageDownloader = new ImageDownloader();

        public static MimeMessage BuildMessage(BuildMessageRequest request)
        {
            request.Validate();

            MimeMessage message = new MimeMessage()
            {
                Subject = request.Subject,
            };

            message.AddFromAddress(new MailboxAddress(request.DisplayName, request.Email));
            message.AddRecipients(request.To, request.Cc, request.Bcc, request.ReplyTo);
            message.Body = GetMessageBody(request.HtmlBody, request.TextBody, request.Attachments);

            return message;
        }

        private static MimeEntity GetMessageBody(string htmlBody, string textBody, IEnumerable<string> attachments)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            ProcessHtmlBody(bodyBuilder, htmlBody);
            ProcessTextBody(bodyBuilder, textBody);
            AddAttachments(bodyBuilder, attachments);

            return bodyBuilder.ToMessageBody();
        }

        private static void ProcessHtmlBody(this BodyBuilder bodyBuilder, string body)
        {
            if (!string.IsNullOrWhiteSpace(body))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                try
                {
                    doc.LoadHtml(body);

                    IDictionary<string, MimeEntity> imageSources = GetImageSources(doc);
                    if (imageSources.Any())
                    {
                        AddLinkedResources(bodyBuilder, imageSources.Values);
                        EmbedImages(doc, imageSources);
                    }

                    body = doc.DocumentNode.OuterHtml;
                }
                finally
                {
                    // worst case scenario send the body without processing images
                    bodyBuilder.HtmlBody = body;
                }
            }
        }

        private static void ProcessTextBody(BodyBuilder bodyBuilder, string body)
        {
            if (!string.IsNullOrWhiteSpace(body))
            {
                bodyBuilder.TextBody = body;
            }
        }

        private static void AddAttachments(BodyBuilder bodyBuilder, IEnumerable<string> attachments)
        {
            if (attachments != null)
            {
                foreach (string attachment in attachments)
                {
                    bodyBuilder.Attachments.Add(attachment);
                }
            }
        }

        private static void AddLinkedResources(BodyBuilder bodyBuilder, IEnumerable<MimeEntity> mimeEntities)
        {
            foreach (MimeEntity mimeEntity in mimeEntities)
            {
                bodyBuilder.LinkedResources.Add(mimeEntity);
            }
        }

        private static string EmbedImages(HtmlAgilityPack.HtmlDocument doc, IDictionary<string, MimeEntity> imageParts)
        {
            int elementCounter = 0;
            foreach (HtmlAgilityPack.HtmlNode img in doc.DocumentNode.SelectNodes("//img"))
            {
                elementCounter++;
                HtmlAgilityPack.HtmlAttribute srcAttribute = img.Attributes["src"];

                if (imageParts.ContainsKey(srcAttribute.Value))
                {
                    MimeEntity linkedResource = imageParts[srcAttribute.Value];
                    srcAttribute.Value = string.Format("cid:{0}", linkedResource.ContentId);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }

        private static IDictionary<string, MimeEntity> GetImageSources(HtmlAgilityPack.HtmlDocument doc)
        {
            Dictionary<string, MimeEntity> source2MimeParts = new Dictionary<string, MimeEntity>();

            HtmlAgilityPack.HtmlNodeCollection imgNodes = doc.DocumentNode.SelectNodes("//img");
            if (imgNodes != null)
            {
                foreach (HtmlAgilityPack.HtmlNode imgNode in imgNodes)
                {
                    HtmlAgilityPack.HtmlAttribute srcAttribute = imgNode.Attributes["src"];
                    if (srcAttribute != null)
                    {
                        Uri source = new Uri(srcAttribute.Value);
                        MimeEntity entity = null;
                        if (source.IsFile)
                        {
                            if (File.Exists(source.LocalPath))
                            {
                                entity = CreateImagePartFromFile(source.LocalPath);
                            }
                        }
                        else if (source.Scheme == Uri.UriSchemeHttp || source.Scheme == Uri.UriSchemeHttps)
                        {
                            entity = CreateImagePartFromWeb(source);
                        }
                        else if (Base64EncodedImages.IsMatch(srcAttribute.Value))
                        {
                            entity = CreateImagePartFromBase64(srcAttribute.Value);
                        }

                        if (entity != null)
                        {
                            source2MimeParts.Add(srcAttribute.Value, entity);
                        }
                    }
                }
            }

            return source2MimeParts;
        }

        private static MimeEntity CreateImagePartFromFile(string source)
        {
            try
            {
                ContentType contentType = new ContentType("image", GetExtensionWithoutPeriod(source));
                Stream stream = File.OpenRead(source);
                MimeEntity mimePart = MimeEntity.Load(contentType, stream);
                mimePart.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                mimePart.ContentId = MimeUtils.GenerateMessageId();

                return mimePart;
            }
            catch
            {
                return null;
            }
        }

        private static MimeEntity CreateImagePartFromWeb(Uri source)
        {
            try
            {
                ContentType contentType = new ContentType("image", GetExtensionWithoutPeriod(source.AbsolutePath));

                byte[] imageData = ImageDownloader.DownloadImageBytes(source);

                MemoryStream stream = new MemoryStream(imageData);
                MimeEntity mimePart = MimeEntity.Load(contentType, stream);
                mimePart.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                mimePart.ContentId = MimeUtils.GenerateMessageId();

                return mimePart;
            }
            catch
            {
                return null;
            }
        }

        private static MimeEntity CreateImagePartFromBase64(string source)
        {
            try
            {
                Match m = Base64EncodedImages.Matches(source)[0];
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(m.Groups["base64"].Value));
                string subMediaType = m.Groups["subMediaType"].Value;

                ContentType contentType = new ContentType("image", subMediaType);
                MimeEntity mimePart = MimeEntity.Load(contentType, stream);
                mimePart.ContentDisposition = new ContentDisposition(ContentDisposition.Inline);
                mimePart.ContentId = MimeUtils.GenerateMessageId();

                return mimePart;
            }
            catch
            {
                return null;
            }
        }

        private static string GetExtensionWithoutPeriod(string filename)
        {
            return Path.GetExtension(filename).Substring(1);
        }
    }
}
