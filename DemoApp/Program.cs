using System.Dynamic;
using Microsoft.Azure.Cosmos;
using DemoApp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CosmosClient client = new(
    accountEndpoint: "https://mycosmos.documents.azure.com:443/",
    authKeyOrResourceToken: ""
);

Database database = client.GetDatabase(id: "DemoDatabase");
Container container = database.GetContainer(id: "Items");


var jsonFromWSA = """
{ "name"   : "John Smith",
  "sku"    : "20223",
  "price"  : 23.95,
  "shipTo" : { "name" : "Jane Smith",
               "address" : "123 Maple Street",
               "city" : "Pretendville",
               "state" : "NY",
               "zip"   : "12345" },
  "billTo" : { "name" : "John Smith",
               "address" : "123 Maple Street",
               "city" : "Pretendville",
               "state" : "NY",
               "zip"   : "12345" }
}
""";

var item = new Item();
item.originalMessage =  JsonConvert.DeserializeObject<ExpandoObject>(jsonFromWSA)!;

Item createdItem = await container.CreateItemAsync<Item>(
    item: item
);
Console.ReadKey();
// Partial update
var partialFromWSA = """
{
"price"  : 99.99
}
""";
var o1 = JObject.Parse(JsonConvert.SerializeObject(item.originalMessage));
var o2 =  JObject.Parse(partialFromWSA);

o1.Merge(o2, new JsonMergeSettings
{
    MergeArrayHandling = MergeArrayHandling.Union
});
createdItem.originalMessage = JsonConvert.DeserializeObject<ExpandoObject>(o1.ToString());
Item updatedItem = await container.UpsertItemAsync<Item>(
    item: createdItem
);
