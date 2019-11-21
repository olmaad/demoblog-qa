﻿using DemoBlogBackend.Rating;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoBlogBackend.Models
{
    public class Comment : ICloneable, IRatingEntity
    {
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
