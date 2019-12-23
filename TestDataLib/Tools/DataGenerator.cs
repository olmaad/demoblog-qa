using System;

namespace DemoBlog.TestDataLib.Tools
{
    public class DataGenerator
    {
        static string[] Adjectives = {
            "Alive",
            "Awful",
            "Green",
            "Minor",
            "Awesome",
            "Callous",
            "Ignorant",
            "Short",
            "Graceful",
            "Scared",
            "Adhesive",
            "Concerned",
            "Sharp",
            "Dull",
            "Lucky",
            "Far",
            "Somber",
            "Yellow",
            "Black",
            "Powerful",
            "Shy",
            "Big",
            "Little",
            "Useful",
            "Loud",
            "Calm",
            "Clean",
            "Wise",
            "Royal",
            "Cold",
            "Wild",
            "White"
        };

        static string[] Nouns = {
            "Smoke",
            "Mind",
            "Pot",
            "Bee",
            "Rabbit",
            "Hat",
            "Hill",
            "Flesh",
            "Queen",
            "Snake",
            "Cent",
            "Null",
            "Son",
            "Doctor",
            "Spoon",
            "Death"
        };

        static Random mRandom = new Random();

        public static string GenerateUsername()
        {
            var first = Adjectives[mRandom.Next(0, Adjectives.Length)];
            var second = Adjectives[mRandom.Next(0, Adjectives.Length)];

            if (second == first)
            {
                second = "";
            }

            var noun = Nouns[mRandom.Next(0, Nouns.Length)];

            if (noun == "Doctor")
            {
                noun += Nouns[mRandom.Next(0, Nouns.Length)];
            }

            return (first + second + noun);
        }
    }
}
