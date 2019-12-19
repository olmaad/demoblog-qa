using DemoBlog.DataLib.Models.Rating;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlog.DataLib.Models
{
    public class Post : ICloneable, IRatingEntity, IEquatable<Post>
    {
        private DateTimeOffset mDate;

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
        public DateTimeOffset Date
        {
            get
            {
                return mDate;
            }
            set
            {
                mDate = new DateTimeOffset(value.DateTime, new TimeSpan());
            }
        }
        public double Rating { get; set; } = 1;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Post other)
        {
            if (other == null)
            {
                return false;
            }

            return (
                Id == other.Id &&
                UserId == other.UserId &&
                Title == other.Title &&
                Preview == other.Preview &&
                Content == other.Content &&
                Date == other.Date);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Post);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
