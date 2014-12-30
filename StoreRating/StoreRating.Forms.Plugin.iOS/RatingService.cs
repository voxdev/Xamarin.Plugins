using System;
using System.Collections.Generic;
using System.Text;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Xamarin.Forms;
using StoreRating.Forms.Plugin.Abstractions;

[assembly: Dependency(typeof(StoreRating.Forms.Plugin.iOS.RatingService))]

namespace StoreRating.Forms.Plugin.iOS
{
    public class RatingService : IRatingService
    {
        public void ShowStore(string id = "", bool IsAmazon = false)
        {
            if (!string.IsNullOrWhiteSpace(id))
                UIApplication.SharedApplication.OpenUrl(new NSUrl("itms-apps://itunes.apple.com/app/id" + id));
        }
        public static void Init() { }
    }
}
