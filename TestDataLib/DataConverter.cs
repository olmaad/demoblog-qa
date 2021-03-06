﻿using DemoBlog.DataLib.Arguments;
using DemoBlog.DataLib.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DemoBlog.TestDataLib
{
    public static class DataConverter
    {
        private static SHA256 sha256 = SHA256.Create();

        /// <summary>
        /// Used to specify conversion type (model data)
        /// </summary>
        public static OutputTypeDataClass OutputTypeData { get; } = null;

        /// <summary>
        /// Used to specify conversion type (create arguments)
        /// </summary>
        public static OutputTypeCreateClass OutputTypeCreate { get; } = null;

        public class OutputTypeDataClass { }
        public class OutputTypeCreateClass { }

        public static Post ToModelType(PostData data, OutputTypeDataClass o)
        {
            return new Post()
            {
                Id = data.Id,
                UserId = data.UserId,
                Date = (DateTimeOffset.UtcNow.Date + new TimeSpan(data.DateOffset, 0, 0, 0)),
                Title = data.Title,
                Preview = data.Preview,
                Content = data.Content
            };
        }

        public static PostCreateArguments ToModelType(string sessionKey, PostData data, OutputTypeCreateClass o)
        {
            return new PostCreateArguments()
            {
                SessionKey = sessionKey,
                Post = ToModelType(data, OutputTypeData)
            };
        }

        public static User ToModelType(UserData data, OutputTypeDataClass o)
        {
            return new User()
            {
                Id = data.Id,
                Login = data.Login,
                Name = data.Name,
                PasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data.Password))
            };
        }
        
        public static UserCreateArguments ToModelType(UserData data, OutputTypeCreateClass o)
        {
            return new UserCreateArguments()
            {
                Login = data.Login,
                Name = data.Name,
                Password = data.Password
            };
        }

        public static CommentCreateArguments ToModelType(string sessionKey, CommentData data, OutputTypeCreateClass o)
        {
            return new CommentCreateArguments()
            {
                SessionKey = sessionKey,
                PostId = data.PostId,
                Text = data.Text
            };
        }

        public static Comment ToModelType(CommentData data, OutputTypeDataClass o)
        {
            return new Comment()
            {
                Id = 0,
                PostId = data.PostId,
                UserId = data.UserId,
                Text = data.Text
            };
        }

        public static VoteCreateArguments ToModelType(string sessionKey, VoteData data, OutputTypeCreateClass o)
        {
            return new VoteCreateArguments()
            {
                SessionKey = sessionKey,
                Vote = new Vote()
                {
                    Id = 0,
                    Type = (Vote.EntityType)data.Type,
                    EntityId = data.EntityId,
                    UserId = data.UserId,
                    Value = data.Value
                }
            };
        }
    }
}
