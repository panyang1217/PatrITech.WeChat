using Newtonsoft.Json;
using PatrITech.WeChat.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Work.Model
{
    public class AccessToken : IToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        [JsonConverter(typeof(ExpiresInConverter))]
        public TimeSpan ExpiresIn { get; set; }

        public DateTime CreateUtcTime { get; set; }

        public AccessToken()
        {
            CreateUtcTime = DateTime.UtcNow;
        }
    }

    internal class ExpiresInConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return TimeSpan.FromSeconds((long)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((long)((TimeSpan)value).TotalSeconds);
        }
    }
}
