using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PatrITech.WeChat.Core;
using PatrITech.WeChat.Work.Api;
using PatrITech.WeChat.Work.Configuration;
using PatrITech.WeChat.Work.Extensions;
using PatrITech.WeChat.Work.Model;
using Refit;

namespace PatrITech.WeChat.Work
{
    public class CustomerService : SecuredServiceBase
    {
        private readonly ICustomerClient _customerClient;

        public CustomerService(IOptionsMonitor<WorkOptions> optionsAccessor, ITokenService tokenService) : base(optionsAccessor, tokenService)
        {
            AccessTokenType = AccessTokenType.Customer;

            _customerClient = RestService.For<ICustomerClient>(BaseUrl);
        }

        #region ListFollowUser
        protected async Task<(string[] userIdList, ResultState ResultState)> DoListFollowUser(IToken token)
        {
            var resp = await _customerClient.ListFollowUser(token.Token);
            return await resp.ReadAsResults<string>("$.follow_user[*]");
        }
        public Task<(string[] userIdList, ResultState ResultState)> ListFollowUser()
            => Invoke(t => DoListFollowUser(t), null);
        public Task<(string[] userIdList, ResultState ResultState)> ListFollowUser(string accountName)
            => Invoke(t => DoListFollowUser(t),accountName, null);
        public Task<(string[] userIdList, ResultState ResultState)> ListFollowUser((string corpId, string secret) credential)
            => Invoke(t => DoListFollowUser(t), credential);
        #endregion

        #region ListExternalUser
        protected async Task<(string[] ExternalUserIds, ResultState ResultState)> DoListExternalUser(IToken token, string userId)
        {

            var resp = await _customerClient.ListExternalContract(token.Token, userId);
            return await resp.ReadAsResults<string>("$.external_userid[*]");
        }
        public Task<(string[] ExternalUserIds, ResultState ResultState)> ListExternalUser(string userId)
            => Invoke(t => DoListExternalUser(t, userId),null);
        public Task<(string[] ExternalUserIds, ResultState ResultState)> ListExternalUser(string userId, string accountName)
            => Invoke(t => DoListExternalUser(t, userId), accountName, null);
        public Task<(string[] ExternalUserIds, ResultState ResultState)> ListExternalUser(string userId, (string corpId, string secret) credential)
            => Invoke(t => DoListExternalUser(t, userId), credential);
        #endregion
    }
}
