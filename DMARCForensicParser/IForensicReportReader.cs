using System.IO;
using DMARCForensicParser.DTO;

namespace DMARCForensicParser;
public interface IForensicReportReader
{
        /// <summary>
        /// Reads a DMARC forensic report(rf 6591) from a given stream.
        /// The caller is responsible for disposing of the provided stream.
        /// </summary>
        /// <param name="stream">The stream containing the forensic report.</param>
        /// <returns>A Result object containing parsed forensic report details.</returns>
        Result ReadForensicReport(Stream stream);
}
