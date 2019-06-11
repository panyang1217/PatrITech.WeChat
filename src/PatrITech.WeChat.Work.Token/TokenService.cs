using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PatrITech.WeChat.Core;
using PatrITech.WeChat.Work.Api;
using PatrITech.WeChat.Work.Configuration;
using PatrITech.WeChat.Work.Model;
using PatrITech.WeChat.Work.Extensions;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.Work
{
    public class TokenService : ServiceBase, ITokenService
    {
        private readonly ITokenClient _tokenClient;
        private readonly IDistributedCache _cache;

        public TokenService(IDistributedCache cache, IOptionsMonitor<WorkModuleOptions> option) : base(option)
        {
            _tokenClient = RestService.For<ITokenClient>(BaseUrl);
            _cache = cache;
        }

        public string GetCorpId(string accountName)
        {
            var account = Options.GetAccountOrDefault(accountName);
            return account.CorpId;
        }

        private string GetTokenCachingKey(string corpId, string secret)
            => corpId + secret;
        private bool IsExpired(IToken token)
            => token.CreateUtcTime + token.ExpiresIn > DateTime.UtcNow - TimeSpan.FromMinutes(-5);

        public async Task<(IToken Token, ResultState ResultState)> GetAccessToken(string corpId, string secret)
        {
            var cacheKey = GetTokenCachingKey(corpId, secret);
            var cacheToken = await _cache.GetStringAsync(cacheKey);

            if (cacheToken != null)
            {
                var token = JsonConvert.DeserializeObject<AccessToken>(cacheToken);

                if (IsExpired(token))
                    return (token, ResultState.OkFromCache);
            }

            var resp = await _tokenClient.GetAccessToken(corpId, secret);

            var result = await resp.ReadAsResult<AccessToken>();

            if (result.State.Successed)
            {
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result.Payload));

            }

            return result;
        }

        Task<IToken> ITokenService.GetAccessToken(AccessTokenType accessTokenType, string agentId)
        {
            (var corpId, var secret) = GetCredential(null, accessTokenType, agentId);
            return ((ITokenService)this).GetAccessToken(corpId, secret);
        }

        Task<IToken> ITokenService.GetAccessToken(string accountName, AccessTokenType accessTokenType, string agentId)
        {
            (var corpId, var secret) = GetCredential(accountName, accessTokenType, agentId);
            return ((ITokenService)this).GetAccessToken(corpId, secret);
        }

        async Task<IToken> ITokenService.GetAccessToken(string corpId, string secret)
        {
            (var accessToken, var resultState) = await GetAccessToken(corpId, secret);

            if (resultState.Successed)
                return accessToken;
            else
                throw new Exception($"Get access token error: {{code:{resultState.ErrorCode}, message:'{resultState.ErrorMessage}'}}");
        }
    }
}
