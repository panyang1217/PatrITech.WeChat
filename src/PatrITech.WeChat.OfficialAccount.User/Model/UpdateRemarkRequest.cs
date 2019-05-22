using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class UpdateRemarkRequest
    {
        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }
        [JsonProperty(PropertyName = "remark")]
        [MaxLength(30)]
        public string Remark { get; set; }
    }
}
