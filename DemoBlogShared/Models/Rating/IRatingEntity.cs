using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlogShared.Models.Rating
{
    public interface IRatingEntity
    {
        double WeightToSelf { get; }
        double WeightToUser { get; }
        double WeightToPersonal { get; }

        long UserId { get; }
        double Rating { get; set; }
    }
}
