using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageCropperSharp
{
    using System.Drawing;

    using MonoTouch.CoreGraphics;
    using MonoTouch.Foundation;
    using MonoTouch.ObjCRuntime;
    using MonoTouch.UIKit;

    public class ImageCropperViewController : UIViewController
    {
        public UIScrollView ScrollView { get; set; }

        public UIImageView ImageView { get; set; }

        public IImageCropperDelegate Delegate { get; set; }

        public ImageCropperViewController(UIImage image)
            : base()
        {
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackTranslucent, true);

            this.ImageView = new UIImageView(image)
                {
                    Frame = new RectangleF(0.0f, 0.0f, image.Size.Width, image.Size.Height) 
                };

            this.ScrollView = new UIScrollView(new RectangleF(0.0f, -20.0f, 320.0f, 480.0f))
                {
                    BackgroundColor = UIColor.Black,
                    Delegate = new CustomUIScrollViewDelegate(this.ImageView),
                    ShowsHorizontalScrollIndicator = false,
                    ShowsVerticalScrollIndicator = false,
                    MaximumZoomScale = 2.0f,
                    ContentSize = this.ImageView.Frame.Size
                };
            
            this.ScrollView.MinimumZoomScale = this.ScrollView.Frame.Size.Width / this.ImageView.Frame.Size.Width;
            this.ScrollView.ZoomScale = this.ScrollView.MinimumZoomScale;
            this.ScrollView.AddSubview(this.ImageView);

            this.View.AddSubview(this.ScrollView);

            var navigationBar = new UINavigationBar(new RectangleF(0.0f, 0.0f, 320.0f, 44.0f))
                { 
                    BarStyle = UIBarStyle.Black, 
                    Translucent = true 
                };

            var navigationItem = new UINavigationItem(@"Move and Scale");
            
            var cancelButton = new UIBarButtonItem(UIBarButtonSystemItem.Cancel);
            cancelButton.Clicked += CancelCropping;

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done);
            doneButton.Clicked += FinishCropping;

            navigationItem.LeftBarButtonItem = cancelButton;
            navigationItem.RightBarButtonItem = doneButton;

            navigationBar.SetItems(new[] { navigationItem }, true);


            this.View.AddSubview(navigationBar);
        }

        private void CancelCropping(object sender, EventArgs e)
        {
            this.Delegate.DidCancel(this);
        }

        private void FinishCropping(object sender, EventArgs e)
        {
            var zoomScale = 1.0f / this.ScrollView.ZoomScale;

            var rect = new RectangleF(
                this.ScrollView.ContentOffset.X * zoomScale,
                this.ScrollView.ContentOffset.Y * zoomScale,
                this.ScrollView.Bounds.Size.Width * zoomScale,
                this.ScrollView.Bounds.Size.Height * zoomScale);

            var cr = this.ImageView.Image.CGImage.WithImageInRect(rect);

            var cropped = new UIImage(cr);

            cr.Dispose();

            this.Delegate.DidFinishCroppingWithImage(this, cropped);
        }
    }
}
