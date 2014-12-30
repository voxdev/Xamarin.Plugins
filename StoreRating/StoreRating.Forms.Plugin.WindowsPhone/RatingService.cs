using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.Phone.Tasks;
using StoreRating.Forms.Plugin.Abstractions;

[assembly: Dependency(typeof(StoreRating.Forms.Plugin.WindowsPhone.RatingService))]

namespace StoreRating.Forms.Plugin.WindowsPhone
{
    /// <summary>
    /// Windows native store/marketplace control
    /// </summary>
    public class RatingService : IRatingService
    {
        /// <summary>
        /// Windows Open the native store/marketplace control
        /// </summary>
        public void ShowStore(string id = "", bool IsAmazon = false)
        {
            new MarketplaceReviewTask().Show();
        }
        /// <summary>
        /// Windows dummy initialisation
        /// </summary>
        public static void Init() { }
    }
}
