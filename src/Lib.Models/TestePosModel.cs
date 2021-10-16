using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib.Models
{
    [DynamoDBTable("teste_dynamodb_posinsert")]
    public class TestePosModel
    {
        public string Id { get; set; }

        public string IdTeste { get; set; }

        public string Texto { get; set; }

        public string Status { get; set; }
        
        public string Mensagem { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}