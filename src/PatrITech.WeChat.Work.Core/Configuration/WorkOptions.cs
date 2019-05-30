using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Configuration
{
    public class WorkOptions
    {
        public class ClientOptions
        {
            public string CorpId { get; set; }
            public string ContactServiceSecret { get; set; }
            public string CustomerServiceSecret { get; set; }
            public Dictionary<string, string> CustomizeApps { get; set; } = new Dictionary<string, string>();
            public Dictionary<string, string> ThirdPartApps { get; set; } = new Dictionary<string, string>();
        }
        public string BaseUrl { get; set; }
        public string DefaultAccountName { get; set; }
        public Dictionary<string, ClientOptions> Accounts { get; set; }
        public ClientOptions GetAccountOrDefault(string accountName)
        {
            return Accounts[string.IsNullOrEmpty(accountName) ? DefaultAccountName : accountName];
        }
    }
}
