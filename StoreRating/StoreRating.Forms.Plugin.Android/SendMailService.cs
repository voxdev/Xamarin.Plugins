using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Xamarin.Forms;
using StoreRating.Forms.Plugin.Abstractions;

[assembly: Dependency(typeof(StoreRating.Forms.Plugin.Droid.SendMailService))]

namespace StoreRating.Forms.Plugin.Droid
{
    public class SendMailService : ISendMailService
    {
        public void ShowDraft(string subject, string body, string[] to, string[] cc, string[] bcc)
        {
            var intent = new Intent(Intent.ActionSend);

            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraEmail, to);
            if (cc != null)
                intent.PutExtra(Intent.ExtraCc, cc);
            if (bcc != null)
                intent.PutExtra(Intent.ExtraBcc, bcc);
            intent.PutExtra(Intent.ExtraSubject, subject ?? string.Empty);
            intent.PutExtra(Intent.ExtraText, body ?? string.Empty);

            Xamarin.Forms.Forms.Context.StartActivity(intent);
        }
    }
}