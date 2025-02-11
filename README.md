# DMARCForensicParser
## A .NET library for reading DMARC failure reports(rfc6591).
 
## Overview
`DMARCForensicParser` is a .NET library for parsing DMARC forensic reports. It extracts key details from reports, including report headers, body, feedback-report, the original emails headers and body.

## Features
- Parses forensic reports from email streams
- Extracts headers, body form the report email.
- Parses `message/feedback-report` (detail about the failed authentication)
- Parses `message/rfc822`(original email)

## Installation

To install the library in your .NET project, use:
```sh
Install-Package DMARCForensicParser
```

## Usage

### 1. Import the Library
```csharp
using DMARCForensicParser;
using System.IO;
```

### 2. Read and Parse a Forensic Report
```csharp
ForensicReportReader reader = new ForensicReportReader();
using (FileStream stream = File.OpenRead("forensic_report.eml"))
{
    Result result = reader.ReadForensicReport(stream);
    Console.WriteLine($"From: {result.From}");
    Console.WriteLine($"To: {result.To}");
    Console.WriteLine($"Date: {result.Date}");
    Console.WriteLine($"Report's email body: {result.EmailTextBody}");
    Console.WriteLine($"Authentication Results: {result.FeedbackReport.AuthenticationResults}");
    Console.WriteLine($"Source IP: {result.FeedbackReport.SourceIP}");
    Console.WriteLine($"Original email subject: {result.OriginalEmail.Subject}");
}
```

## Classes & Data Structures

TODO:

## License
This project is licensed under the MIT License.

## Contributing
Contributions are welcome! Please submit pull requests or report issues.

## Contact
For questions or support, open an issue on GitHub.
