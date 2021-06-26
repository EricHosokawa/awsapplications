using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.Models;
using Lib.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWSServerlessApp.Controllers
{
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        // POST: api/Teste
        [HttpPost]
        public async Task PostAsync([FromBody] TesteModel teste)
        {
            teste.Id = Guid.NewGuid().ToString();
            teste.DataCriacao = DateTime.UtcNow;

            await teste.SalvarAsync();

            Console.WriteLine($"Save Teste Model sucessfully. Id: {teste.Id}");
        }
    }
}
