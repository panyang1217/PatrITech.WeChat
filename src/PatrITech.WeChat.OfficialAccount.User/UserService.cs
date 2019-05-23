using PatrITech.WeChat.OfficialAccount.Model;
using Refit;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PatrITech.WeChat.OfficialAccount.Configuration;
using PatrITech.WeChat.OfficialAccount.Api;
using System;

namespace PatrITech.WeChat.OfficialAccount
{
    public class UserService : SecuredServiceBase
    {
        private readonly IUserClient _userClient;

        public UserService(IOptionsMonitor<OfficialAccountOptions> optionsAccessor, ITokenService tokenService) :
            base(optionsAccessor, tokenService)
        {
            _userClient = RestService.For<IUserClient>(BaseUrl);
        }

        #region GetUsers
        protected virtual async Task<(UsersResult Users, ResultState ResultState)> DoGetUsers(IToken token, string nextOpenId)
        {
            var resp = await _userClient.GetUsers(token.Token, nextOpenId);
            return await resp.ReadAsResult<UsersResult>();
        }
        public Task<(UsersResult Users, ResultState ResultState)> GetUsers(string nextOpenId)
            => Invoke(token => DoGetUsers(token, nextOpenId));
        public Task<(UsersResult Users, ResultState ResultState)> GetUsers(string nextOpenId, string accountName)
            => Invoke(token => DoGetUsers(token, nextOpenId), accountName);
        public Task<(UsersResult Users, ResultState ResultState)> GetUsers(string nextOpenId, string appId, string secret)
            => Invoke(token => DoGetUsers(token, nextOpenId), appId, secret);
        #endregion

        #region GetUserInfo
        protected virtual async Task<(UserInfoResult UserInfo, ResultState ResultState)> DoGetUserInfo(IToken token, string openId, string lang)
        {
            var resp = await _userClient.GetUserInfo(token.Token, openId, lang);
            return await resp.ReadAsResult<UserInfoResult>();
        }
        public Task<(UserInfoResult UserInfo, ResultState ResultState)> GetUserInfo(
            string openId, string lang)
            => Invoke(token => DoGetUserInfo(token, openId, lang));
        public Task<(UserInfoResult UserInfo, ResultState ResultState)> GetUserInfo(
            string openId, string lang, string accountName)
            => Invoke(token => DoGetUserInfo(token, openId, lang), accountName);
        public Task<(UserInfoResult UserInfo, ResultState ResultState)> GetUserInfo(
            string openId, string lang, string appId, string secret)
            => Invoke(token => DoGetUserInfo(token, openId, lang), appId, secret);
        #endregion

        #region BatchGetUserInfo
        protected virtual async Task<(UserInfoResult[] userInfoList, ResultState ResultState)> DoBatchGetUserInfo(IToken token
            , BatchGetUsersRequest request)
        {
            var resp = await _userClient.BatchGetUserInfo(token.Token, request);
            return await resp.ReadAsResults<UserInfoResult>("$.user_info_list[*]");
        }
        public Task<(UserInfoResult[] userInfoList, ResultState ResultState)> BatchGetUserInfo(BatchGetUsersRequest request)
            => Invoke(token => DoBatchGetUserInfo(token, request));
        public Task<(UserInfoResult[] userInfoList, ResultState ResultState)> BatchGetUserInfo(BatchGetUsersRequest request
            , string accountName)
            => Invoke(token => DoBatchGetUserInfo(token, request), accountName);
        public Task<(UserInfoResult[] userInfoList, ResultState ResultState)> BatchGetUserInfo(BatchGetUsersRequest request
            , string appId, string secret)
            => Invoke(token => DoBatchGetUserInfo(token, request), appId, secret);
        #endregion

        #region UpdateRemark
        protected virtual async Task<ResultState> DoUpdateRemark(IToken token, UpdateRemarkRequest request)
        {
            var resp = await _userClient.UpdateRemark(token.Token, request);
            return await resp.ReadAsResult();
        }
        public Task<ResultState> UpdateRemark(UpdateRemarkRequest request)
            => Invoke(token => DoUpdateRemark(token, request));
        public Task<ResultState> UpdateRemark(UpdateRemarkRequest request, string accountName)
            => Invoke(token => DoUpdateRemark(token, request), accountName);
        public Task<ResultState> UpdateRemark(UpdateRemarkRequest request, string appId, string secret)
            => Invoke(token => DoUpdateRemark(token, request), appId, secret);
        #endregion

        #region CreateTag
        protected virtual async Task<(CreateTagResult Result, ResultState ResultStatue)> DoCreateTag(IToken token, string name)
        {
            var resp = await _userClient.CreateTag(token.Token, new CreateTagRequest(name));
            return await resp.ReadAsResult<CreateTagResult>();
        }
        public Task<(CreateTagResult Result, ResultState ResultStatue)> CreateTag(string name)
            => Invoke(token => DoCreateTag(token, name));
        public Task<(CreateTagResult Result, ResultState ResultStatue)> CreateTag(string name, string accountName)
            => Invoke(token => DoCreateTag(token, name), accountName);
        public Task<(CreateTagResult Result, ResultState ResultStatue)> CreateTag(string name, string appId, string secret)
            => Invoke(token => DoCreateTag(token, name), appId, secret);
        #endregion

        #region GetTags
        protected virtual async Task<(TagEntity[] Tags, ResultState ResultState)> DoGetTags(IToken token)
        {
            var resp = await _userClient.GetTags(token.Token);
            return await resp.ReadAsResults<TagEntity>("$.tags[*]");
        }
        public Task<(TagEntity[] Tags, ResultState ResultState)> GetTags()
            => Invoke(token => DoGetTags(token));
        public Task<(TagEntity[] Tags, ResultState ResultState)> GetTags(string accountName)
            => Invoke(token => DoGetTags(token), accountName);
        public Task<(TagEntity[] Tags, ResultState ResultState)> GetTags(string appId, string secret)
            => Invoke(token => DoGetTags(token), appId, secret);
        #endregion

        #region UpdateTag
        protected virtual async Task<ResultState> DoUpdateTag(IToken token, int id, string name)
        {
            var resp = await _userClient.UpdateTag(token.Token, new UpdateTagRequest(id, name));

            return await resp.ReadAsResult();
        }
        public Task<ResultState> UpdateTag(int id, string name)
            => Invoke(token => DoUpdateTag(token, id, name));
        public Task<ResultState> UpdateTag(int id, string name, string accountName)
           => Invoke(token => DoUpdateTag(token, id, name), accountName);
        public Task<ResultState> UpdateTag(int id, string name, string appId, string secret)
           => Invoke(token => DoUpdateTag(token, id, name), appId, secret);
        #endregion

        #region DeleteTag
        protected virtual async Task<ResultState> DoDeleteTag(IToken token, int id)
        {
            var resp = await _userClient.DeleteTag(token.Token, new DeleteTagRequest(id));
            return await resp.ReadAsResult();
        }
        public Task<ResultState> DeleteTag(int id)
            => Invoke(token => DoDeleteTag(token, id));
        public Task<ResultState> DeleteTag(int id, string accountName)
            => Invoke(token => DoDeleteTag(token, id), accountName);
        public Task<ResultState> DeleteTag(int id, string appId, string secret)
            => Invoke(token => DoDeleteTag(token, id), appId, secret);
        #endregion

        #region GetUsersWithTag
        protected virtual async Task<(UsersResult Users, ResultState ResultState)> DoGetUsersWithTag(IToken token, int tagId, string nextOpenId)
        {
            var resp = await _userClient.GetUsersWithTag(token.Token, new GetUsersWithTagRequest(tagId, nextOpenId));
            return await resp.ReadAsResult<UsersResult>();
        }
        public Task<(UsersResult Users, ResultState ResultState)> GetUsersWithTag(int tagId, string nextOpenId)
            => Invoke(token => DoGetUsersWithTag(token, tagId, nextOpenId));
        public Task<(UsersResult Users, ResultState ResultState)> GetUsersWithTag(int tagId, string nextOpenId, string accountName)
            => Invoke(token => DoGetUsersWithTag(token, tagId, nextOpenId), accountName);
        public Task<(UsersResult Users, ResultState ResultState)> GetUsersWithTag(int tagId, string nextOpenId, string appId, string secret)
            => Invoke(token => DoGetUsersWithTag(token, tagId, nextOpenId), appId, secret);
        #endregion

        #region BatchTagging
        protected virtual async Task<ResultState> DoBatchTagging(IToken token, int tagId, string[] openIds)
        {
            var resp = await _userClient.BatchTagging(token.Token,
                new BatchTaggingRequest(tagId,openIds));

            return await resp.ReadAsResult();
        }
        public Task<ResultState> BatchTagging(int tagId, string[] openIds)
            => Invoke(token => DoBatchTagging(token, tagId, openIds));
        public Task<ResultState> BatchTagging(int tagId, string[] openIds, string accountName)
            => Invoke(token => DoBatchTagging(token, tagId, openIds), accountName);
        public Task<ResultState> BatchTagging(int tagId, string[] openIds, string appId, string secret)
            => Invoke(token => DoBatchTagging(token, tagId, openIds), appId, secret);
        #endregion

        #region BatchUntagging
        protected virtual async Task<ResultState> DoBatchUntagging(IToken token, int tagId, string[] openIds)
        {
            var resp = await _userClient.BatchUntagging(token.Token,
                new BatchTaggingRequest(tagId, openIds));

            return await resp.ReadAsResult();
        }
        public Task<ResultState> BatchUntagging(int tagId, string[] openIds)
            => Invoke(token => DoBatchUntagging(token, tagId, openIds));
        public Task<ResultState> BatchUntagging(int tagId, string[] openIds, string accountName)
            => Invoke(token => DoBatchUntagging(token, tagId, openIds), accountName);
        public Task<ResultState> BatchUntagging(int tagId, string[] openIds, string appId, string secret)
            => Invoke(token => DoBatchUntagging(token, tagId, openIds), appId, secret);
        #endregion

        #region GetTagsByUser
        protected virtual async Task<(int[] TagIds, ResultState ResultState)> DoGetTagsByUser(IToken token, string openId)
        {
            var resp = await _userClient.GetTagsByUser(token.Token, new GetTagsByUserRequest(openId));
            return await resp.ReadAsResults<int>("$.tagid_list[*]");
        }
        public Task<(int[] TagIds, ResultState ResultState)> GetTagsByUser(string openId)
            => Invoke(token => DoGetTagsByUser(token, openId));
        public Task<(int[] TagIds, ResultState ResultState)> GetTagsByUser(string openId, string accountName)
            => Invoke(token => DoGetTagsByUser(token, openId), accountName);
        public Task<(int[] TagIds, ResultState ResultState)> GetTagsByUser(string openId, string appId, string secret)
            => Invoke(token => DoGetTagsByUser(token, openId), appId, secret);
        #endregion
    }
}
