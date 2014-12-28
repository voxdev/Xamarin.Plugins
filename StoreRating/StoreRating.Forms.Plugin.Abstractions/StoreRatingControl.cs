using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

namespace StoreRating.Forms.Plugin.Abstractions
{
    /// <summary>
    /// StoreRating Interface
    /// </summary>
    public class StoreRatingControl : RelativeLayout
    {
        /// <summary>
        /// Your application name
        /// </summary>
        private static string _AppName = "";
        public static string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }

        /// <summary>
        /// iOS application ID from iTunes Connect
        /// </summary>
        private static string _AppID = "";
        public static string AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }
        
        /// <summary>
        /// Set for Amazon store (Android only)
        /// </summary>
        private static bool _IsAmazon = false;
        public static bool IsAmazon
        {
            get { return _IsAmazon; }
            set { _IsAmazon = value; }
        }

        /// <summary>
        /// Rating message title text
        /// </summary>
        private static string _RatingMessageTitle = "Rate #MyApp#";
        public static string RatingMessageTitle
        {
            get { return _RatingMessageTitle; }
            set { _RatingMessageTitle = value; }
        }

        /// <summary>
        /// Rating message text
        /// </summary>
        private static string _RatingMessage = "If you enjoy using #MyApp#, would you mind taking a moment to rate it?\n\nIt won't take more than a minute. Thanks for your support!";
        public static string RatingMessage
        {
            get { return _RatingMessage; }
            set { _RatingMessage = value; }
        }

        /// <summary>
        /// Rating message cancel button text
        /// </summary>
        private static string _RatingCancelText = "No, Thanks";
        public static string RatingCancelText
        {
            get { return _RatingCancelText; }
            set { _RatingCancelText = value; }
        }

        /// <summary>
        /// Rating message remind later button text
        /// </summary>
        private static string _RatingRemindText = "Remind Me Later";
        public static string RatingRemindText
        {
            get { return _RatingRemindText; }
            set { _RatingRemindText = value; }
        }

        /// <summary>
        /// Rating message rate it now button text
        /// </summary>
        private static string _RateText = "Rate It Now";
        public static string RateText
        {
            get { return _RateText; }
            set { _RateText = value; }
        }

        /// <summary>
        /// Feedback message title text
        /// </summary>
        private static string _FeedbackMessageTitle = "Can we make it better?";
        public static string FeedbackMessageTitle
        {
            get { return _FeedbackMessageTitle; }
            set { _FeedbackMessageTitle = value; }
        }

        /// <summary>
        /// Feedback message
        /// </summary>
        private static string _FeedbackMessage = "Sorry to hear you didn't want to rate #MyApp#.\n\nTell us about your experience or suggest how we can make it better.";
        public static string FeedbackMessage
        {
            get { return _FeedbackMessage; }
            set { _FeedbackMessage = value; }
        }

        /// <summary>
        /// Feedback message cancel button text
        /// </summary>
        private static string _FeedbackCancelText = "No, Thanks";
        public static string FeedbackCancelText
        {
            get { return _FeedbackCancelText; }
            set { _FeedbackCancelText = value; }
        }

        /// <summary>
        /// Feedback message give feedback button text
        /// </summary>
        private static string _FeedbackText = "Give Feedback";
        public static string FeedbackText
        {
            get { return _FeedbackText; }
            set { _FeedbackText = value; }
        }

        /// <summary>
        /// Feedback email address
        /// </summary>
        private static string _FeedbackEmail = "";
        public static string FeedbackEmail
        {
            get { return _FeedbackEmail; }
            set { _FeedbackEmail = value; }
        }

        /// <summary>
        /// Number of application uses before rating message appears
        /// </summary>
        private static int _UsesBeforeRating = 10;
        public static int UsesBeforeRating
        {
            get { return _UsesBeforeRating; }
            set { _UsesBeforeRating = value; }
        }

        /// <summary>
        /// Number of days from first use before rating message appears
        /// </summary>
        private static int _DaysBeforeRating = 10;
        public static int DaysBeforeRating
        {
            get { return _DaysBeforeRating; }
            set { _DaysBeforeRating = value; }
        }

        /// <summary>
        /// Number of days after remind later button click before rating message appears
        /// </summary>
        private static int _DaysBeforeReminder = 5;
        public static int DaysBeforeReminder
        {
            get { return _DaysBeforeReminder; }
            set { _DaysBeforeReminder = value; }
        }

        /// <summary>
        /// Override condition to show rating message (for testing purposes)
        /// </summary>
        private static bool _Preview = false;
        public static bool Preview
        {
            get { return _Preview; }
            set { _Preview = value; }
        }

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private static DateTime DateInstalled
        {
            get
            {
                return AppSettings.GetValueOrDefault("date_installed", DateTime.Now);
            }
            set
            {
                AppSettings.AddOrUpdateValue("date_installed", value);
            }
        }

        private static DateTime DateReminder
        {
            get
            {
                return AppSettings.GetValueOrDefault("date_reminder", DateTime.MinValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue("date_reminder", value);
            }
        }

        private static int UsageCount
        {
            get
            {
                return AppSettings.GetValueOrDefault("usage_count", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("usage_count", value);
            }
        }

        private static bool IsRated
        {
            get
            {
                return AppSettings.GetValueOrDefault("is_rated", false);
            }
            set
            {
                AppSettings.AddOrUpdateValue("is_rated", value);
            }
        }

        public StoreRatingControl()
        {
            try
            {
                this.GestureRecognizers.Add(new TapGestureRecognizer());
                if (IsShowTime())
                {
                    ShowRating();
                }
                else
                {
                    this.IsVisible = false;
                }
            }
            catch (Exception X)
            {
                Debug.WriteLine(X.Message);
            }
        }

        public void ShowRating()
        {
            try
            {
                Dismiss();
                UpdateText();
                this.IsVisible = true;

                var bcol = new Color(Device.OnPlatform(Color.White.R, Color.Black.R, Color.Black.R),
                                     Device.OnPlatform(Color.White.G, Color.Black.G, Color.Black.G),
                                     Device.OnPlatform(Color.White.B, Color.Black.B, Color.Black.B),
                                    0.9);
                Label lbTitle = new Label { Text = RatingMessageTitle, TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), Font = Font.SystemFontOfSize(NamedSize.Large).WithAttributes(FontAttributes.Bold), HorizontalOptions = LayoutOptions.Center };
                Label lbBody = new Label { Text = RatingMessage, TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), Font = Font.SystemFontOfSize(NamedSize.Medium) };
                Button btnRate = new Button { Text = RateText, Font = Font.SystemFontOfSize(NamedSize.Medium) };
                Button btnRemind = new Button { Text = RatingRemindText, Font = Font.SystemFontOfSize(NamedSize.Medium) };
                Button btnCancel = new Button { Text = RatingCancelText, Font = Font.SystemFontOfSize(NamedSize.Medium) };
                if (Device.OS == TargetPlatform.WinPhone)
                {
                    btnRate.TextColor = Color.White;
                    btnRemind.TextColor = Color.White;
                    btnCancel.TextColor = Color.White;
                    btnRate.BorderColor = Color.White;
                    btnRemind.BorderColor = Color.White;
                    btnCancel.BorderColor = Color.White;
                }

                btnRate.Clicked += ((s, e) =>
                {
                    IsRated = true;
                    ShowStore();
                    Dismiss();
                });
                btnRemind.Clicked += ((s, e) =>
                {
                    IsRated = false;
                    DateReminder = DateTime.Now;
                    Dismiss();
                });
                btnCancel.Clicked += ((s, e) =>
                {
                    IsRated = true;
                    ShowFeedback();
                });

                StackLayout sl = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { lbTitle, lbBody, btnRate, btnRemind, btnCancel },
                };
                Frame fr = new Frame
                {
                    Content = sl,
                    BackgroundColor = bcol,
                };

                this.Children.Add(fr, xConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32((parent.Width - parent.Width * 0.9) / 2); }),
                                        yConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32((parent.Height - parent.Height * 0.7) / 2); }),
                                        widthConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32(parent.Width * 0.9); }),
                                        heightConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32(parent.Height * 0.7); }));
                this.UpdateChildrenLayout();
            }
            catch (Exception X)
            {
                Debug.WriteLine(X.Message);
            }
        }

        private void ShowFeedback()
        {
            try
            {
                Dismiss();
                this.IsVisible = true;

                var bcol = new Color(Device.OnPlatform(Color.White.R, Color.Black.R, Color.Black.R),
                                     Device.OnPlatform(Color.White.G, Color.Black.G, Color.Black.G),
                                     Device.OnPlatform(Color.White.B, Color.Black.B, Color.Black.B),
                                    0.9);
                Label lbTitle = new Label { Text = FeedbackMessageTitle, TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), Font = Font.SystemFontOfSize(NamedSize.Large).WithAttributes(FontAttributes.Bold), HorizontalOptions = LayoutOptions.Center };
                Label lbBody = new Label { Text = FeedbackMessage, TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White), Font = Font.SystemFontOfSize(NamedSize.Medium) };
                Button btnFeedback = new Button { Text = FeedbackText, Font = Font.SystemFontOfSize(NamedSize.Medium) };
                Button btnCancel = new Button { Text = FeedbackCancelText, Font = Font.SystemFontOfSize(NamedSize.Medium) };
                if (Device.OS == TargetPlatform.WinPhone)
                {
                    btnFeedback.TextColor = Color.White;
                    btnCancel.TextColor = Color.White;
                    btnFeedback.BorderColor = Color.White;
                    btnCancel.BorderColor = Color.White;
                }

                btnFeedback.Clicked += ((s, e) =>
                {
                    var mailservice = DependencyService.Get<ISendMailService>();
                    if (mailservice == null)
                        return;
                    mailservice.ShowDraft(AppName + " Feedback", "", new[] { FeedbackEmail }, null, null);
                    Dismiss();
                });
                btnCancel.Clicked += ((s, e) =>
                {
                    Dismiss();
                });

                StackLayout sl = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { lbTitle, lbBody, btnFeedback, btnCancel },
                };
                Frame fr = new Frame
                {
                    Content = sl,
                    BackgroundColor = bcol,
                };
                this.Children.Add(fr, xConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32((parent.Width - parent.Width * 0.9) / 2); }),
                                        yConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32((parent.Height - parent.Height * 0.7) / 2); }),
                                        widthConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32(parent.Width * 0.9); }),
                                        heightConstraint: Constraint.RelativeToParent((parent) => { return Convert.ToInt32(parent.Height * 0.7); }));
                
                this.UpdateChildrenLayout();
            }
            catch (Exception X)
            {
                Debug.WriteLine(X.Message);
            }
        }

        private void Dismiss()
        {
            if (this.Children.Count > 0)
            {
                this.Children.Clear();
            }
            this.IsVisible = false;
        }

        public static void IncUsage()
        {
            try
            {
                int i = UsageCount;
                UsageCount = i + 1;
            } catch (Exception X)
            {
                Debug.WriteLine(X.Message);
            }
        }

        private bool IsShowTime()
        {
            try
            {
                if (Preview || !IsRated)
                {
                    if (Preview
                        || (DateReminder == DateTime.MinValue && UsageCount >= UsesBeforeRating)
                        || (DateReminder == DateTime.MinValue && DateTime.Now >= DateInstalled.AddDays(DaysBeforeRating))
                        || (DateReminder != DateTime.MinValue && DateTime.Now >= DateReminder.AddDays(DaysBeforeReminder))
                        )
                    {
                        return true;
                    }
                }
            }
            catch (Exception X)
            {
                Debug.WriteLine(X.Message);
            }
            return false;
        }

        private void ShowStore()
        {
            var ratingservice = DependencyService.Get<IRatingService>();
            if (ratingservice == null)
                return;
            if (Device.OS == TargetPlatform.iOS)
                ratingservice.ShowStore(AppID, false);
            if (Device.OS == TargetPlatform.WinPhone)
                ratingservice.ShowStore("", false);
            if (Device.OS == TargetPlatform.Android)
                ratingservice.ShowStore("", IsAmazon);
        }

        private void UpdateText()
        {
            if (!string.IsNullOrEmpty(AppName))
            {
                RatingMessageTitle = RatingMessageTitle.Replace("#MyApp#", AppName);
                RatingMessage = RatingMessage.Replace("#MyApp#", AppName);
                RatingCancelText = RatingCancelText.Replace("#MyApp#", AppName);
                RatingRemindText = RatingRemindText.Replace("#MyApp#", AppName);
                RateText = RateText.Replace("#MyApp#", AppName);
                FeedbackMessageTitle = FeedbackMessageTitle.Replace("#MyApp#", AppName);
                FeedbackMessage = FeedbackMessage.Replace("#MyApp#", AppName);
                FeedbackCancelText = FeedbackCancelText.Replace("#MyApp#", AppName);
                FeedbackText = FeedbackText.Replace("#MyApp#", AppName);
            }
        }
    }
}
