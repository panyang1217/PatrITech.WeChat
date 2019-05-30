using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PatrITech.WeChat.OfficialAccount.DependencyInjection;
using PatrITech.WeChat.OfficialAccount.Model;
using PatrITech.WeChat.Test;
using Shouldly;
using System;
using System.Linq;
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

        protected override void ConfigureService(IServiceCollection services, IConfiguration config)
        {
            services.AddOfficialAccount(config)
                .WithUserService();
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
            var (userList, state) = await UserService.GetUsers(null);

            var userInfoListResult = await UserService.BatchGetUserInfo(userList.Data.OpenId, "zh_CN");

            userInfoListResult.ResultState.Successed.ShouldBeTrue();
            userInfoListResult.userInfoList.ShouldNotBeEmpty();
            userInfoListResult.userInfoList[0].Nickname.ShouldNotBeNullOrEmpty();
            userInfoListResult.userInfoList[0].Content.ShouldNotBeNullOrEmpty();
            userInfoListResult.userInfoList[0].SHA1.ShouldNotBeEmpty();

            var obj = JsonConvert.DeserializeObject<UserInfoResult>(userInfoListResult.userInfoList[0].Content);
            obj.ShouldNotBeNull();
            obj.OpenId.ShouldBe(userInfoListResult.userInfoList[0].OpenId);
        }

        [Fact]
        public async void UpdateRemark_Test()
        {
            var expectedRemark = "Test Remark";
            var (userList, state) = await UserService.GetUsers(null);
            
            var userInfoListResult = await UserService.BatchGetUserInfo(userList.Data.OpenId, "zh_CN");
            userInfoListResult.ResultState.Successed.ShouldBeTrue();
            userInfoListResult.userInfoList[0].Remark.ShouldNotBe(expectedRemark);

            var updateRemarkResult = await UserService.UpdateRemark(new UpdateRemarkRequest()
            {
                OpenId = userInfoListResult.userInfoList[0].OpenId,
                Remark = expectedRemark
            });

            updateRemarkResult.Successed.ShouldBeTrue();

            userInfoListResult = await UserService.BatchGetUserInfo(userList.Data.OpenId, "zh_CN");
            userInfoListResult.ResultState.Successed.ShouldBeTrue();
            userInfoListResult.userInfoList[0].Remark.ShouldBe(expectedRemark);

            updateRemarkResult = await UserService.UpdateRemark(new UpdateRemarkRequest()
            {
                OpenId = userInfoListResult.userInfoList[0].OpenId,
                Remark = ""
            });

            updateRemarkResult.Successed.ShouldBeTrue();
        }

        [Fact]
        public async void CreateTag_Test()
        {
            string expectTagName = "test create";

            var result = await UserService.CreateTag(expectTagName);

            result.ResultStatue.Successed.ShouldBeTrue();
            var tag = result.Result.Tag;
            tag.Name.ShouldBe(expectTagName);
            tag.Id.HasValue.ShouldBeTrue();
            tag.Id.Value.ShouldBeGreaterThan(0);

            var deleteTagResult = await UserService.DeleteTag(tag.Id.Value);

            deleteTagResult.Successed.ShouldBeTrue();
        }

        [Fact]
        public async void GetTags_Test()
        {
            string expectTagName = "test get";

            await UserService.CreateTag(expectTagName);

            var result = await UserService.GetTags();

            result.ResultState.Successed.ShouldBeTrue();
            result.Tags.ShouldNotBeEmpty();
            result.Tags.Length.ShouldBeGreaterThan(0);
            var tag = result.Tags.Where(t => t.Name == expectTagName).Single();
            tag.Name.ShouldBe(expectTagName);
            tag.Id.HasValue.ShouldBeTrue();
            tag.Id.Value.ShouldBeGreaterThan(0);

            var deleteTagResult = await UserService.DeleteTag(tag.Id.Value);

            deleteTagResult.Successed.ShouldBeTrue();
        }

        [Fact]
        public async void UpdateTag_Test()
        {
            string originTagName = "origin test update";
            string expectedTagName = "expected test update";

            var createTagResult = await UserService.CreateTag(originTagName);
            var tag = createTagResult.Result.Tag;

            var updateTagResult = await UserService.UpdateTag(tag.Id.Value, expectedTagName);

            updateTagResult.Successed.ShouldBeTrue();

            var deleteTagResult = await UserService.DeleteTag(tag.Id.Value);

            deleteTagResult.Successed.ShouldBeTrue();
        }

        [Fact]
        public async void Tagging_Test()
        {
            const string userOpenId = "oYUL-54BgAKpjk_bmtwSeFtKs_Sc";

            var tagResult = await UserService.GetTags();
            var tag = tagResult.Tags[0];

            var taggingResult = await UserService.BatchTagging(tag.Id.Value, new string[] { userOpenId });

            taggingResult.Successed.ShouldBeTrue();

            var usersResult = await UserService.GetUsersWithTag(tag.Id.Value, null);

            usersResult.ResultState.Successed.ShouldBeTrue();
            usersResult.Users.Count.ShouldBeGreaterThan(0);
            usersResult.Users.Data.OpenId.Contains(userOpenId).ShouldBeTrue();

            var getTagsResult = await UserService.GetTagsByUser(userOpenId);

            getTagsResult.ResultState.Successed.ShouldBeTrue();
            getTagsResult.TagIds.ShouldNotBeNull();
            getTagsResult.TagIds.ShouldNotBeEmpty();
            getTagsResult.TagIds.Contains(tag.Id.Value).ShouldBeTrue();

            var untaggingResult = await UserService.BatchUntagging(tag.Id.Value, new string[] { userOpenId });

            untaggingResult.Successed.ShouldBeTrue();

            usersResult = await UserService.GetUsersWithTag(tag.Id.Value, null);

            usersResult.ResultState.Successed.ShouldBeTrue();
            usersResult.Users.Count.ShouldBe(0);

            getTagsResult = await UserService.GetTagsByUser(userOpenId);

            getTagsResult.ResultState.Successed.ShouldBeTrue();
            getTagsResult.TagIds.ShouldBeEmpty();
        }
    }
}
