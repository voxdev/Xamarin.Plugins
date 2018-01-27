Xamarin.Plugins
===============

Xamarin.Plugins by VoxDev

## Store Rating Plugin for Xamain.Forms

Display a store rating dialog after a certain number of uses, or a certain number of days after installation 
or on demand (eg on button tap). The user can leave a rating, delay giving a rating or decline to give a rating.

The rating dialog brings up the relevant store page. If the rating request is declined then a feedback dialog is 
shown which sends an email to the developer.

![Store Rating Plugin for Xamain.Forms](http://www.voxdev.com/storerating3.png "Store Rating Plugin for Xamain.Forms")

![Store Rating Plugin for Xamain.Forms](http://www.voxdev.com/storerating4.png "Store Rating Plugin for Xamain.Forms")

### Settings

The plugin properties can be set once in the shared App.cs file

```C#
StoreRatingControl.AppName = "My App";
StoreRatingControl.FeedbackEmail = "myemail@mycompany.com";
StoreRatingControl.IncUsage();
```

#### Required Settings
+ AppName - the name of your application
+ FeedbackEmail - your contact email for receiving feedback

#### Optional Settings
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

### Manually Display the Rating Dialog

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

### Required Initialisation
To ensure that the control does not get linked out in the application the Init methods must be called just after the Xamarin.Forms.Init() in **each** of the 3 platform specific projects (MainPage.xaml.cs, AppDelegate.cs and MainActivity.cs).

```C#
Forms.Init();
StoreRating.Forms.Plugin.WindowsPhone.RatingService.Init();
StoreRating.Forms.Plugin.WindowsPhone.SendMailService.Init();
```

### Usage
Create a StoreRatingControl object and place in a grid on top of your existing page in position (0,0).

```C#
	public class App
	{
        private static StoreRatingControl _ratingControl;

		public static Page GetMainPage()
		{
            StoreRatingControl.AppName = "My App";
            StoreRatingControl.AppID = "0123456789";
            StoreRatingControl.FeedbackEmail = "myemail@mycompany.com";
            StoreRatingControl.IncUsage();
            //StoreRatingControl.Preview = true;

            Grid grTopLevel = new Grid
            {
                Padding = 0,
                BackgroundColor = Color.Blue,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = 
                    {
                        new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) },
                    },
                ColumnDefinitions = 
                    {
                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                    },
            };

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

            StackLayout sl = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label {
					    Text = "Hello, Forms !",
					    VerticalOptions = LayoutOptions.CenterAndExpand,
					    HorizontalOptions = LayoutOptions.CenterAndExpand,
				    },
                    btnRate
                }
            };

            grTopLevel.Children.Add(sl, 0, 1, 0, 1);

            _ratingControl = new StoreRatingControl();
            grTopLevel.Children.Add(_ratingControl, 0, 1, 0, 1);

			return new ContentPage
			{
                Content = grTopLevel
			};
		}
	}
```

### Usage Counter
To use the usage counter, you need to call the increment function each time the app starts (such as in the shared App.cs file)

```C#
StoreRatingControl.AppName = "My App";
StoreRatingControl.FeedbackEmail = "myemail@mycompany.com";
StoreRatingControl.IncUsage();
```

### License
Licensed under MIT see License file
