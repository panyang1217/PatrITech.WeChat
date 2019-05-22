using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Api
{
    class TemplateIdShortRequest
    {
        [JsonProperty(PropertyName = "template_id_short")]
        public string Value { get; set; }

        public TemplateIdShortRequest(string value)
        {
            Value = value;
        }
    }
}
