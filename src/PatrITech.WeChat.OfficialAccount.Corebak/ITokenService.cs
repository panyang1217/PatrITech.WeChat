using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.OfficialAccount
{
    public interface ITokenService
    {
        string GetAppId(string accountName);
        Task<IToken> GetAccessToken();
        Task<IToken> GetAccessToken(string accountName);
        Task<IToken> GetAccessToken(string appId, string secret);
    }
}
