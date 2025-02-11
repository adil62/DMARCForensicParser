namespace DMARCForensicParser.DTO
{
    public class Result 
    {
        public string MessageId {get; set;}
        public string From {get; set;}
        public string To {get; set;}
        public string Cc {get; set;}
        public DateTime Date {get; set;}
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public string EmailTextBody {get; set;} = "";

        public FeedbackReport FeedbackReport {get; set;}

        public RFC822 OriginalEmail {get; set;}
    }
}
