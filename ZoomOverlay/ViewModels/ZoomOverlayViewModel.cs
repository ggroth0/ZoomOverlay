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
        private double _dotY;
        private double _dotX;

        public ZoomOverlayViewModel()
        {
            ZoomFactor = 1.0;
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

        public double DotX
        {
            get { return _dotX; }
            set
            {
                if (value.Equals(_dotX)) return;
                _dotX = value;
                OnPropertyChanged();
            }
        }

        public double DotY
        {
            get { return _dotY; }
            set
            {
                if (value.Equals(_dotY)) return;
                _dotY = value;
                OnPropertyChanged();
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