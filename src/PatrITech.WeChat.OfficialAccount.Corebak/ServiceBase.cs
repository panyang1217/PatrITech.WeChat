using Microsoft.Extensions.Options;
using PatrITech.WeChat.OfficialAccount.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount
{
    public abstract class ServiceBase
    {
        protected OfficialAccountOptions Options { get; }
        public string BaseUrl { get => string.IsNullOrEmpty(Options?.BaseUrl) ? OfficialAccountConsts.ServiceUrl : Options.BaseUrl; }

        public ServiceBase(IOptionsMonitor<OfficialAccountOptions> optionsAccessor)
        {
            Options = optionsAccessor.CurrentValue;
        }

        protected (string AppId, string Secret) GetClientCredential(string accountName)
        {
            if (!Options.Accounts.TryGetValue(GetAccountName(accountName), out var client))
                throw new Exception($"Invalid AccountName: {accountName}");

            return (client.AppId, client.Secret);
        }

        private string GetAccountName(string accountName)
        {
            if (string.IsNullOrEmpty(Options.DefaultAccountName))
                throw new Exception($"DefaultAccountName can not be null or empty");

            return string.IsNullOrEmpty(accountName) ? Options.DefaultAccountName : accountName;
        }
    }
}
