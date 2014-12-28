using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRating.Forms.Plugin.Abstractions
{
    public interface IRatingService
    {
        void ShowStore(string id="", bool IsAmazon = false);
    }
}
