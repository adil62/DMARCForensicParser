using MimeKit;

namespace DMARCForensicParser {
    public class ForensicReportReader
    {
        public void ReadForensicReport(Stream stream)
        {
            var message = MimeMessage.Load(stream);

            
        }
    }
}
