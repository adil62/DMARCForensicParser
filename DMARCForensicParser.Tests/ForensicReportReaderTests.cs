using DMARCForensicParser;
using Xunit;

namespace DMARCForensicParser.Tests;

public class ForensicReportReaderTests
{
    private readonly ForensicReportReader _parser;

    public ForensicReportReaderTests()
    {
        _parser = new ForensicReportReader();
    }

    [Fact]
    public void ReadForensicReport_ValidReport_ParsesCorrectly()
    {
        string emlFilePath = Path.Combine("TestData", "email-1.eml");
        using var stream = new FileStream(emlFilePath, FileMode.Open, FileAccess.Read);

        var result = _parser.ReadForensicReport(stream);

        Assert.NotNull(result);
        Assert.Equal("feedback@mail.receiver.example", result.From);
        Assert.Equal("", result.Cc);
        Assert.Equal("2011-10-08T15:15:59", result.Date.ToString("yyyy-MM-ddTHH:mm:ss"));
        Assert.Equal("433689.81121.example@mta.mail.receiver.example", result.MessageId);
        Assert.NotNull(result.Headers);
        Assert.NotNull(result.FeedbackReport);
        Assert.Equal("mta1011.mail.tp2.receiver.example; dkim=fail (bodyhash) header.d=sender.example", result.FeedbackReport.AuthenticationResults);
        Assert.Equal("192.0.2.1", result.FeedbackReport.SourceIP);
    }
}
