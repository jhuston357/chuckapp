// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MobileAppClass
{
    [Register ("EditViewController")]
    partial class EditViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField abox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView qbox { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (abox != null) {
                abox.Dispose ();
                abox = null;
            }

            if (qbox != null) {
                qbox.Dispose ();
                qbox = null;
            }
        }
    }
}