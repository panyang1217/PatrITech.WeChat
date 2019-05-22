using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.DependencyInjection
{
    public static class AccountServiceBuilderExtension
    {
        public static IOfficialAccountServiceBuilder WithMessageService(this IOfficialAccountServiceBuilder builder)
        {
            builder.Services.AddTransient<TemplateMessageService>();

            return builder;
        }
    }
}
