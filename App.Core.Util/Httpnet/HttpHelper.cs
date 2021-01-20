using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Core.Util.Httpnet
{
    public class HttpHelper
    {
        public static async Task<string> PostAsync(string baseUri, string content, string requestUri, string mediaType = "application/x-www-form-urlencoded")
        {
            string jsonResp = string.Empty;
            try
            {
                using var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(uriString: baseUri)
                };
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                using var httpContent = new StringContent(content: content, encoding: Encoding.UTF8, mediaType: mediaType);
                using var response = await httpClient.PostAsync(requestUri: requestUri, content: httpContent);

                jsonResp = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync()
                    : JsonConvert.DeserializeObject<InvalidResp>(value: await response.Content.ReadAsStringAsync()).Title;
            }
            catch (Exception)
            {
                // ignored
            }
            return jsonResp;
        }

        public static Task PostAsync(string baseUri, string requestUri)
        {
            throw new NotImplementedException();
        }

        public static async Task<string> GetAsync(string baseUri, string requestUri)
        {
            string jsonResp = string.Empty;
            try
            {
                using var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(uriString: baseUri)
                };
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                using var response = await httpClient.GetAsync(requestUri: requestUri);

                jsonResp = response.IsSuccessStatusCode
                     ? await response.Content.ReadAsStringAsync()
                     : JsonConvert.DeserializeObject<InvalidResp>(value: await response.Content.ReadAsStringAsync()).Title;
            }
            catch (Exception)
            {
                // ignored
            }
            return jsonResp;
        }

        public static async Task<string> PutAsync(string baseUri, string content, string requestUri, string mediaType = "application/x-www-form-urlencoded")
        {
            string jsonResp = string.Empty;
            try
            {
                using var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(uriString: baseUri)
                };
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                using var httpContent = new StringContent(content: content, encoding: Encoding.UTF8, mediaType: mediaType);
                using var response = await httpClient.PutAsync(requestUri: requestUri, content: httpContent);

                jsonResp = response.IsSuccessStatusCode
                    ? await response.Content.ReadAsStringAsync()
                    : JsonConvert.DeserializeObject<InvalidResp>(value: await response.Content.ReadAsStringAsync()).Title;
            }
            catch (Exception)
            {
                // ignored
            }
            return jsonResp;
        }
    }

    class InvalidResp
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
    }
}
