using DemoBlogShared.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DemoBlogBaseBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DemoBlogBaseBuilder <data folder path>");
                Console.ReadKey();
                return;
            }

            var dataFolderPath = args[0];

            if (!Directory.Exists(dataFolderPath))
            {
                Console.WriteLine("Data folder not exists");
                Console.ReadKey();
                return;
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

                var users = new UsersData();

                using (StreamReader reader = new StreamReader(dataFolderPath + "/users.json"))
                {
                    var dataString = reader.ReadToEnd();

                    users = JsonConvert.DeserializeObject<UsersData>(dataString);
                }

                foreach (var user in users.Users)
                {
                    context.Users.Add(new User()
                    {
                        Id = user.Id,
                        Login = user.Login,
                        Name = user.Name,
                        PasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password))
                    });
                }

                var posts = new PostsData();

                using (StreamReader reader = new StreamReader(dataFolderPath + "/posts.json"))
                {
                    var dataString = reader.ReadToEnd();

                    posts = JsonConvert.DeserializeObject<PostsData>(dataString);
                }

                foreach (var post in posts.Posts)
                {
                    context.Posts.Add(new Post()
                    {
                        Id = post.Id,
                        UserId = post.UserId,
                        Date = (DateTime.Now.Date + new TimeSpan(post.DateOffset, 0, 0, 0)),
                        Title = post.Title,
                        Preview = post.Preview,
                        Content = post.Content
                    });
                }

                var comments = new CommentsData();

                using (StreamReader reader = new StreamReader(dataFolderPath + "/comments.json"))
                {
                    var dataString = reader.ReadToEnd();

                    comments = JsonConvert.DeserializeObject<CommentsData>(dataString);
                }

                foreach (var comment in comments.Comments)
                {
                    context.Comments.Add(new Comment()
                    {
                        PostId = comment.PostId,
                        UserId = comment.UserId,
                        Text = comment.Text
                    });
                }

                context.SaveChanges();

                Console.WriteLine("Base created");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
