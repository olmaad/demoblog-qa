using DemoBlogShared.Bundles;
using TestDataLib;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ApiTestLib
{
    public static class Assert
    {
        public static void Diff<T>(IEnumerable<T> result, IEnumerable<T> expected, string message = "")
        {
            var straightDiff = result.Except(expected);
            var reversedDiff = expected.Except(result);

            if (straightDiff.Any() || reversedDiff.Any())
            {
                var exceptionMessage = message + "\nЗначения, которых нет в ожидаемых:";

                foreach (var it in straightDiff)
                {
                    exceptionMessage += "\n" + JsonConvert.SerializeObject(it);
                }

                exceptionMessage += "\nЗначения, которых нет в полученных:";

                foreach (var it in reversedDiff)
                {
                    exceptionMessage += "\n" + JsonConvert.SerializeObject(it);
                }

                throw new Exception(exceptionMessage);
            }
        }

        public static void PostBundle(PostsBundle bundle, IEnumerable<PostData> postsData, IEnumerable<UserData> usersData)
        {
            var postList = postsData.Select(d => DataConverter.ToModel(d)).ToList();
            var userList = usersData.Select(d => DataConverter.ToModel(d)).ToList();

            Diff(bundle.Posts, postList, "Неверный список постов");
            Diff(bundle.Users, userList, "Неверный список пользователей");
        }
    }
}
