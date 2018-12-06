using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureSearch
{
    class IndexLetras
    {
        [Key]
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Id { get; set; }
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string NomeBanda { get; set; }
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Album { get; set; }
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        [IsSearchable]
        public string Letra { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new SearchServiceClient("teste-azuresearch-dois", 
                new SearchCredentials("4A91F9970AB73ADB4106D6BC56FD0EE2"));

            var index = client.Indexes.GetClient("index-bandas");
            var index2 = client.Indexes.Get("index-bandas");
            index2.Analyzers.Add(new PatternAnalyzer
            {
                Name = "custom",
                Pattern = @"||,||"
            });

            index2.Analyzers.Add(new CustomAnalyzer
            {
                Name = "custom",
                Tokenizer = TokenizerName.Standard,
                TokenFilters = new[] {
                    TokenFilterName.Phonetic,
                    TokenFilterName.Lowercase,
                    TokenFilterName.AsciiFolding
                }
            });

            index2.Fields[3].Analyzer = "custom";
            client.Indexes.CreateOrUpdate(index2, true);

            var batch = IndexBatch.Upload<IndexLetras>(new List<IndexLetras> {
                new IndexLetras
                {
                    Id = "rm331334",
                    Album = "Dark Side Of The Moon",
                    Letra = @"
                        What seems to be the trouble
                        Are we feeling out of sorts
                        Do you think they're gone forever
                        Can you show me where it hurts
                        I think I know what's wrong
                        A lot people get this way
                        Take two of these four times a day
                        I'll drop in when I pass this way again
                        There is no pain, you are receding
                        A distant ship smoke on the horizon
                        You are only coming through in waves
                        Your lips move but I can't hear what you're saying
                        When I was a child I had a fever
                        My hands felt just like two balloons
                        Now I've got that feeling once again
                        I can't explain, you would not understand
                        This is not how I am
                        I have become comfortably numb
                        You have to pull yourself together
                        You have to take it like a man
                        You mustn't let it get your head down
                        You should go out and do the town
                        There's no good moping here alone
                        Feeling sorry for yourself
                        Go and see what you can find
                        It's saturday night and you should unwind
                    ",
                    NomeBanda = "Pink Floyd"
                }
                });

            //index.Documents.Index(batch);
            //Console.WriteLine("Indexado");

            Console.WriteLine("Digite um termo para a busca");
            var term = Console.ReadLine();

            var result = index.Documents.Search<IndexLetras>(term, new SearchParameters
            {
                IncludeTotalResultCount = true
            });

            Console.WriteLine($"{result.Count} resultados encontrados");
            foreach (var item in result.Results)
            {
                Console.WriteLine($"{item.Document.Id} - {item.Document.NomeBanda}");
            }

            Console.Read();
        }
    }
}
