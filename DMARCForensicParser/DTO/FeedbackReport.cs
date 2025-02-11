namespace DMARCForensicParser.DTO
{
    public class FeedbackReport
    {
        public string UserAgent {get; set;}    
        public string Version {get; set;}
        public string OriginalMailFrom {get; set;}
        public string OriginalEnvelopeId {get; set;}
        public DateTime? ArrivalDate {get; set;}
        public string AuthenticationResults {get; set;}
        public string SourceIP {get; set;} 
        public string ReportedDomain {get; set;} 
        public string ReportedURI {get; set;}
        public string DeliveryResult {get; set;} 
        public string AuthFailure {get; set;} 

        public string DKIMDomain { get; set; }
        public string DKIMIdentity { get; set; }
        public string DKIMSelector  { get; set; }
        public string DKIMCanonicalizedHeader { get; set; }
        public string DKIMCanonicalizedBody { get; set; }
        public string DKIMSelectorDNS { get; set; }

        public string SPFDNS { get; set; }
    }
}
