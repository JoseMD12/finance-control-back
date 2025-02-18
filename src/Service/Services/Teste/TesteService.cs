using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interface.Services.Teste;

namespace Service.Services.Teste
{
    public class TesteService : ITesteService
    {
        public string GetHelloWorld()
        {
            return "Hello World!";
        }
    }
}