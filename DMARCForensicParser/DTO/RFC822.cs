namespace DMARCForensicParser.DTO
{
  public class RFC822 
  {
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string ContentType {get; set;} 
    public string Subject {get; set;} 
    public string Cc {get; set;} 
    public string Bcc {get; set;} 
    public string From {get; set;}
    public string To {get; set;}
    public string MessageId {get; set;} 
    public string ReplyTo {get; set;} 
    public string InReplyTo {get; set;} 
    public DateTime DateTime {get; set;} 
    public string EmailTextBody {get; set;} 
  }
}
