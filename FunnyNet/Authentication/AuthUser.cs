using FunnyNet.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FunnyNet.Authentication
{
    public class AuthUser : User
    {
        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("messenger_token")]
        public string MessengerToken { get; private set; }

        [JsonProperty("messenger_active")]
        public bool MessengerActive { get; private set; }

        public string Password { get; private set; }
        public string Token { get; private set; }

        public bool LoggedIn => Token != null;

        public static async Task<AuthUser> LoginAsync(string email, string password)
        {
            AuthUser user = new AuthUser();
            return await user.TryLoginAsync(email, password) ? user : null;
        }

        public async Task<bool> TryLoginAsync(string email, string password)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoints.AuthToken);
            using FormUrlEncodedContent formContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", email },
                    { "password", password }
                });

            request.Content = formContent;

            using HttpResponseMessage response = await Funny.SendInternalAsync(request);
            if (!response.IsSuccessStatusCode)
                return false;

            JObject jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            return await TryLoginAsync(jObject["access_token"].ToString());
        }

        public static async Task<AuthUser> LoginAsync(string token)
        {
            AuthUser user = new AuthUser();
            return await user.TryLoginAsync(token) ? user : null;
        }

        public async Task<bool> TryLoginAsync(string token)
        {
            Token = token;
            Getter = this;

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Endpoints.Account);
            using HttpResponseMessage response = await Funny.SendInternalAsync(request, this);
            if (!response.IsSuccessStatusCode)
                return false;

            string json = await response.Content.ReadAsStringAsync();
            Newtonsoft.Json.JsonSerializer.CreateDefault().Populate(JObject.Parse(json)["data"].CreateReader(), this);
            return true;
        }
    }
}
