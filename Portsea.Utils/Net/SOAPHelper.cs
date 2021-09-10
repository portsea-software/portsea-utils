using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Xml;

namespace Portsea.Utils.Net
{
    public static class SOAPHelper
    {
        /// <summary>
        /// Sends a custom sync SOAP request to given URL and receive a request.
        /// </summary>
        /// <param name="url">The WebService endpoint URL.</param>
        /// <param name="action">The WebService action name.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="parameters">A dictionary containing the parameters in a key-value fashion.</param>
        /// <param name="soapAction">The SOAPAction value, as specified in the Web Service's WSDL (or NULL to use the url parameter).</param>
        /// <param name="useSOAP12">Set this to TRUE to use the SOAP v1.2 protocol, FALSE to use the SOAP v1.1 (default).</param>
        /// <returns>A string containing the raw Web Service response.</returns>
        public static string SendSOAPRequest(
            string url,
            string action,
            HttpMethod httpMethod,
            Dictionary<string, string> parameters,
            string soapAction = null,
            bool useSOAP12 = false)
        {
            XmlDocument soapEnvelope = GetSoapEnvelope(url, action, parameters, useSOAP12);
            HttpWebRequest request = GetWebRequest(url, httpMethod, soapAction, useSOAP12, soapEnvelope);

            string result;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    result = rd.ReadToEnd();
                }
            }

            return result;
        }

        private static XmlDocument GetSoapEnvelope(string url, string action, Dictionary<string, string> parameters, bool useSOAP12)
        {
            XmlDocument soapEnvelope = new XmlDocument();

            string xmlTemplate = GetSoapTemplate(useSOAP12);
            string @params = GetParametersAsString(parameters);

            string soapEnvelopeString = string.Format(xmlTemplate, action, new Uri(url).GetLeftPart(UriPartial.Authority) + "/", @params);
            soapEnvelope.LoadXml(soapEnvelopeString);

            return soapEnvelope;
        }

        private static HttpWebRequest GetWebRequest(string url, HttpMethod httpMethod, string soapAction, bool useSOAP12, XmlDocument soapEnvelope)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", soapAction ?? url);
            request.ContentType = useSOAP12 ? "application/soap+xml;charset=\"utf-8\"" : "text/xml;charset=\"utf-8\"";
            request.Accept = useSOAP12 ? "application/soap+xml" : "text/xml";
            request.Method = httpMethod.Method;

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelope.Save(stream);
            }

            return request;
        }

        private static string GetSoapTemplate(bool useSOAP12)
        {
            if (useSOAP12)
            {
                return
                    @"<?xml version=""1.0"" encoding=""utf-8""?>
                      <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                        xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                        xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                        <soap12:Body>
                          <{0} xmlns=""{1}"">{2}</{0}>
                        </soap12:Body>
                      </soap12:Envelope>";
            }

            return
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                  <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" 
                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                      <{0} xmlns=""{1}"">{2}</{0}>
                    </soap:Body>
                  </soap:Envelope>";
        }

        private static string GetParametersAsString(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return string.Empty;
            }

            return string.Join(string.Empty, parameters.Select(kv => string.Format("<{0}>{1}</{0}>", kv.Key, kv.Value)));
        }
    }
}
