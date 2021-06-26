using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lib.Models
{
    [DynamoDBTable("teste_aws")]
    public class TesteModel
    {
        public string Id { get; set; }

        public string Texto { get; set; }

        public List<ItemModel> Itens { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}