using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount
{
    public interface IToken
    {
        string Token { get; }
        TimeSpan ExpiresIn { get; }
        DateTime CreateUtcTime { get; }
    }
}
