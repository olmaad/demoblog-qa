using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlogBackend.Rating
{
    public interface IRatingEntity
    {
        long UserId { get; }
        double Rating { get; set; }
    }
}
