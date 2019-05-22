using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Api
{
    class TemplateIdRequest
    {
        [JsonProperty("template_id")]
        public string Value { get; set; }
    }
}
