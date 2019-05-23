using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class CreateTagResult
    {
        [JsonProperty(PropertyName = "tag")]
        public TagEntity Tag { get; set; }
    }
}
