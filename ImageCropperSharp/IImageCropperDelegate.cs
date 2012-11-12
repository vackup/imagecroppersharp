namespace ImageCropperSharp
{
    using MonoTouch.UIKit;

    public interface IImageCropperDelegate
    {
        void DidFinishCroppingWithImage(ImageCropperViewController cropper, UIImage image);

        void DidCancel(ImageCropperViewController cropper);
    }
}