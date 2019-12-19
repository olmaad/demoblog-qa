using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using DemoBlog.TestDataLib;
using DemoBlog.DataLib.Models;

namespace DemoBlog.BaseBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DemoBlogBaseBuilder <data folder path> <output folder path>");
                return;
            }

            var dataFolderPath = args[0];

            if (!Directory.Exists(dataFolderPath))
            {
                Console.WriteLine("Data folder not exists");
                return;
            }

            string outputFolderPath = null;

            if (args.Length > 1)
            {
                outputFolderPath = args[1];

                if (!Directory.Exists(outputFolderPath))
                {
                    Console.WriteLine("Output folder not exists");
                    return;
                }
            }

            try
            {
                var sha256 = SHA256.Create();

                if (File.Exists("blog.db"))
                {
                    File.Delete("blog.db");
                }

                var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
                optionsBuilder.UseSqlite("Data Source=blog.db");

                var context = new BlogContext(optionsBuilder.Options);

                context.Database.EnsureCreated();

                var data = new Data();

                using (StreamReader reader = new StreamReader(dataFolderPath + "/builder.json"))
                {
                    var dataString = reader.ReadToEnd();

                    data = JsonConvert.DeserializeObject<Data>(dataString);
                }

                foreach (var user in data.Users)
                {
                    context.Users.Add(DataConverter.ToModelType(user, DataConverter.OutputTypeData));
                }

                foreach (var post in data.Posts)
                {
                    context.Posts.Add(DataConverter.ToModelType(post, DataConverter.OutputTypeData));
                }

                foreach (var comment in data.Comments)
                {
                    context.Comments.Add(DataConverter.ToModelType(comment, DataConverter.OutputTypeData));
                }

                context.SaveChanges();

                Console.WriteLine("Base created");

                if (outputFolderPath != null)
                {
                    File.Copy("blog.db", Path.Combine(outputFolderPath, "blog.db"), true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
