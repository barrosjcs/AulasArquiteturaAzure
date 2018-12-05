﻿using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Newtonsoft.Json;
using System;

namespace BingResource
{
    class Program
    {
        static void Main(string[] args)
        {
            // Chave do Bing no Azure
            var subscriptionKey = "5783dbc5562e4297a79a3e9f2f0aca2c";

            Console.WriteLine("Digite o nome de uma personalidade");
            var nome = Console.ReadLine();

            var client = new WebSearchClient(new ApiKeyServiceClientCredentials(subscriptionKey));
                        
            var result = client.Web.SearchAsync(nome).Result;
            Console.WriteLine(JsonConvert.SerializeObject(result));
            Console.Read();
        }
    }
}
