using Newtonsoft.Json;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class UpdateRemarkRequest
    {
        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }
    }
}
