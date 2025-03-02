using DMARCForensicParser;
using MimeKit;
using DMARCForensicParser.DTO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DMARCForensicParser {
    public class ForensicReportReader  
    {
        public Result ReadForensicReport(Stream stream)
        {
            MimeMessage message = MimeMessage.Load(stream);
            Result result = new Result();
            result.Cc = string.Join(", ", message.Cc.Select(a => ((MailboxAddress)a).Address));
            result.From = string.Join(", ", message.From.Select(a => ((MailboxAddress)a).Address));
            result.To = string.Join(", ", message.To.Select(a => ((MailboxAddress)a).Address));

            result.Date = message.Date.DateTime;
            result.MessageId = message.MessageId;

            foreach (var header in message.Headers) 
            {
                result.Headers[header.Id.ToString()] = header.Value;
            }

            if (message.BodyParts != null)
            {
                foreach (var bodyPart in message.BodyParts)
                {
                    if (bodyPart is MessagePart messagePart)
                    {
                        if (messagePart.ContentType.MimeType.Equals("message/rfc822") && messagePart.Message != null)
                        {
                            // Process Original email
                            RFC822 rfc822 = new RFC822();
                            rfc822.Bcc = string.Join(", ", messagePart.Message.Bcc.Select(a => ((MailboxAddress)a).Address));
                            rfc822.Cc =  string.Join(", ", messagePart.Message.Cc.Select(a => ((MailboxAddress)a).Address));
                            rfc822.From = string.Join(", ", messagePart.Message.From.Select(a => ((MailboxAddress)a).Address));
                            rfc822.ReplyTo = string.Join(", ", messagePart.Message.ReplyTo.Select(a => ((MailboxAddress)a).Address));
                            rfc822.To = string.Join(", ", messagePart.Message.To.Select(a => ((MailboxAddress)a).Address));
                            rfc822.ContentType = messagePart.ContentType.MimeType;
                            rfc822.DateTime = messagePart.Message.Date.DateTime;
                            rfc822.EmailTextBody = messagePart.Message.TextBody;
                            rfc822.InReplyTo = messagePart.Message.InReplyTo;
                            rfc822.MessageId = messagePart.Message.MessageId;
                            rfc822.Subject = messagePart.Message.Subject;
                            foreach (var header in messagePart.Message.Headers) 
                            {
                                rfc822.Headers[header.Id.ToString()] = header.Value;
                            }
                            result.OriginalEmail = rfc822;
                        }
                    }
                    else if (bodyPart is MimePart mimePart) 
                    {
                        using (var reader = new StreamReader(mimePart.Content.Open()))
                        {
                            string textContent = reader.ReadToEnd();
                            if (mimePart.ContentType.MimeType.Equals("text/plain"))
                            {
                                // Process email body
                                result.EmailTextBody = textContent;
                            } 
                            else if (mimePart.ContentType.MimeType.Equals("message/feedback-report"))
                            {
                                // Process feedback report
                                FeedbackReport feedbackReport = BuildFeedbackReport(textContent);
                                result.FeedbackReport = feedbackReport;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private FeedbackReport BuildFeedbackReport(string message) 
        {
            FeedbackReport feedbackReport = new FeedbackReport();

            using (var sr = new StringReader(message))
            {
                string line;
                string currentHeader = null;
                string currentValue = string.Empty;

                while ((line = sr.ReadLine()) != null) 
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    // Checks if continuation of the previous header
                    if (char.IsWhiteSpace(line[0]) && currentHeader != null)
                    {
                        currentValue += " " + line.Trim();
                    }
                    else
                    {
                        if (currentHeader != null)
                        {
                            AssignHeaderValue(feedbackReport, currentHeader, currentValue);
                        }

                        var parts = line.Split(new[] { ':' }, 2);
                        if (parts.Length == 2) 
                        {
                            currentHeader = parts[0].Trim();
                            currentValue = parts[1].Trim();
                        }
                    }
                }

                if (currentHeader != null)
                {
                    AssignHeaderValue(feedbackReport, currentHeader, currentValue);
                }
            }

            return feedbackReport;
        }

        private void AssignHeaderValue(FeedbackReport feedbackReport, string header, string value)
        {
            switch (header.ToLower()) 
            {
                case "user-agent":
                    feedbackReport.UserAgent = value;
                    break;
                case "version":
                    feedbackReport.Version = value;
                    break;
                case "original-mail-from":
                    feedbackReport.OriginalMailFrom = value;
                    break;
                case "original-envelope-id":
                    feedbackReport.OriginalEnvelopeId = value;
                    break;
                case "arrival-date":
                    value = Regex.Replace(value, @"\s*\(.*\)$", "").Trim();
                    string[] formats = { "ddd, d MMM yyyy HH:mm:ss zzz", "d MMM yyyy HH:mm:ss zzz" };
                    feedbackReport.ArrivalDate = DateTime.ParseExact(
                        value, formats, 
                        CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal
                    );
                    break;
                case "authentication-results":
                    feedbackReport.AuthenticationResults = value;
                    break;
                case "source-ip":
                    feedbackReport.SourceIP = value;
                    break;
                case "reported-domain":
                    feedbackReport.ReportedDomain = value;
                    break;
                case "reported-uri":
                    feedbackReport.ReportedURI = value;
                    break;
                case "delivery-result":
                    feedbackReport.DeliveryResult = value;
                    break;
                case "auth-failure":
                    feedbackReport.AuthFailure = value;
                    break;
                
                case "dkim-domain":
                    feedbackReport.DKIMDomain = value;
                    break;
                case "dkim-identity":
                    feedbackReport.DKIMIdentity = value;
                    break;
                case "dkim-selector":
                    feedbackReport.DKIMSelector = value;
                    break;
                case "dkim-canonicalized-header":
                    feedbackReport.DKIMCanonicalizedHeader = value;
                    break;
                case "dkim-canonicalized-body":
                    feedbackReport.DKIMCanonicalizedBody = value;
                    break;
                case "dkim-selector-dns":
                    feedbackReport.DKIMSelectorDNS = value;
                    break;

                case "spf-dns":
                    feedbackReport.SPFDNS = value;
                    break;
                
                default:
                    break;
            }
        }
    }
}
