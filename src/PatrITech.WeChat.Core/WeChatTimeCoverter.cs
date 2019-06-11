using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatrITech.WeChat.Core
{
    public class WeChatTimeCoverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(long))
            {
                var dt = new DateTime(1970, 1, 1).AddSeconds((long)reader.Value);
                return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"), TimeZoneInfo.Utc);
            }
            else if (reader.ValueType == typeof(string) && long.TryParse(reader.Value.ToString(), out var value))
            {
                var dt = new DateTime(1970, 1, 1).AddSeconds(value);
                return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"), TimeZoneInfo.Utc);
            }
            else
                throw new Exception($"Can not convert from value: {reader.Value}");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
