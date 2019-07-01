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
            foreach(Content feature in features)
            {
                Console.WriteLine("\nCreated on {0}\nFeatured on {1}", feature.DateCreated, feature.DateFeatured);
                Feed<Comment> comments = await feature.GetCommentsAsync(2);
                foreach (Comment comment in comments)
                {
                    Console.WriteLine("{0}: {1}", comment.Creator.Name, comment.Text);
                }
            }
        }
    }
}
