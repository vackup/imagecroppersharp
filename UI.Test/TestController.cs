using ImageCropperSharp;

namespace UI.Test
{
    using System;
    using System.Drawing;

    using MonoTouch.UIKit;

    public class TestController : UIViewController, IImageCropperDelegate
    {
        private UIImageView imageView;

        public override void LoadView()
        {
            base.LoadView();

            this.imageView = new UIImageView(UIImage.FromFile(@"Images/SteveJobsMacbookAir.JPG"))
                {
                    Frame = new RectangleF(80.0f, 20.0f, 160.0f, 230.0f), 
                    ContentMode = UIViewContentMode.ScaleAspectFit 
                };

            this.View.AddSubview(this.imageView);

            var button = UIButton.FromType(UIButtonType.RoundedRect);

            button.TouchUpInside += (sender, args) => this.CropImage();
            button.Frame = new RectangleF(124.0f, 258.0f, 72.0f, 37.0f);
            button.SetTitle("Crop", UIControlState.Normal);

            this.View.AddSubview(button);
        }

        private void CropImage()
        {
            var cropper = new ImageCropper(this.imageView.Image) { Delegate = this };

            this.PresentViewController(cropper, true, null);

            //[cropper release];
        }


        #region Implementation of IImageCropperDelegate

        public void DidFinishCroppingWithImage(ImageCropper cropper, UIImage image)
        {
            this.imageView.Image = image;

            this.DismissViewController(true, null);

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, true);
        }


        public void DidCancel(ImageCropper cropper)
        {
            this.DismissViewController(true, null);

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, true);
        }

        #endregion
    }
}