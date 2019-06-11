using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Configuration
{
    public class WorkModuleOptions
    {
        public class ClientOptions
        {
            public string CorpId { get; set; }
            public string ContactServiceSecret { get; set; }
            public string CustomerServiceSecret { get; set; }
            public Dictionary<string, string> CustomizeApps { get; set; } = new Dictionary<string, string>();
            public Dictionary<string, string> ThirdPartApps { get; set; } = new Dictionary<string, string>();

            public void AddCustomizeApp(string appId, string secret)
                => CustomizeApps.Add(appId, secret);

            public void AddThirdPartApps(string appId, string secret)
                => ThirdPartApps.Add(appId, secret);
        }
        public string BaseUrl { get; set; }
        public string DefaultAccountName { get; set; }
        public Dictionary<string, ClientOptions> Accounts { get; set; } = new Dictionary<string, ClientOptions>();
        public ClientOptions GetAccountOrDefault(string accountName)
        {
            return Accounts[string.IsNullOrEmpty(accountName) ? DefaultAccountName : accountName];
        }

        public void AddAccount(string accountName, Action<ClientOptions> configAccount)
        {
            var account = new ClientOptions();
            configAccount(account);
            Accounts.Add(accountName, account);
        }
    }
}
