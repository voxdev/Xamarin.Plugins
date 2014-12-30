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
    public class RatingService : IRatingService
    {
        public void ShowStore(string id = "", bool IsAmazon = false)
        {
            new MarketplaceReviewTask().Show();
        }
        public static void Init() { }
    }
}
