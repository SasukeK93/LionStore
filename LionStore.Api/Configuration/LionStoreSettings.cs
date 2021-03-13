using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Configuration
{
    public class AppSettings
    {
        public Security Security { get; set; }
    }

    public class Security
    {
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Audience { get; set; }
        public string Authority { get; set; }
    }
}
