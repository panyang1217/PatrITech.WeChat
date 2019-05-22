using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.OfficialAccount.User.Api
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
    }
}
