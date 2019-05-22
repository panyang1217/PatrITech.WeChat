using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Core
{
    public interface IRawResult
    {
        string Content { get; set; }
        byte[] SHA1 { get; set; }
    }
}
