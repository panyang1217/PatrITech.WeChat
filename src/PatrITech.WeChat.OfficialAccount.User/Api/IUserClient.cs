using PatrITech.WeChat.OfficialAccount.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.OfficialAccount.Api
{
    interface IUserClient
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140840"/>
        [Get("/cgi-bin/user/get")]
        Task<HttpResponseMessage> GetUsers(
            [AliasAs("access_token")][Query] string accessToken,
            [AliasAs("next_openid")][Query] string nextOpenId = null);

        /// <summary>
        /// 获取用户基本信息（包括UnionID机制）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140839"/>
        [Get("/cgi-bin/user/info")]
        Task<HttpResponseMessage> GetUserInfo(
            [AliasAs("access_token")][Query] string accessToken,
            [AliasAs("openid")][Query] string openId,
            [AliasAs("lang")][Query] string lang = "zh_CN");

        /// <summary>
        /// 批量获取用户基本信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [Post("/cgi-bin/user/info/batchget")]
        Task<HttpResponseMessage> BatchGetUserInfo(
            [AliasAs("access_token")][Query] string accessToken,
            [AliasAs("openid")][Body(BodySerializationMethod.Serialized)] BatchGetUsersRequest openId);

        /// <summary>
        /// 设置用户备注名
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140838"/>
        [Post("/cgi-bin/user/info/updateremark")]
        Task<HttpResponseMessage> UpdateRemark(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] UpdateRemarkRequest request);

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/tags/create")]
        Task<HttpResponseMessage> CreateTag(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] CreateTagRequest request);

        /// <summary>
        /// 获取公众号已创建的标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Get("/cgi-bin/tags/get")]
        Task<HttpResponseMessage> GetTags([AliasAs("access_token")][Query] string accessToken);

        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/tags/update")]
        Task<HttpResponseMessage> UpdateTag(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] UpdateTagRequest request);

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/tags/delete")]
        Task<HttpResponseMessage> DeleteTag(
             [AliasAs("access_token")][Query] string accessToken,
             [Body(BodySerializationMethod.Serialized)] DeleteTagRequest request);

        /// <summary>
        /// 获取标签下粉丝列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/user/tag/get")]
        Task<HttpResponseMessage> GetUsersWithTag(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] GetUsersWithTagRequest request);

        /// <summary>
        /// 批量为用户打标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/tags/members/batchtagging")]
        Task<HttpResponseMessage> BatchTagging(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] BatchTaggingRequest request);

        /// <summary>
        /// 批量为用户取消标签
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/tags/members/batchuntagging")]
        Task<HttpResponseMessage> BatchUntagging(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] BatchTaggingRequest request);

        /// <summary>
        /// 获取用户身上的标签列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <seealso cref="https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140837"/>
        [Post("/cgi-bin/tags/getidlist")]
        Task<HttpResponseMessage> GetTagsByUser(
            [AliasAs("access_token")][Query] string accessToken,
            [Body(BodySerializationMethod.Serialized)] GetTagsByUserRequest request);
    }
}
