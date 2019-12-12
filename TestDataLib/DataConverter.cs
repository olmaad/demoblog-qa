using DemoBlogShared.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TestDataLib
{
    public class DataConverter
    {
        private static SHA256 sha256 = SHA256.Create();

        public static Post ToModel(PostData data)
        {
            return new Post()
            {
                Id = data.Id,
                UserId = data.UserId,
                Date = (DateTime.UtcNow.Date + new TimeSpan(data.DateOffset, 0, 0, 0)),
                Title = data.Title,
                Preview = data.Preview,
                Content = data.Content
            };
        }

        public static User ToModel(UserData data)
        {
            return new User()
            {
                Id = data.Id,
                Login = data.Login,
                Name = data.Name,
                PasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data.Password))
            };
        }
    }
}
