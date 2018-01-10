using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace StorageQueueCoreSample
{
    /// <summary>
    /// Sample for emit queue for Storage Queue
    /// https://docs.microsoft.com/en-us/azure/visual-studio/vs-storage-aspnet5-getting-started-queues
    /// I added a Connected Service to my project according to the direction. 
    /// https://github.com/aspnet/Docs/tree/master/aspnetcore/fundamentals/configuration/index/sample/ConfigJson
    /// Sample
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            new Program().EmmitQueue().GetAwaiter().GetResult();

        }

        public static IConfigurationRoot Configuration { get; set; }

         private async Task EmmitQueue()
        {
            var builder = new ConfigurationBuilder()             
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var storageAccount = CloudStorageAccount.Parse(Configuration["ConnectionString"]);

            var queueClient = storageAccount.CreateCloudQueueClient();
            var messageQueue = queueClient.GetQueueReference("somequeue");
            await messageQueue.CreateIfNotExistsAsync();
            var message = new CloudQueueMessage("NiE ORAiNE LWA!!");
            await messageQueue.AddMessageAsync(message);
            Console.WriteLine("Done");
            Console.ReadLine();
        }


    }
}
