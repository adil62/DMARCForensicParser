using DMARCForensicParser;
using Xunit;

namespace DMARCForensicParser.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Console.Write("AppDomain.CurrentDomain.BaseDirectory", AppDomain.CurrentDomain.BaseDirectory);
        // Arrange: Locate the test .eml file
        string emlFilePath = Path.Combine("TestData", "test.eml");

        using var stream = new FileStream(emlFilePath, FileMode.Open, FileAccess.Read);

        var parser = new ForensicReportReader();

        parser.ReadForensicReport(stream);

        Assert.True(true);
    }
}
