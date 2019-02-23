using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using ZoomOverlay.Annotations;

namespace ZoomOverlay.ViewModels
{
    public class ZoomOverlayViewModel : INotifyPropertyChanged
    {
        private BitmapSource _image;
        private double _zoomFactor;
        private double _borderHeight;
        private double _borderWidth;
        private double _imageScaler;

        public ZoomOverlayViewModel()
        {
            ZoomFactor = 1.0;
            ImageScaler = 1.0;
        }

        public BitmapSource Image
        {
            get { return _image; }
            set
            {
                if (Equals(value, _image)) return;
                _image = value;
                OnPropertyChanged();
            }
        }

        public double ZoomFactor
        {
            get { return _zoomFactor; }
            set
            {
                if (value.Equals(_zoomFactor)) return;
                _zoomFactor = value;
                OnPropertyChanged();
            }
        }

        public double ImageScaler
        {
            get { return _imageScaler; }
            set
            {
                if (value.Equals(_imageScaler)) return;
                _imageScaler = value;
                OnPropertyChanged();
            }
        }

        public double BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (value.Equals(_borderWidth)) return;
                _borderWidth = value;
                OnPropertyChanged();
                UpdateImageScaler();
            }
        }

        public double BorderHeight
        {
            get { return _borderHeight; }
            set
            {
                if (value.Equals(_borderHeight)) return;
                _borderHeight = value;
                OnPropertyChanged();
                UpdateImageScaler();
            }
        }

        private void UpdateImageScaler()
        {
            var imageWidth = Image.PixelWidth;
            var imageHeight = Image.PixelHeight;

            if (imageWidth > 0 && imageHeight > 0 && BorderWidth > 0 && BorderHeight > 0)
            {

                if (BorderWidth / BorderHeight >= imageWidth / imageHeight)
                {
                    ImageScaler = ZoomFactor * imageWidth / BorderWidth;
                }
                else
                {
                    ImageScaler = ZoomFactor * imageHeight / BorderHeight;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}