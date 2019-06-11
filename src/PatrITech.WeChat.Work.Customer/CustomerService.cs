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

        public CustomerService(IOptionsMonitor<WorkModuleOptions> optionsAccessor, ITokenService tokenService) : base(optionsAccessor, tokenService)
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

        #region ListExternalContact
        protected async Task<(string[] ExternalUserIds, ResultState ResultState)> DoListExternalContact(IToken token, string userId)
        {

            var resp = await _customerClient.ListExternalContact(token.Token, userId);
            return await resp.ReadAsResults<string>("$.external_userid[*]");
        }
        public Task<(string[] ExternalUserIds, ResultState ResultState)> ListExternalUser(string userId)
            => Invoke(t => DoListExternalContact(t, userId),null);
        public Task<(string[] ExternalUserIds, ResultState ResultState)> ListExternalUser(string userId, string accountName)
            => Invoke(t => DoListExternalContact(t, userId), accountName, null);
        public Task<(string[] ExternalUserIds, ResultState ResultState)> ListExternalUser(string userId, (string corpId, string secret) credential)
            => Invoke(t => DoListExternalContact(t, userId), credential);
        #endregion

        #region GetExternalContact
        protected async Task<(GetExternalContactResult Result, ResultState ResultState)> DoGetExternalContact(IToken token, string externalUserId)
        {
            var resp = await _customerClient.GetExternalContact(token.Token, externalUserId);
            return await resp.ReadAsResult<GetExternalContactResult>();
        }
        public Task<(GetExternalContactResult Result, ResultState ResultState)> GetExternalContact(string externalUserId)
            => Invoke(t => DoGetExternalContact(t, externalUserId), null);
        public Task<(GetExternalContactResult Result, ResultState ResultState)> GetExternalContact(string externalUserId, string accountName)
            => Invoke(t => DoGetExternalContact(t, externalUserId),accountName, null);
        public Task<(GetExternalContactResult Result, ResultState ResultState)> GetExternalContact(string externalUserId, (string corpId, string secret) credential)
            => Invoke(t => DoGetExternalContact(t, externalUserId), credential);

        #endregion
    }
}
