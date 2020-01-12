using Microsoft.Azure.Cosmos.Table;

namespace Husyoudaddy.Data
{
    public class Tenant : TableEntity
    {
        public string name {get;set;}
        public string email {get;set;}        
        public string friendly_name { get; set; }
    }
}