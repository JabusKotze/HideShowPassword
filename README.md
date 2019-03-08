# HideShowPassword
HideShowPassword is an easy to use xamarin ios extension of UITextField and/or the MaterialComponents TextField to toggle the password visibility in a text field. Just tap the eye icon on right view to toggle visibility.

<link href='//fonts.googleapis.com/css?family=RobotoDraft:regular,bold,italic,thin,light,bolditalic,black,medium&amp;lang=en' rel='stylesheet' type='text/css'>

<h1 align="center">
  HideShowPassword for Xamarin.iOS
</h1>

[![GitHub release](https://img.shields.io/github/release/jabusjavus/HideShowPassword.svg)](https://github.com/jabusjavus/HideShowPassword/releases) [![NuGet Badge](https://buildstats.info/nuget/Plugin.HideShowPassword.iOS)](https://www.nuget.org/packages/Plugin.HideShowPassword.iOS/) [![license](https://img.shields.io/github/license/jabusjavus/HideShowPassword.svg)](https://github.com/jabusjavus/HideShowPassword/blob/master/LICENSE)[![GitHub Issues](https://img.shields.io/github/issues/jabusjavus/HideShowPassword.svg)](https://github.com/jabusjavus/HideShowPassword/issues) [![Contributions welcome](https://img.shields.io/badge/contributions-welcome-orange.svg)](https://github.com/jabusjavus/HideShowPassword#contribute)

<p align="center">
<a href="samples/"><img  src="assets/sample.gif" width="33%"></a>
</p>

## Install NuGet Package
* [Plugin.HideShowPassword.iOS](http://www.nuget.org/packages/Plugin.HideShowPassword.iOS) [![NuGet](https://img.shields.io/nuget/v/Plugin.HideShowPassword.iOS.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.HideShowPassword.iOS)

## Sample Usage - Native UITextField

View the sample application - * [HideShowPasswordSample](https://github.com/jabusjavus/HideShowPassword/tree/master/HideShowPasswordSample)

1. Add a UITextField to your Storyboard or Nib layout and select HideShowPasswordTextField as the Class
2. Or just select the Hide Show Password Text Field from toolbox custom controls 
3. Optionally extend `IHideShowPassWordTextFieldDelegate` and implement `IsValidPassword` to show a checkmark if password is valid and set `passwordDelegate`
4. Optionally extend `IUITextFieldDelegate` and override `EditingEnded` to reset secure entry when not in focus and set text field `Delegate`

```csharp
public class ViewController : UIViewController, IHideShowPassWordTextFieldDelegate, IUITextFieldDelegate
{
	      protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitComponents();
        }

        void InitComponents()
        {
            // Set Delegates on normal UITextField password
            password.passwordDelegate = this;
            password.Delegate = this;
            // Optionally set tint color on right view
            password.RightView.TintColor = UIColor.Blue;
        }
        
        /* Optionally implement interface member for Native UITextField IsValidPassword, 
         * To show a checkmark in field if the entered password is correct
         */
        public bool IsValidPassword(UITextField textField, string password)
        {
            return password.Length > 5;
        }
        
        [Export("textFieldDidEndEditing:")]
        public void EditingEnded(UITextField textField)
        {
            /* After editing of text field was complete, 
             * you can call the TextFieldDidEndEditing to change it back to a secure text entry.
             * 
             * You can set an Accessibility label for these text fields in the UI designer to determine from which textField the editing was ended
             */            
            switch (textField.AccessibilityLabel)
            {
                case "Password":
                    password.TextFieldDidEndEditing(textField);
                    break;
            }

        }
}
```

# License #

Xamarin HideShowPassword for iOS

Copyright (c) 2019 Xamarin HideShowPassword for iOS Authors.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
