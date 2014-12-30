Xamarin.Plugins
===============

Xamarin.Plugins by VoxDev

##Store Rating Plugin for Xamain.Forms

Display a store rating dialog after a certain number of uses, or a certain number of days after installation 
or on demand (eg on button tap). The user can leave a rating, delay giving a rating or decline to give a rating.

The rating dialog brings up the relevant store page. If the rating request is declined then a feedback dialog is 
shown which sends an email to the developer.

![Store Rating Plugin for Xamain.Forms](http://www.voxdev.com/storerating1.png "Store Rating Plugin for Xamain.Forms")

![Store Rating Plugin for Xamain.Forms](http://www.voxdev.com/storerating2.png "Store Rating Plugin for Xamain.Forms")

###Settings

The plugin properties can be set once in the shared App.cs file

```C#
StoreRatingControl.AppName = "My App";
StoreRatingControl.FeedbackEmail = "myemail@mycompany.com";
```

####Required Settings
+ AppName - the name of your application
+ FeedbackEmail - your contact email for receiving feedback

####Optional Settings
+ AppID - the Apple ID of your app
+ IsAmazon - indicates if the Amazon Android store should be shown (instead of the default Google Play store)
+ RatingMessageTitle - the title of the Rating dialog
+ RatingMessage - the body of the Rating dialog
+ RatingCancelText - the cancel button text for the Rating dialog
+ RatingRemindText - the remind later button text for the Rating dialog
+ RatingText - the rate it now button text for the Rating dialog
+ FeedbackMessageTitle - the title of the Feedback dialog
+ FeedbackMessage - the body of the Feedback dialog
+ FeedbackCancelText - the cancel button text for the Feedback dialog
+ FeedbackText - the give feedback button text for the Feedback dialog
+ UsesBeforeRating - the number of application uses before Rating dialog is shown (default 10)
+ DaysBeforeRating - the number of days from first use before the Rating dialog is shown (default 10)
+ DaysBeforeReminder - the number of days after remind later button click before the Rating dialog is shown again (default 5)
+ Preview - force the Rating dialog to show on next use

###Manually Display the Rating Dialog

The Rating dialog can be shown manually, for example on a button click

```C#
Button btnRate = new Button
{
    Text = "Rate Now!",
    VerticalOptions = LayoutOptions.CenterAndExpand,
    HorizontalOptions = LayoutOptions.CenterAndExpand,
};
btnRate.Clicked += ((s, e) =>
{
    _ratingControl.ShowRating();
});
```

###Required Initialisation
To ensure that the control does not get linked out in the application the Init methods must be called just after the Xamarin.Forms.Init() in **each** of the 3 platform specific projects (MainPage.xaml.cs, AppDelegate.cs and MainActivity.cs).

```C#
Forms.Init();
StoreRating.Forms.Plugin.WindowsPhone.RatingService.Init();
StoreRating.Forms.Plugin.WindowsPhone.SendMailService.Init();
```

###License
Licensed under MIT see License file
