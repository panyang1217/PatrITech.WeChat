using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PatrITech.WeChat.Core;
using PatrITech.WeChat.Work.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PatrITech.WeChat.Work.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public async static Task<ResultState> ReadAsResult(this HttpResponseMessage resp)
        {
            var jobj = JObject.Load(new JsonTextReader(new StreamReader(await resp.Content.ReadAsStreamAsync())));

            if (jobj.TryGetValue("errcode", out var errorCode) && errorCode.Value<int>() != 0)
            {
                return jobj.ToObject<ResultState>();
            }
            else
            {
                return ResultState.OK;
            }
        }

        public async static Task<(T[], ResultState)> ReadAsResults<T>(this HttpResponseMessage resp, string jsonPath)
        {
            var jobj = JObject.Load(new JsonTextReader(new StreamReader(await resp.Content.ReadAsStreamAsync())));

            if (jobj.TryGetValue("errcode", out var errorCode) && errorCode.Value<int>() != 0)
            {
                return (null, jobj.ToObject<ResultState>());
            }
            else
            {
                jsonPath = string.IsNullOrEmpty(jsonPath) ? "$" : jsonPath;

                var token = jobj.SelectTokens(jsonPath);

                if (token == null)
                {
                    throw new Exception($"无效的JsonPath: '{jsonPath}'");
                }
                else
                {
                    return (token.Select(t =>
                    {
                        var result = t.ToObject<T>();

                        if (result is IRawResult)
                        {
                            var raw = (result as IRawResult);
                            raw.Content = t.ToString();
                            raw.SHA1 = SHA1(raw.Content);
                        }

                        return result;
                    }).ToArray(), ResultState.OK);
                }

            }
        }
        public async static Task<(T Payload, ResultState State)> ReadAsResult<T>(this HttpResponseMessage resp, string jsonPath) where T : class
        {
            var jobj = JObject.Load(new JsonTextReader(new StreamReader(await resp.Content.ReadAsStreamAsync())));

            if (jobj.TryGetValue("errcode", out var errorCode) && errorCode.Value<int>() != 0)
            {
                return (null, jobj.ToObject<ResultState>());
            }
            else
            {
                jsonPath = string.IsNullOrEmpty(jsonPath) ? "$" : jsonPath;

                var token = jobj.SelectToken(jsonPath);

                if (token == null)
                {
                    throw new Exception($"无效的JsonPath: '{jsonPath}'");
                }
                else
                {
                    var result = token.ToObject<T>();

                    if (result is IRawResult)
                    {
                        var raw = (result as IRawResult);
                        raw.Content = token.ToString();
                        raw.SHA1 = SHA1(raw.Content);
                    }
                    return (result, ResultState.OK);
                }

            }
        }

        public async static Task<(T Payload, ResultState State)> ReadAsResult<T>(this HttpResponseMessage resp) where T : class
        {
            return await resp.ReadAsResult<T>("");
        }

        public async static Task<(T[] Payload, ResultState State)> ReadAsResults<T>(this HttpResponseMessage resp) where T : class
        {
            return await resp.ReadAsResults<T>("");
        }

        private static byte[] SHA1(string content)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();

            return sha.ComputeHash(Encoding.Default.GetBytes(content));
        }
    }
}
