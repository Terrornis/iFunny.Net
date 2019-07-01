using System;
using System.Collections.Generic;
using System.Text;

namespace FunnyNet
{
    internal static class Endpoints
    {
        internal static readonly string AuthToken = "https://api.ifunny.mobi/v4/oauth2/token";
        internal static readonly string Account = "https://api.ifunny.mobi/v4/account";
        internal static readonly string FeaturedFeed = "https://api.ifunny.mobi/v4/feeds/featured";
        internal static readonly string Comments = "https://api.ifunny.mobi/v4/content/{0}/comments";
    }
}
