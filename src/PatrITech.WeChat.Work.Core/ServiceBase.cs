using Microsoft.Extensions.Options;
using PatrITech.WeChat.Work.Configuration;
using System;
using System.Collections.Generic;

namespace PatrITech.WeChat.Work
{
    public abstract class ServiceBase
    {
        protected WorkModuleOptions Options { get; }
        public string BaseUrl { get => string.IsNullOrEmpty(Options?.BaseUrl) ? WorkConsts.ServiceUrl : Options.BaseUrl; }

        public ServiceBase(IOptionsMonitor<WorkModuleOptions> optionsAccessor)
        {
            Options = optionsAccessor.CurrentValue;
        }

        private string GetAccountName(string accountName)
        {
            if (string.IsNullOrEmpty(Options.DefaultAccountName))
                throw new Exception($"DefaultAccountName can not be null or empty");

            return string.IsNullOrEmpty(accountName) ? Options.DefaultAccountName : accountName;
        }

        private (string CorpId, string Secret) DoGetCredential(string accountName, Func<WorkModuleOptions.ClientOptions, string> getFunc)
        {
            if (!Options.Accounts.TryGetValue(GetAccountName(accountName), out var client))
                throw new Exception($"Invalid AccountName: {accountName}");

            return (client.CorpId, getFunc(client));
        }

        protected (string CorpId, string Secret) GetCredential(string accountName, AccessTokenType accessTokenType, string agentId)
        {
            try
            {
                switch (accessTokenType)
                {
                    case AccessTokenType.Contact:
                        return DoGetCredential(accountName, c => c.ContactServiceSecret);
                    case AccessTokenType.Customer:
                        return DoGetCredential(accountName, c => c.CustomerServiceSecret);
                    case AccessTokenType.CustomizeApp:
                        return DoGetCredential(accountName, c => c.CustomizeApps[agentId ?? throw new Exception("AgentId can not be null.")]);
                    case AccessTokenType.ThirdPartApp:
                        return DoGetCredential(accountName, c => c.ThirdPartApps[agentId ?? throw new Exception("AgentId can not be null.")]);
                    default:
                        throw new Exception($"Unknown AccessTokenType: {nameof(accessTokenType)}");
                }
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Invalid AgentId for \"{accountName}\": {agentId}");
            }
        }
    }
}
