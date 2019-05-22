using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PatrITech.WeChat.OfficialAccount.Configuration;
using PatrITech.WeChat.OfficialAccount.Model;

namespace PatrITech.WeChat.OfficialAccount
{
    public class SecuredServiceBase : ServiceBase
    {
        protected ITokenService TokenService { get; }
        public SecuredServiceBase(IOptionsMonitor<OfficialAccountOptions> optionsAccessor
            , ITokenService tokenService) : base(optionsAccessor)
        {
            TokenService = tokenService;
        }

        protected async Task<(T Payload, ResultState ResultState)> Invoke<T>(Func<IToken, Task<(T, ResultState)>> func)
        {
            var token = await TokenService.GetAccessToken();

            return await func(token);
        }

        protected async Task<(T Payload, ResultState ResultState)> Invoke<T>(Func<IToken, Task<(T, ResultState)>> func, string accountName)
        {
            var token = await TokenService.GetAccessToken(accountName);

            return await func(token);
        }

        protected async Task<(T Payload, ResultState ResultState)> Invoke<T>(Func<IToken, Task<(T, ResultState)>> func,string appId, string secret)
        {
            var token = await TokenService.GetAccessToken(appId, secret);

            return await func(token);
        }

        protected async Task<ResultState> Invoke(Func<IToken, Task<ResultState>> func)
        {
            var token = await TokenService.GetAccessToken();

            return await func(token);
        }
        protected async Task<ResultState> Invoke(Func<IToken, Task<ResultState>> func, string accountName)
        {
            var token = await TokenService.GetAccessToken(accountName);

            return await func(token);
        }
        protected async Task<ResultState> Invoke(Func<IToken, Task<ResultState>> func, string appId, string secret)
        {
            var token = await TokenService.GetAccessToken(appId, secret);

            return await func(token);
        }
    }
}
