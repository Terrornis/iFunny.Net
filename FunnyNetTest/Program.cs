using FunnyNet;
using FunnyNet.Authentication;
using FunnyNet.Paging;
using FunnyNet.Rest;
using System;
using System.Threading.Tasks;

namespace FunnyNetTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Feed<Content> features = await Funny.GetFeaturedAsync();
            foreach(Content feature in features.Items)
                Console.WriteLine("Created on {0}\nFeatured on {1}\n", feature.DateCreated, feature.DateFeatured);
        }
    }
}
