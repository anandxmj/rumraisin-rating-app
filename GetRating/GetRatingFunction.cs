using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace GetRating
{

    public class Rating
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int RatingVal { get; set; }

        public string LocationName { get; set; }
        public string UserNotes { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public static class GetRatingDTO
    {
        public static void InitGetRatingDTO()
        {
        }
        public static List<Rating> GetRatingList()
        {



            List<Rating> ratings = new List<Rating>();


            ratings.Add(new Rating() { 
                Id = "79c2779e-dd2e-43e8-803d-ecbebed8972c", 
                UserId = "cc20a6fb-a91f-4192-874d-132493685376",
                ProductId= "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
                LocationName= "Sample ice cream shop",
                UserNotes= "I love the subtle notes of orange in this ice cream!",
                TimeStamp= DateTime.UtcNow,
                RatingVal = 5
            });

            ratings.Add(new Rating()
            {
                Id = "8947f7cc-6f4c-49ed-a7aa-62892eac8f31",
                UserId = "cc20a6fb-a91f-4192-874d-132493685376",
                ProductId = "e4e7068e-500e-4a00-8be4-630d4594735b",
                LocationName = "Another Sample Shop",
                UserNotes = "I really enjoy this grape ice cream!",
                TimeStamp = DateTime.UtcNow,
                RatingVal = 4
            });


            ratings.Add(new Rating()
            {
                Id = "1",
                UserId = "1",
                ProductId = "1",
                LocationName = "Another Sample Shop",
                UserNotes = "I really enjoy this grape ice cream!",
                TimeStamp = DateTime.UtcNow,
                RatingVal = 1
            });

            ratings.Add(new Rating()
            {
                Id = "2",
                UserId = "1",
                ProductId = "1",
                LocationName = "Another Sample Shop",
                UserNotes = "I really enjoy this grape ice cream!",
                TimeStamp = DateTime.UtcNow,
                RatingVal = 2
            });

            ratings.Add(new Rating()
            {
                Id = "3",
                UserId = "1",
                ProductId = "1",
                LocationName = "Dummy Location",
                UserNotes = "I love ice cream!",
                TimeStamp = DateTime.UtcNow,
                RatingVal = 3
            });

            return ratings;
        }
    }

    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            //        public static string connectionstring = System.Environment.GetEnvironmentVariable("table_connection_string", EnvironmentVariableTarget.Process);

            //string connectionstr = connectionstring;
            string ratingId = req.Query["ratingId"];

            Rating requestedRating=new Rating();
            foreach (Rating aRating in GetRatingDTO.GetRatingList())
            {
                if(aRating.Id == ratingId)
                {
                    Console.WriteLine(aRating);
                    requestedRating = aRating;
                }
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            ratingId = ratingId ?? data?.name;

            string jsonString = System.Text.Json.JsonSerializer.Serialize(requestedRating);

            string responseMessage = string.IsNullOrEmpty(jsonString)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"{jsonString}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
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
                if (aRating.UserId == userId)
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
