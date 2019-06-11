using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PatrITech.WeChat.Core;
using PatrITech.WeChat.Work.Configuration;
using PatrITech.WeChat.Work.Model;

namespace PatrITech.WeChat.Work
{
    public class SecuredServiceBase : ServiceBase
    {
        protected ITokenService TokenService { get; set; }

        protected AccessTokenType AccessTokenType { get; set; }

        public SecuredServiceBase(IOptionsMonitor<WorkModuleOptions> optionsAccessor
            , ITokenService tokenService) : base(optionsAccessor)
        {
            TokenService = tokenService;
        }

        public string GetCorpId(string accountName)
            => TokenService.GetCorpId(accountName);

        protected async Task<(T Payload, ResultState ResultState)> Invoke<T>(Func<IToken, Task<(T, ResultState)>> func, string agentId)
        {
            var token = await TokenService.GetAccessToken(AccessTokenType, agentId);

            return await func(token);
        }

        protected async Task<(T Payload, ResultState ResultState)> Invoke<T>(Func<IToken, Task<(T, ResultState)>> func, string accountName, string agentId)
        {
            var token = await TokenService.GetAccessToken(accountName, AccessTokenType, agentId);

            return await func(token);
        }

        protected async Task<(T Payload, ResultState ResultState)> Invoke<T>(Func<IToken, Task<(T, ResultState)>> func, (string corpId, string secret) credential)
        {
            var token = await TokenService.GetAccessToken(credential.corpId, credential.secret);

            return await func(token);
        }

        protected async Task<ResultState> Invoke(Func<IToken, Task<ResultState>> func, string agentId)
        {
            var token = await TokenService.GetAccessToken(AccessTokenType, agentId);

            return await func(token);
        }
        protected async Task<ResultState> Invoke(Func<IToken, Task<ResultState>> func, string accountName, string agentId)
        {
            var token = await TokenService.GetAccessToken(accountName, AccessTokenType, agentId);

            return await func(token);
        }
        protected async Task<ResultState> Invoke(Func<IToken, Task<ResultState>> func, (string corpId, string secret) credential)
        {
            var token = await TokenService.GetAccessToken(credential.corpId, credential.secret);

            return await func(token);
        }
    }
}
