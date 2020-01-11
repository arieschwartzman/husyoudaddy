
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace Husyoudaddy.Data
{
    public class CosmosTableService {
        private readonly CloudTableClient tableClient;

        public CosmosTableService(IConfiguration configation) {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(configation["CosmosDB:AccountName"], 
                        configation["tenant-table-primarykey"]), 
                        new Uri(configation["CosmosDB:Endpoint"]));
            tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        }

        public Task<TableQuerySegment<Tenant>> GetTenantByNameAsync(string name) {
            var table = tableClient.GetTableReference("tenants");
            TableContinuationToken token = null;
            TableQuery<Tenant> rangeQuery = new TableQuery<Tenant>().Where(
                    TableQuery.GenerateFilterCondition("name", QueryComparisons.Equal, name));
            return table.ExecuteQuerySegmentedAsync(rangeQuery, token);            
        }

    }

}