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
        /// <summary>
        /// POST que insere o objeto Teste
        /// </summary>
        /// <param name="teste"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task PostAsync([FromBody] TesteModel teste)
        {
            teste.Id = Guid.NewGuid().ToString();
            teste.DataCriacao = DateTime.UtcNow;

            await teste.SaveAsync();

            Console.WriteLine($"Save Teste Model sucessfully. Id: {teste.Id}");
        }
    }
}
