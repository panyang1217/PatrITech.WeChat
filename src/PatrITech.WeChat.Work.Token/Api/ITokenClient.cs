using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.Work.Api
{
    interface ITokenClient
    {
        [Get("/cgi-bin/gettoken")]
        Task<HttpResponseMessage> GetAccessToken(
            [AliasAs("corpid")] string corpId,
            [AliasAs("corpsecret")] string corpSecret);
    }
}
