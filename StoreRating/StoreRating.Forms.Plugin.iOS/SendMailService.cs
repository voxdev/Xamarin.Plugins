using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
using StoreRating.Forms.Plugin.Abstractions;

[assembly: Dependency(typeof(StoreRating.Forms.Plugin.iOS.SendMailService))]

namespace StoreRating.Forms.Plugin.iOS
{
    public class SendMailService : ISendMailService
    {
        public void ShowDraft(string subject, string body, string[] to, string[] cc, string[] bcc)
        {
            var mailer = new MFMailComposeViewController();

            mailer.SetMessageBody(body ?? string.Empty, false);
            mailer.SetSubject(subject ?? string.Empty);
            if (cc != null)
                mailer.SetCcRecipients(cc);
            if (bcc != null)
                mailer.SetBccRecipients(bcc);
            mailer.SetToRecipients(to);
            mailer.Finished += (s, e) => ((MFMailComposeViewController)s).DismissViewController(true, () => { });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailer, true, null);
        }
        public static void Init() { }
    }
}
