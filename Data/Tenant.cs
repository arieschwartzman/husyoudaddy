using Microsoft.Azure.Cosmos.Table;

namespace Husyoudaddy.Data
{
    public class Tenant : TableEntity
    {
        public string name { get; set; }
        public string email { get; set; }
        public string friendly_name { get; set; }
        public string saasSubscriptionId { get; set; }
        public int maxMessages { get; set; }
        public int msgCount { get; set; }
        public string app_id {get;set;}
        public string app_secret {get;set;}
        public string botName { get; set; }
        public string webchat_secret { get; set; }
    }
}