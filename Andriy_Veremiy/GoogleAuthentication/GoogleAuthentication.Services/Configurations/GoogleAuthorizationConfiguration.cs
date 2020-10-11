using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAuthentication.Services.Configurations
{
    public class GoogleAuthorizationConfiguration
    {
        public string ClientSecretsFilePath { get; set; }

        public string ResponseTokenDirPath { get; set; }

        public IEnumerable<string> Scopes { get; set; }
    }
}
