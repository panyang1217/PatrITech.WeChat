using PatrITech.WeChat.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.Work
{
    public interface ITokenService
    {
        string GetCorpId(string accountName);
        Task<IToken> GetAccessToken(AccessTokenType accessTokenType, string agentId);
        Task<IToken> GetAccessToken(string accountName, AccessTokenType accessTokenType, string agentId);
        Task<IToken> GetAccessToken(string corpId, string secret);
    }
}
