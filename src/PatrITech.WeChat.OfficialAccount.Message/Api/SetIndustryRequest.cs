using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Api
{
    public class SetIndustryRequest
    {
        [JsonProperty(PropertyName = "industry_id1")]
        public int PrimaryIndustry { get; set; }
        [JsonProperty(PropertyName = "industry_id2")]
        public int DeputyIndustry { get; set; }
    }
}
