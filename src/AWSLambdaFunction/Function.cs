using System;
using System.IO;
using System.Text;
using Lib.Repository;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Lib.Models;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaFunction
{
    public class Function
    {
        /// <summary>
        /// Function handler de eventos do dynamodb que consulta o Teste inserido e cria um objeto TestePos, 
        /// salva no dynamodb e insere na fila sqs e sns
        /// </summary>
        /// <param name="dynamoEvent"></param>
        /// <param name="context"></param>
        public async Task FunctionHandler(DynamoDBEvent dynamoEvent, ILambdaContext context)
        {
            foreach (var record in dynamoEvent.Records)
            {
                if (record.EventName == OperationType.INSERT)
                {
                    var teste = record.Dynamodb.NewImage.ToObject<TesteModel>();

                    var testepos = new TestePosModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        DataCriacao = DateTime.UtcNow,
                        Status = "OK",
                        Mensagem = "OK"
                    };

                    try
                    {
                        var testeget = await AWSRepository.GetAsync(teste.Id);
                        testepos.IdTeste = teste.Id;
                        testepos.Texto = teste.Texto + " | POS";

                        await AWSRepository.PutInSQS(testepos);

                        context.Logger.LogLine($"TestePos criado com sucesso | Id: {testepos.Id}, IdTeste: {teste.Id}");
                    }
                    catch (Exception ex)
                    {
                        testepos.Status = "NOK";
                        testepos.Mensagem = ex.Message;

                        await AWSRepository.PutInSNS(testepos);

                        context.Logger.LogLine($"Erro: {ex.Message} {ex.StackTrace}");
                    }

                    await AWSRepository.SaveAsync(testepos);
                }
            }
        }
    }
}