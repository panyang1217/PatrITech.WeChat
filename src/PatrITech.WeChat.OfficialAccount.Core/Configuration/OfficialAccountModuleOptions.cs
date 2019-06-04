using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Configuration
{
    public class OfficialAccountModuleOptions
    {
        public class ClientOptions
        {
            public string AppId { get; set; }
            public string Secret { get; set; }

            public ClientOptions() { }
            public ClientOptions(string appId, string secret) {
                AppId = appId;
                Secret = secret;
            }
        }
        public string BaseUrl { get; set; }
        public string DefaultAccountName { get; set; }
        public Dictionary<string, ClientOptions> Accounts { get; set; } = new Dictionary<string, ClientOptions>();

        public void AddAccount(string accountName, string appId, string secret)
        {
            Accounts.Add(accountName, new ClientOptions(appId, secret));
        }
    }
}
