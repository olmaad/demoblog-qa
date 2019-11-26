using DemoBlogBackend.Rating;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Post : ICloneable, IRatingEntity
    {
        public double WeightToSelf { get; } = RatingWeights.PostVoteToPost;
        public double WeightToUser { get; } = RatingWeights.PostVoteToUser;
        public double WeightToPersonal { get; } = RatingWeights.PostVoteToPersonal;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; } = 1;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
