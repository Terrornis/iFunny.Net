using FunnyNet.Authentication;
using FunnyNet.Paging;
using FunnyNet.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunnyNet
{
    public static class Funny
    {
        public static string BasicToken { get; private set; } = "MzEzMTM3MzIzMTY1MzczMjJEMzYzNTYyMzcyRDM0NjUzMTM1MkQzOTM0MzA2NDJENjMzMzYxNjU2MjM4MzYzOTYzMzk2NTMwX01zT0lKMzlRMjg6ZmM1MmE5NTFhNmYwNGJjNTJlNGZlOTIxYWQzMGYyYjMwMGI0YTk2OQ==";

        internal static HttpClient Client { get; } = new HttpClient();

        static Funny()
        {
            Client.DefaultRequestHeaders.Add("iFunny-Project-Id", "iFunny");
            Client.DefaultRequestHeaders.Add("User-Agent", "iFunny/5.27.2");
            Client.DefaultRequestHeaders.Add("Accept", "application/json,image/webp,video/mp4");
        }

        public static void RenewBasicToken()
        {
            BasicToken = GuestToken.Create();
        }

        public static Task<Feed<Content>> GetFeaturedAsync(int limit = 30, string next = null, AuthUser getter = null)
            => GetFeedAsync<Content>(Endpoints.FeaturedFeed, limit, next, getter);

        internal static async Task<Feed<T>> GetFeedAsync<T>(string feedUrl, int limit = 30, string next = null, AuthUser getter = null, bool flag = false) where T : RestObject
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Concat(feedUrl, flag ? "&" : "?", "limit=", limit, next == null ? "" : string.Concat("&next=", next)));
            using HttpResponseMessage response = await SendInternalAsync(request, getter);

            return new Feed<T>(JObject.Parse(await response.Content.ReadAsStringAsync()), feedUrl, getter);
        }

        internal static Task<HttpResponseMessage> SendInternalAsync(HttpRequestMessage request, AuthUser sender = null)
        {
            request.Headers.Add("Authorization", sender != null && sender.LoggedIn ? $"Bearer {sender.Token}" : $"Basic {BasicToken}");
            return Client.SendAsync(request);
        }
    }
}
