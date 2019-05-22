using Newtonsoft.Json;
using PatrITech.WeChat.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class UserInfoResult:IRawResult
    {
        [JsonProperty(PropertyName = "subscribe")]
        public int Subscribe { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        [JsonProperty(PropertyName = "sex")]
        public int Gender { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "headimgurl")]
        public string HeadImgUrl { get; set; }

        [JsonProperty(PropertyName = "subscribe_time")]
        public long SubscribeTime { get; set; }

        [JsonProperty(PropertyName = "unionid")]
        public string UnionId { get; set; }

        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        [JsonProperty(PropertyName = "groupid")]
        public int GroupId { get; set; }

        [JsonProperty(PropertyName = "tagid_list")]
        public int[] TagIdList { get; set; }

        [JsonProperty(PropertyName = "subscribe_scene")]
        public string SubscribeScene { get; set; }

        [JsonProperty(PropertyName = "qr_scene")]
        public int QrScene { get; set; }

        [JsonProperty(PropertyName = "qr_scene_str")]
        public string QrSceneStr { get; set; }
        
        [JsonIgnore]
        public string Content { get; set; }
        [JsonIgnore]
        public byte[] SHA1 { get; set; }
    }
}
