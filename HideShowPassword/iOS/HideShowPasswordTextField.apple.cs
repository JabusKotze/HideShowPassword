using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using MaterialComponents;
using UIKit;

namespace Plugin.HideShowPassword.iOS
{
    public interface IHideShowPassWordTextFieldDelegate
    {
        bool IsValidPassword(TextField textField, string password);
    }

    public class HideShowPasswordTextField : TextField, IPasswordToggleVissibilityDelegate
    {
        private PasswordToggleVissibilityView passwordToggleVissibilityView;
        public IHideShowPassWordTextFieldDelegate passwordDelegate;


        public HideShowPasswordTextField(IntPtr handle) : base(handle) { }

        public HideShowPasswordTextField()
        {
            SetupViews();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            SetupViews();
        }

        UIFont prefferedFont;
        /// <summary>
        /// Sets the preferred font.
        /// </summary>
        /// <value>The preferred font.</value>
        UIFont PreferredFont
        {
            get
            {
                return prefferedFont;
            }
            set
            {
                Font = value;
                if (SecureTextEntry)
                {
                    Font = null;
                }
                prefferedFont = Font;
            }
        }

        public override bool SecureTextEntry
        {
            get => base.SecureTextEntry; set
            {
                base.SecureTextEntry = value;
                if (!SecureTextEntry)
                {
                    Font = null;
                    Font = PreferredFont;
                }
            }
        }



        public void ViewHasToggled(PasswordToggleVissibilityView view, bool isSelected)
        {
            string hackString = Text;
            Text = " ";
            Text = hackString;

            SecureTextEntry = !isSelected;

        }

        private void SetupViews()
        {
            CGRect toggleFrame = new CGRect(0, 0, 66, 40);
            passwordToggleVissibilityView = new PasswordToggleVissibilityView(toggleFrame);
            passwordToggleVissibilityView.vissibilityDelegate = this;
            passwordToggleVissibilityView.CheckMarkVisible = false;

            KeyboardType = UIKeyboardType.ASCIICapable;
            RightView = passwordToggleVissibilityView;
            RightViewMode = UITextFieldViewMode.WhileEditing;
            Font = PreferredFont;

            AddTarget(PasswordTextChanged, UIControlEvent.EditingChanged);

            // if we don't do this, the eye flies in on textfield focus!
            RightView.Frame = RightViewRect(Bounds);

            // Default eye state based on our initial secure text entry
            passwordToggleVissibilityView.EyeStatus = SecureTextEntry ? PasswordToggleVissibilityView.EyeState.Closed : PasswordToggleVissibilityView.EyeState.Open;
        }

        void PasswordTextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Text))
            {
                passwordToggleVissibilityView.CheckMarkVisible = passwordDelegate?.IsValidPassword(this, Text) ?? false;
            }
            else
            {
                passwordToggleVissibilityView.CheckMarkVisible = false;
            }
        }

        public void TextFieldDidEndEditing(UITextField textField)
        {
            passwordToggleVissibilityView.EyeStatus = PasswordToggleVissibilityView.EyeState.Closed;
            SecureTextEntry = !Selected;
        }

    }
}
