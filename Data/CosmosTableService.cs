
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

        public async Task<TableResult> GetTenantByIdAsync(string rowkey)
        {
            var table = tableClient.GetTableReference("tenants");
            return await table.ExecuteAsync(TableOperation.Retrieve<Tenant>("tenants", rowkey));
            
        }

        public async Task<List<Tenant>> GetAllTenantsAsync()
        {
            var table = tableClient.GetTableReference("tenants");
            TableContinuationToken token = null;
            List<Tenant> list = new List<Tenant>();

            do
            {
                TableQuery<Tenant> rangeQuery = new TableQuery<Tenant>().Where(
                        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "tenants"));
                TableQuerySegment<Tenant> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
                token = segment.ContinuationToken;
                list.AddRange(segment.Results);

            } while (token != null);

            return list;
        
        }

        public async Task<List<Scenario>> GetAllScenariosAsync(string tenantId)
        {
          var table = tableClient.GetTableReference("scenarios");
          TableContinuationToken token = null;
          List<Scenario> list = new List<Scenario>();

          do
          {
            TableQuery<Scenario> rangeQuery = new TableQuery<Scenario>().Where(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, tenantId));
            TableQuerySegment<Scenario> segment = await table.ExecuteQuerySegmentedAsync(rangeQuery, token);
            token = segment.ContinuationToken;
            list.AddRange(segment.Results);

          } while (token != null);

          return list;

        }

    }

}