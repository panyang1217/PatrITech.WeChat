using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.OfficialAccount.Model
{
    public class IndustryInfo
    {
        [JsonProperty(PropertyName ="primary_industry")]
        public Industry PrimaryIndstry { get; set; }

        [JsonProperty(PropertyName = "secondary_industry")]
        public Industry DeputyIndustry { get; set; }

        public class Industry
        {
            [JsonProperty(PropertyName = "first_class")]
            public string FirstClass { get; set; }

            [JsonProperty(PropertyName ="second_class")]
            public string SecondClass { get; set; }
        }
    }
}
