using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAuthentication.Services.Configurations
{
    public class GoogleAuthorizationConfiguration
    {
        public string ClientSecretsFilePath { get; set; }

        public string ResponseTokenDirPath { get; set; }

        public string ResponseTokenFilePath { get; set; }

        public IEnumerable<string> Scopes { get; set; }

        public string User { get; set; }
    }
}
