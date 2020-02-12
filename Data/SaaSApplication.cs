namespace Husyoudaddy.Data
{
    public class SaaSApplication
    {
        public string saasSubscriptionStatus { get; set; }
        public string name { get; set; }
        public Beneficiary beneficiary { get; set; }
    }

    public class Beneficiary
    {
        public string emailId { get; set; }
    }
}
    