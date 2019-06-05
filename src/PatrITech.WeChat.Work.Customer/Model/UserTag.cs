using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Model
{
    public class UserTag
    {
        [JsonProperty(PropertyName = "group_name")]
        public string GroupName { get; set; }
        [JsonProperty(PropertyName = "tag_name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }
    }
}
