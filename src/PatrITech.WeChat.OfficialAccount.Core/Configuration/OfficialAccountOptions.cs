using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Configuration
{
    public class OfficialAccountOptions
    {
        public class ClientOptions
        {
            public string AppId { get; set; }
            public string Secret { get; set; }
        }
        public string BaseUrl { get; set; }
        public string DefaultAccountName { get; set; }
        public Dictionary<string, ClientOptions> Accounts { get; set; }
    }
}
