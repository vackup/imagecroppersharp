namespace ImageCropperSharp
{
    using MonoTouch.UIKit;

    public interface IImageCropperDelegate
    {
        void DidFinishCroppingWithImage(ImageCropper cropper, UIImage image);

        void DidCancel(ImageCropper cropper);
    }
}