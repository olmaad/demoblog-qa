using DemoBlog.DataLib.Models.Rating;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlog.DataLib.Models
{
    public class Comment : ICloneable, IRatingEntity
    {
        public double WeightToSelf { get; } = RatingWeights.CommentVoteToComment;
        public double WeightToUser { get; } = RatingWeights.CommentVoteToUser;
        public double WeightToPersonal { get; } = RatingWeights.CommentVoteToPersonal;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; } = 1;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
