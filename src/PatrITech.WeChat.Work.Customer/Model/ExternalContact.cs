using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Model
{
    public class ExternalContact
    {
        [JsonProperty(PropertyName = "external_userid")]
        public string ExternalUserId { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }
        [JsonProperty(PropertyName = "gender")]
        public int Gender { get; set; }
        [JsonProperty(PropertyName = "unionid")]
        public string UnionId { get; set; }
    }
}
