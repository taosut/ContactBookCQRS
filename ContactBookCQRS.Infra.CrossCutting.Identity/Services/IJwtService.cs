using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookCQRS.Infra.CrossCutting.Identity.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwt(string email);
    }
}
