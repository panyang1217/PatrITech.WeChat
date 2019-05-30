using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.DependencyInjection
{
    public static class WorkModuleServiceBuilderExtension
    {
        public static IWorkModuleServiceBuilder WithCustomerService(this IWorkModuleServiceBuilder builder)
        {
            builder.Services.AddTransient<CustomerService>();

            return builder;
        }
    }
}
