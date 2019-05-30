using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Core
{
    public interface IToken
    {
        string Token { get; }
        TimeSpan ExpiresIn { get; }
        DateTime CreateUtcTime { get; }
    }
}
