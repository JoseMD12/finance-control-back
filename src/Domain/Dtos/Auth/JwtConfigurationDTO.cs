using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dtos.Auth
{
    public class JwtConfigurationDTO
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; }
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
