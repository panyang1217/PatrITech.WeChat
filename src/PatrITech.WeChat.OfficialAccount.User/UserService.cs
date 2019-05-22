using PatrITech.WeChat.OfficialAccount.Model;
using PatrITech.WeChat.OfficialAccount.User.Api;
using PatrITech.WeChat.OfficialAccount.User.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using PatrITech.WeChat.OfficialAccount;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PatrITech.WeChat.OfficialAccount.Configuration;

namespace PatrITech.WeChat.OfficialAccount
{
    public class UserService : SecuredServiceBase
    {
        private readonly IUserClient _userClient;

        public UserService(IOptionsMonitor<OfficialAccountOptions> optionsAccessor, ITokenService tokenService) :
            base(optionsAccessor, tokenService)
        {
            _userClient = RestService.For<IUserClient>(BaseUrl);
        }

        protected virtual async Task<(UsersResult Users, ResultState ResultState)> DoGetUsers(IToken token, string nextOpenId)
        {
            var resp = await _userClient.GetUsers(token.Token, nextOpenId);
            return await resp.ReadAsResult<UsersResult>();
        }
        public Task<(UsersResult Users, ResultState ResultState)> GetUsers(string nextOpenId)
            =>Invoke(token => DoGetUsers(token, nextOpenId));
        public Task<(UsersResult Users, ResultState ResultState)> GetUsers(string nextOpenId, string accountName)
            => Invoke(token => DoGetUsers(token, nextOpenId), accountName);
        public Task<(UsersResult Users, ResultState ResultState)> GetUsers(string nextOpenId, string appId, string secret)
            => Invoke(token => DoGetUsers(token, nextOpenId), appId, secret);

        protected virtual async Task<(UserInfoResult UserInfo, ResultState ResultState)> DoGetUserInfo(IToken token, string openId, string lang)
        {
            var resp = await _userClient.GetUserInfo(token.Token, openId, lang);
            return await resp.ReadAsResult<UserInfoResult>();
        }
        public Task<(UserInfoResult UserInfo, ResultState ResultState)> GetUserInfo(
            string openId, string lang)
        	=> Invoke(token => DoGetUserInfo(token, openId, lang));
        public Task<(UserInfoResult UserInfo, ResultState ResultState)> GetUserInfo(
            string openId, string lang, string accountName)
        	=> Invoke(token => DoGetUserInfo(token, openId, lang), accountName);
        public Task<(UserInfoResult UserInfo, ResultState ResultState)> GetUserInfo(
            string openId, string lang, string appId, string secret)
        	=> Invoke(token => DoGetUserInfo(token, openId, lang), appId, secret);

        protected virtual async Task<(UserInfoResult[] userInfoList, ResultState ResultState)> DoBatchGetUserInfo(IToken token
            , BatchGetUsersRequest request)
        {
            var resp = await _userClient.BatchGetUserInfo(token.Token, request);
            return await resp.ReadAsResults<UserInfoResult>("$.user_info_list[*]");
        }
        public Task<(UserInfoResult[] userInfoList, ResultState ResultState)> BatchGetUserInfo(BatchGetUsersRequest request)
        	=> Invoke(token => DoBatchGetUserInfo(token, request));
        public Task<(UserInfoResult[] userInfoList, ResultState ResultState)> BatchGetUserInfo(BatchGetUsersRequest request
            , string accountName)
            => Invoke(token => DoBatchGetUserInfo(token, request), accountName);
        public Task<(UserInfoResult[] userInfoList, ResultState ResultState)> BatchGetUserInfo(BatchGetUsersRequest request
            , string appId, string secret)
            => Invoke(token => DoBatchGetUserInfo(token, request), appId, secret);
    }
}
