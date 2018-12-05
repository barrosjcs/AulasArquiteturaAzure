using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CognitiveService
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriptionKey = "002d9cf2be714679abeedb67f0bf137a";
            var imageUrl = "https://image.redbull.com/rbcom/010/2014-11-04/1331688446890_2/0010/1/1050/700/1/aurora-boreal-espet%C3%A1culo-da-natureza.jpg";

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey));
            var features = new List<VisualFeatureTypes>
            {
                VisualFeatureTypes.Faces,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Categories
            };

            client.Endpoint = "https://brazilsouth.api.cognitive.microsoft.com/";
            var result = client.AnalyzeImageAsync(imageUrl, features).Result;

            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.Read();
        }
    }
}
