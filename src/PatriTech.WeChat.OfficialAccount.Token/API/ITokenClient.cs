using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace PatrITech.WeChat.OfficialAccount.API
{
    interface ITokenClient
    {
        [Get("/cgi-bin/token?grant_type=client_credential")]
        Task<HttpResponseMessage> GetAccessToken(
            [AliasAs("appid")] string appId,
            [AliasAs("secret")] string appSecret);
    }
}
