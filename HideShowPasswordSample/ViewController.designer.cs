// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace HideShowPasswordSample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField email { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Plugin.HideShowPassword.iOS.MDCHideShowPasswordTextField mdcPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Plugin.HideShowPassword.iOS.HideShowPasswordTextField password { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (email != null) {
                email.Dispose ();
                email = null;
            }

            if (mdcPassword != null) {
                mdcPassword.Dispose ();
                mdcPassword = null;
            }

            if (password != null) {
                password.Dispose ();
                password = null;
            }
        }
    }
}