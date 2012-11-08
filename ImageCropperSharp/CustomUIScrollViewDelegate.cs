namespace ImageCropperSharp
{
    using MonoTouch.UIKit;

    public class CustomUIScrollViewDelegate : UIScrollViewDelegate
    {
        private readonly UIView imageView;

        public CustomUIScrollViewDelegate(UIView imageView)
        {
            this.imageView = imageView;
        }

        public override UIView ViewForZoomingInScrollView(UIScrollView scrollView)
        {
            return this.imageView;
        }
    }
}