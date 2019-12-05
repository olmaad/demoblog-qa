using DemoBlogBackend.Rating;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Post : ICloneable, IRatingEntity
    {
        [JsonIgnore]
        public double WeightToSelf { get; } = RatingWeights.PostVoteToPost;
        [JsonIgnore]
        public double WeightToUser { get; } = RatingWeights.PostVoteToUser;
        [JsonIgnore]
        public double WeightToPersonal { get; } = RatingWeights.PostVoteToPersonal;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public double Rating { get; set; } = 1;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
