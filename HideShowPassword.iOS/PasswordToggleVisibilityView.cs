using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Plugin.HideShowPassword.iOS
{
    public interface IPasswordToggleVisibilityDelegate
    {
        void ViewHasToggled(PasswordToggleVisibilityView view, bool isSelected);
    }

    public class PasswordToggleVisibilityView : UIView
    {
        private UIImage eyeOpenedImage, eyeClosedImage, checkMarkImage;
        private UIButton eyeButton;
        private UIImageView checkmarkImageView;
        public IPasswordToggleVisibilityDelegate vissibilityDelegate;

        public PasswordToggleVisibilityView(IntPtr handle) : base(handle) { }

        public PasswordToggleVisibilityView(CGRect frame) : base(frame)
        {
            eyeOpenedImage = UIImage.FromBundle("hsp_eye_open").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            eyeClosedImage = UIImage.FromBundle("hsp_eye_closed");
            checkMarkImage = UIImage.FromBundle("hsp_password_checkmark").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            eyeButton = new UIButton(UIButtonType.Custom);
            checkmarkImageView = new UIImageView()
            {
                Image = checkMarkImage
            };
            SetupViews();
        }

        public enum EyeState
        {
            Open,
            Closed
        };

        public EyeState EyeStatus
        {
            get
            {
                return eyeButton.Selected ? EyeState.Open : EyeState.Closed;
            }
            set
            {
                eyeButton.Selected = (value == EyeState.Open);
            }
        }

        public bool CheckMarkVisible
        {
            get
            {
                return !checkmarkImageView.Hidden;
            }
            set
            {
                checkmarkImageView.Hidden = !value;
            }
        }

        public override UIColor TintColor
        {
            get => base.TintColor;
            set
            {
                eyeButton.TintColor = value;
                checkmarkImageView.TintColor = value;
            }
        }


        private void SetupViews()
        {
            nfloat padding = 10;
            nfloat buttonWidth = Frame.Width / 2 - padding;
            CGRect buttonFrame = new CGRect(buttonWidth + padding, 0, buttonWidth, Frame.Height);
            eyeButton.Frame = buttonFrame;
            eyeButton.BackgroundColor = UIColor.Clear;
            eyeButton.AdjustsImageWhenHighlighted = false;
            eyeButton.SetImage(eyeClosedImage, UIControlState.Normal);
            eyeButton.SetImage(eyeOpenedImage, UIControlState.Selected);
            eyeButton.AddTarget(EyeButtonPressed, UIControlEvent.TouchUpInside);
            eyeButton.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            eyeButton.TintColor = TintColor;
            AddSubview(eyeButton);

            nfloat checkMarkImageWidth = Frame.Width / 2 - padding;
            CGRect checkMarkFrame = new CGRect(padding, 0, checkMarkImageWidth, Frame.Height);
            checkmarkImageView.Frame = checkMarkFrame;
            checkmarkImageView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
            checkmarkImageView.ContentMode = UIViewContentMode.Center;
            checkmarkImageView.BackgroundColor = UIColor.Clear;
            checkmarkImageView.TintColor = TintColor;
            AddSubview(checkmarkImageView);
        }




        void EyeButtonPressed(object sender, EventArgs e)
        {
            eyeButton.Selected = !eyeButton.Selected;
            vissibilityDelegate?.ViewHasToggled(this, eyeButton.Selected);
        }

    }
}