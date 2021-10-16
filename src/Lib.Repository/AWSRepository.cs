using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
using Lib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lib.Repository
{
    public static class AWSRepository
    {
        public static async Task SaveAsync(this TesteModel teste)
        {
            var client = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var context = new DynamoDBContext(client);
            await context.SaveAsync(teste);
        }

        public static async Task SaveAsync(this TestePosModel testepos)
        {
            var client = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var context = new DynamoDBContext(client);
            await context.SaveAsync(testepos);
        }

        public static async Task<TesteModel> GetAsync(string Id)
        {
            var client = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var context = new DynamoDBContext(client);

            var request = new QueryRequest
            {
                TableName = "teste_dynamodb",
                KeyConditionExpression = "Id = :v_id",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> { { ":v_id", new AttributeValue { S = Id } } }
            };

            var response = await client.QueryAsync(request);
            var item = response.Items.FirstOrDefault();

            if (item == null) 
                return await Task.FromResult<TesteModel>(null);

            return await Task.FromResult<TesteModel>(item.ToObject<TesteModel>());
        }

        public static T ToObject<T>(this Dictionary<string, AttributeValue> dictionary)
        {
            var doc = Document.FromAttributeMap(dictionary);

            var client = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var context = new DynamoDBContext(client);

            return context.FromDocument<T>(doc);
        }
    
    
    
        public static async Task PutInSQS(TestePosModel testepos)
        {
            var message = JsonConvert.SerializeObject(testepos);

            var client = new AmazonSQSClient(RegionEndpoint.USEast2);

            var request = new SendMessageRequest
            {
                QueueUrl = "https://sqs.us-east-2.amazonaws.com/713434528261/teste_sqs",
                MessageBody = message
            };

            await client.SendMessageAsync(request);
        }

        public static async Task PutInSNS(TestePosModel testepos)
        {
            await Task.CompletedTask;
        }
    }
}
