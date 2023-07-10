using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChangeFeedFunction
{
    
    public static class ChangeFeedProcessor
    {
        
        [FunctionName("ChangeFeedProcessor")]
        public static void Run(
            //change database name below if different than specified in the lab
            [CosmosDBTrigger(
                databaseName: "DemoDatabase",
                containerName: "Items",
                Connection  = "DBconnection",
                LeaseContainerName  = "leases",
                CreateLeaseContainerIfNotExists  = true)]IReadOnlyList<Item> documents, ILogger log)
        {
            foreach (var doc in documents)
            {
                Console.WriteLine(doc.originalMessage);
            }
   

        }
        
    }
}
