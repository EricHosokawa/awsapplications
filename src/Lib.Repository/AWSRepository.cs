using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Lib.Models;
using System;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public static class AWSRepository
    {
        public static async Task SalvarAsync(this TesteModel teste)
        {
            var client = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var context = new DynamoDBContext(client);
            await context.SaveAsync(teste);
        }
    }
}
