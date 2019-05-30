using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.Test;
using PatrITech.WeChat.Work.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PatrITech.WeChat.Work.Tests
{
    public class CustomerTests : TestBase
    {
        public CustomerService CustomerService { get => Provider.GetService<CustomerService>(); }

        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddWorkModule(config)
                .WithCustomerService();
        }

        [Fact(DisplayName = "获取配置了客户联系功能的成员列表")]
        public async void ListFollowUser_Test()
        {
            (var userIdList, var state) = await CustomerService.ListFollowUser();

            state.Successed.ShouldBeTrue();
            userIdList.ShouldNotBeEmpty();
            userIdList.Length.ShouldBeGreaterThan(0);
        }

        [Fact(DisplayName = "获取外部联系人列表")]
        public async void ListExternalContact_Test()
        {
            (var userIdList, _) = await CustomerService.ListFollowUser();

            var userId = userIdList[0];

            (var exUserIdList, var state) = await CustomerService.ListExternalUser(userId);

            Assert.True(state.Successed || state.ErrorCode == 84061, $"ListExternalUser Error: {{ErrorCode: {state.ErrorCode}, ErrorMessage: {state.ErrorMessage}}}");
            if (state.Successed)
            {
                exUserIdList.ShouldNotBeEmpty();
                exUserIdList.Length.ShouldBeGreaterThan(0);
            }
        }
    }
}
