using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRating.Forms.Plugin.Abstractions
{
    /// <summary>
    /// Interface for leaving a native rating
    /// </summary>
    public interface IRatingService
    {
        /// <summary>
        /// Open the native store / marketplace
        /// </summary>
        void ShowStore(string id = "", bool IsAmazon = false);
    }
}
