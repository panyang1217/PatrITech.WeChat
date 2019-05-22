using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.User;
using PatrITech.WeChat.OfficialAccount.User.Api;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PatrITech.WeChat.OfficialAccount.Tests
{
    public class UserTest : TestBase
    {
        public UserService UserService { get => Provider.GetService<UserService>(); }
        [Fact]
        public async void GetUsers_Test()
        {
            var result = await UserService.GetUsers(null);

            result.ResultState.Successed.ShouldBeTrue();
            result.Users.Data.OpenId.ShouldNotBeEmpty();
        }

        [Fact]
        public async void GetUserInfo_Test()
        {
            var result = await UserService.GetUsers(null);

            var openId = result.Users.Data.OpenId[0];

            var userInfoResult = await UserService.GetUserInfo(openId, "zh_CN");

            userInfoResult.ResultState.Successed.ShouldBeTrue();
            userInfoResult.UserInfo.Nickname.ShouldNotBeEmpty();
            userInfoResult.UserInfo.Content.ShouldNotBeEmpty();
            userInfoResult.UserInfo.SHA1.ShouldNotBeEmpty();
        }

        [Fact]
        public async void BatchGetUserInfo_Test()
        {
            var result = await UserService.GetUsers(null);

            var request = new BatchGetUsersRequest();
            request.UserList = result.Users.Data.OpenId.Select(id => new BatchGetUsersRequest.ListItem()
            {
                OpenId = id,
                Lang = "zh_CN"
            }).ToArray();

            var userInfoListResult = await UserService.BatchGetUserInfo(request);

            userInfoListResult.ResultState.Successed.ShouldBeTrue();
            userInfoListResult.userInfoList.ShouldNotBeEmpty();
            userInfoListResult.userInfoList[0].Nickname.ShouldNotBeNullOrEmpty();
            userInfoListResult.userInfoList[0].Content.ShouldNotBeNullOrEmpty();
            userInfoListResult.userInfoList[0].SHA1.ShouldNotBeEmpty();
        }

        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddOfficialAccount(config)
                .WithUserService();
        }
    }
}
