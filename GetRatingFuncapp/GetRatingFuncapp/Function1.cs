using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Azure.Data.Tables;
using Azure;
using System.Collections.Concurrent;

namespace GetRatingFuncapp
{


  //  "id": "79c2779e-dd2e-43e8-803d-ecbebed8972c",
  //"userId": "cc20a6fb-a91f-4192-874d-132493685376",
  //"productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
  //"timestamp": "2018-05-21 21:27:47Z",
  //"locationName": "Sample ice cream shop",
  //"rating": 5,
  //"userNotes": "I love the subtle notes of orange in this ice cream!"

    public class Rating: ITableEntity
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string productId { get; set; }
        public int rating { get; set; }

        public string timestamp { get; set; }

        
        public string locationName { get; set; }
        public string userNotes { get; set; }

        public string PartitionKey { get; set ; }
        public string RowKey { get; set ; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set ; }
    }

    public static class GetRatingDTO
    {
        public static void InitGetRatingDTO()
        {
        }
        public static List<Rating> GetRatingList()
        {



            List<Rating> ratings = new List<Rating>();

            return ratings;
        }
    }

    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            //[Table("table_connection_string")] CloudTable cloudTable,           
            ILogger log)
        {

            string ratingId = req.Query["ratingId"];

            TableServiceClient tableServiceClient = new TableServiceClient(Environment.GetEnvironmentVariable("table_connection_string"));
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "ratings");

            await tableClient.CreateIfNotExistsAsync();

            Rating requestedRating = tableClient.GetEntity<Rating>("AllRows"
                , ratingId);


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            ratingId = ratingId ?? data?.name;

            string jsonString = System.Text.Json.JsonSerializer.Serialize(requestedRating);

            string responseMessage = string.IsNullOrEmpty(jsonString)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"{jsonString}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(jsonString);
        }
    }
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string userId = req.Query["userId"];

            List<Rating> ratings = new List<Rating>();
            foreach (Rating aRating in GetRatingDTO.GetRatingList())
            {
                if (aRating.userId == userId)
                {
                    Console.WriteLine(aRating);
                    ratings.Add(aRating);
                }
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            userId = userId ?? data?.name;

            string jsonString = System.Text.Json.JsonSerializer.Serialize(ratings);

            string responseMessage = string.IsNullOrEmpty(jsonString)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"{jsonString}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }


}
