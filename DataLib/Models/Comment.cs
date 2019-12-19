using DemoBlog.DataLib.Models.Rating;
using Newtonsoft.Json;
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

        public bool Equals(Comment other)
        {
            if (other == null)
            {
                return false;
            }

            return (
                PostId == other.PostId &&
                UserId == other.UserId &&
                Text == other.Text);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Comment);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
