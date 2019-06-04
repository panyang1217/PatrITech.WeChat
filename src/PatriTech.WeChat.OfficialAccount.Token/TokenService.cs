using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatrITech.WeChat.OfficialAccount.API;
using PatrITech.WeChat.OfficialAccount.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using PatrITech.WeChat.OfficialAccount.Configuration;
using Microsoft.Extensions.Options;
using PatrITech.WeChat.OfficialAccount.Extensions;

namespace PatrITech.WeChat.OfficialAccount
{
    public class TokenService : ServiceBase
        , ITokenService
    {
        private readonly ITokenClient _tokenClient;
        private readonly IDistributedCache _cache;

        public TokenService(IOptionsMonitor<OfficialAccountModuleOptions> optionsAccessor, IDistributedCache cache)
            : base(optionsAccessor)
        {
            _cache = cache;
            _tokenClient = Refit.RestService.For<ITokenClient>(BaseUrl);
        }

        public string GetAppId(string accountName)
        {
            var account = Options.Accounts[string.IsNullOrEmpty(accountName) ? Options.DefaultAccountName : accountName];
            return account.AppId;
        }

        public Task<(AccessToken AccessToken, ResultState ResultState)> GetAccessToken(string accountName)
        {
            var account = Options.Accounts[string.IsNullOrEmpty(accountName) ? Options.DefaultAccountName : accountName];

            return GetAccessToken(account.AppId, account.Secret);
        }

        public async Task<(AccessToken AccessToken, ResultState ResultState)> GetAccessToken(string appId, string appSecret)
        {
            var cacheToken = await _cache.GetStringAsync(appId);

            if (cacheToken != null)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(cacheToken);

                if (token.CreateUtcTime + token.ExpiresIn > DateTime.UtcNow - TimeSpan.FromMinutes(-5))
                    return (token, ResultState.OkFromCache);
            }

            var resp = await _tokenClient.GetAccessToken(appId, appSecret);

            var result = await resp.ReadAsResult<AccessToken>();

            if (result.State.Successed)
            {
                await _cache.SetStringAsync(appId, JsonConvert.SerializeObject(result.Payload));

            }

            return result;
        }

        async Task<IToken> ITokenService.GetAccessToken(string appId, string secret)
        {
            var result = await GetAccessToken(appId, secret);

            if (result.ResultState.Successed)
                return result.AccessToken;
            else
                throw new Exception($"Get access token error: {{code:{result.ResultState.ErrorCode}, message:'{result.ResultState.ErrorMessage}'}}");
        }

        async Task<IToken> ITokenService.GetAccessToken(string accountName)
        {
            var result = await GetAccessToken(accountName);

            if (result.ResultState.Successed)
                return result.AccessToken;
            else
                throw new Exception($"Get access token error: {{code:{result.ResultState.ErrorCode}, message:'{result.ResultState.ErrorMessage}'}}");
        }

        async Task<IToken> ITokenService.GetAccessToken()
        {
            var result = await GetAccessToken(null);

            if (result.ResultState.Successed)
                return result.AccessToken;
            else
                throw new Exception($"Get access token error: {{code:{result.ResultState.ErrorCode}, message:'{result.ResultState.ErrorMessage}'}}");
        }
    }
}
