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

### `Result`
Represents the output from the parser.
```csharp
public class Result 
{
    public string MessageId { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Cc { get; set; }
    public DateTime Date { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string EmailTextBody { get; set; } = "";
    public FeedbackReport FeedbackReport { get; set; }
    public RFC822 OriginalEmail { get; set; }
}
```

### `FeedbackReport`
Represents the `message/feedback-report` extracted from the forensic report.
```csharp
public class FeedbackReport
{
    public string UserAgent { get; set; }
    public string Version { get; set; }
    public string OriginalMailFrom { get; set; }
    public string OriginalEnvelopeId { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public string AuthenticationResults { get; set; }
    public string SourceIP { get; set; }
    public string ReportedDomain { get; set; }
    public string ReportedURI { get; set; }
    public string DeliveryResult { get; set; }
    public string AuthFailure { get; set; }
    public string DKIMDomain { get; set; }
    public string DKIMIdentity { get; set; }
    public string DKIMSelector { get; set; }
    public string DKIMCanonicalizedHeader { get; set; }
    public string DKIMCanonicalizedBody { get; set; }
    public string DKIMSelectorDNS { get; set; }
    public string SPFDNS { get; set; }
}
```

### `RFC822`
Represents the `rfc-822`(original email) details extracted from the forensic report.
```csharp
public class RFC822 
{
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string ContentType { get; set; }
    public string Subject { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string MessageId { get; set; }
    public string ReplyTo { get; set; }
    public string InReplyTo { get; set; }
    public DateTime DateTime { get; set; }
    public string EmailTextBody { get; set; }
}
```

Github: https://github.com/adil62/DMARCForensicParser