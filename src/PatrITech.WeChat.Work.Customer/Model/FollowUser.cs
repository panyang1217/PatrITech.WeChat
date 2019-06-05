using Newtonsoft.Json;
using PatrITech.WeChat.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Model
{
    public class FollowUser
    {
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "createtime")]
        [JsonConverter(typeof(WeChatTimeCoverter))]
        public DateTime CreateTimeUTC { get; set; }
        //[JsonProperty(PropertyName = "createtime")]
        //public long CreateTimeUTC { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public UserTag[] Tags { get; set; }
        [JsonProperty(PropertyName = "remark_mobiles")]
        public string[] RemarkMobiles { get; set; }
    }
}
