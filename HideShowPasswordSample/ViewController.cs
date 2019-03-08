using System;
using Foundation;
using Plugin.HideShowPassword.iOS;
using MaterialComponents;

using UIKit;

namespace HideShowPasswordSample
{
    public partial class ViewController : UIViewController, IHideShowPassWordTextFieldDelegate, IMDCHideShowPassWordTextFieldDelegate, IUITextFieldDelegate
    {
        // Declare MDC TextField Text Input Controller
        TextInputControllerOutlined controllerMDCPassword;

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


            // Setup mdcPassword controller for material component
            controllerMDCPassword = new TextInputControllerOutlined(mdcPassword);

            // Set delegates on MDC TextField
            mdcPassword.passwordDelegate = this;
            mdcPassword.Delegate = this;
            // Optionally set tint color on right view
            mdcPassword.RightView.TintColor = UIColor.Blue;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        /* Optionally implement interface member for Native UITextField IsValidPassword, 
         * To show a checkmark in field if the entered password is correct
         */
        public bool IsValidPassword(UITextField textField, string password)
        {
            return password.Length > 5;
        }

        /* Optionally implement interface member for MDC TextField IsValidPassword, 
         * To show a checkmark in field if the entered password is correct
         */
        public bool IsValidPassword(TextField textField, string password)
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
                case "MDCPassword":
                    mdcPassword.TextFieldDidEndEditing(textField);
                    break;
            }

        }


    }
}
