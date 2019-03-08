using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CoreGraphics;
using Foundation;
using MaterialComponents;
using UIKit;

namespace Plugin.HideShowPassword.iOS
{
    public interface IHideShowPassWordTextFieldDelegate
    {
        /// <summary>
        /// Override this method to toggle the checkmark for valid passwords
        /// </summary>
        /// <returns><c>true</c>, if valid password was set, <c>false</c> otherwise.</returns>
        /// <param name="textField">The UITextField considered</param>
        /// <param name="password">The current password</param>
        bool IsValidPassword(UITextField textField, string password);
    }

    public interface IMDCHideShowPassWordTextFieldDelegate
    {
        /// <summary>
        /// Override this method to toggle the checkmark for valid passwords using MDC TextField
        /// </summary>
        /// <returns><c>true</c>, if valid password was set, <c>false</c> otherwise.</returns>
        /// <param name="textField">The MDC TextField</param>
        /// <param name="password">The current password in field</param>
        bool IsValidPassword(TextField textField, string password);
    }

    [Register("HideShowPasswordTextField"), DesignTimeVisible(true)]
    public class HideShowPasswordTextField : UITextField, IPasswordToggleVisibilityDelegate
    {
        private PasswordToggleVisibilityView passwordToggleVissibilityView;
        public IHideShowPassWordTextFieldDelegate passwordDelegate;

        public CGSize ToggleFrameSize { get; set; }

        public HideShowPasswordTextField(IntPtr handle) : base(handle) { }

        public HideShowPasswordTextField()
        {
            ToggleFrameSize = new CGSize(66, Frame.Height);
            SetupViews();
        }

        public HideShowPasswordTextField(CGSize frameSize)
        {
            ToggleFrameSize = frameSize;
            SetupViews();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            ToggleFrameSize = new CGSize(66, Frame.Height);
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



        public void ViewHasToggled(PasswordToggleVisibilityView view, bool isSelected)
        {
            string hackString = Text;
            Text = " ";
            Text = hackString;

            SecureTextEntry = !isSelected;

        }

        private void SetupViews()
        {
            CGRect toggleFrame = new CGRect(new CGPoint(0,0), ToggleFrameSize);
            passwordToggleVissibilityView = new PasswordToggleVisibilityView(toggleFrame);
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
            passwordToggleVissibilityView.EyeStatus = SecureTextEntry ? PasswordToggleVisibilityView.EyeState.Closed : PasswordToggleVisibilityView.EyeState.Open;
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

        /// <summary>
        /// Call this method inside EditingEnded when you override IUITextFieldDelegate,
        /// to reset the textfield to a secure text entry when not focussed
        /// </summary>
        /// <param name="textField">Text field.</param>
        public void TextFieldDidEndEditing(UITextField textField)
        {
            passwordToggleVissibilityView.EyeStatus = PasswordToggleVisibilityView.EyeState.Closed;
            SecureTextEntry = !Selected;
        }

    }

    [Register("MDCHideShowPasswordTextField"), DesignTimeVisible(false)]
    public class MDCHideShowPasswordTextField : TextField, IPasswordToggleVisibilityDelegate
    {
        private PasswordToggleVisibilityView passwordToggleVissibilityView;
        public IMDCHideShowPassWordTextFieldDelegate passwordDelegate;

        public CGSize ToggleFrameSize { get; set; }

        public MDCHideShowPasswordTextField(IntPtr handle) : base(handle) { }

        public MDCHideShowPasswordTextField()
        {
            ToggleFrameSize = new CGSize(66, Frame.Height);
            SetupViews();
        }

        public MDCHideShowPasswordTextField(CGSize frameSize)
        {
            ToggleFrameSize = frameSize;
            SetupViews();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            ToggleFrameSize = new CGSize(66, Frame.Height);
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



        public void ViewHasToggled(PasswordToggleVisibilityView view, bool isSelected)
        {
            string hackString = Text;
            Text = " ";
            Text = hackString;

            SecureTextEntry = !isSelected;

        }

        private void SetupViews()
        {
            CGRect toggleFrame = new CGRect(new CGPoint(0, 0), ToggleFrameSize);
            passwordToggleVissibilityView = new PasswordToggleVisibilityView(toggleFrame);
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
            passwordToggleVissibilityView.EyeStatus = SecureTextEntry ? PasswordToggleVisibilityView.EyeState.Closed : PasswordToggleVisibilityView.EyeState.Open;
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

        /// <summary>
        /// Call this method inside EditingEnded when you override IUITextFieldDelegate,
        /// to reset the textfield to a secure text entry when not focussed
        /// </summary>
        /// <param name="textField">Text field.</param>
        public void TextFieldDidEndEditing(UITextField textField)
        {
            passwordToggleVissibilityView.EyeStatus = PasswordToggleVisibilityView.EyeState.Closed;
            SecureTextEntry = !Selected;
        }

    }
}