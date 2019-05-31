using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.DependencyInjection
{
    public interface IOfficialAccountServiceBuilder
    {
        IServiceCollection Services { get; }
        IConfiguration Config { get; }
    }
}
