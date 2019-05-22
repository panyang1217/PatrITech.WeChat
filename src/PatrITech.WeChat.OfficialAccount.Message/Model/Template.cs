using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class Template
    {
        [JsonProperty(PropertyName = "template_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "primary_industry")]
        public string PrimaryIndustry { get; set; }
        [JsonProperty(PropertyName = "deputy_industry")]
        public string DeputyIndustry { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "example")]
        public string Example { get; set; }
    }
}
