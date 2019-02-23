using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZoomOverlay.Views
{
    /// <summary>
    /// Interaction logic for ZoomOverlayView.xaml
    /// </summary>
    public partial class ZoomOverlayView : UserControl
    {
        public ZoomOverlayView()
        {
            InitializeComponent();

            ZoomFactor = 1;
        }

        private void BorderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScaleImage();
        }

        private double _imageWidth, _imageHeight;

        void ScaleImage()
        {
            if (TheImage.Source != null)
            {
                var containerWidth = SV_Container.ActualWidth;
                var containerHeight = SV_Container.ActualHeight;

                if (_imageWidth > 0 && _imageHeight > 0 && containerWidth > 0 && containerHeight > 0)
                {
                    _scale = 1.0;
                    double imageLeft = 0, imageTop = 0;

                    if (containerWidth / containerHeight >= _imageWidth / _imageHeight)
                    {
                        //container too wide
                        _scale = ZoomFactor * containerHeight / _imageHeight;
                        imageLeft = (containerWidth / _scale - _imageWidth) / 2;
                    }
                    else
                    {
                        //container too tall
                        _scale = ZoomFactor * containerWidth / _imageWidth;
                        imageTop = (containerHeight / _scale - _imageHeight) / 2;
                    }

                    Canvas.SetLeft(Dot, _imageWidth / 4 - 2.5);
                    Canvas.SetTop(Dot, _imageHeight / 4 - 2.5);

                    Border.Width = _scale * _imageWidth;
                    Border.Height = _scale * _imageHeight;
                    CanvasScaler.ScaleX = CanvasScaler.ScaleY = _scale;
                }
            }
        }

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            "Image", typeof(BitmapImage), typeof(ZoomOverlayView), new PropertyMetadata(default(BitmapImage), OnImageChanged));

        private static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as ZoomOverlayView;

            var img = e.NewValue as BitmapImage;
            view.TheImage.Source = img;
            if (img != null)
            {
                view._imageWidth = img.PixelWidth;
                view._imageHeight = img.PixelHeight;
            }
            view.TheImage.Width = view._imageWidth;
            view.TheImage.Height = view._imageHeight;
            view.ScaleImage();
        }

        public BitmapImage Image
        {
            get { return (BitmapImage) GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        private Point _start;
        private bool _dragActive;
        private double _vOffsetOrigin;
        private double _hOffsetOrigin;
        private double _scale;

        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register(
            "ZoomFactor", typeof(double), typeof(ZoomOverlayView), new PropertyMetadata(default(double), (o, args) =>
            {
                (o as ZoomOverlayView)?.ScaleImage();
            }));

        public double ZoomFactor
        {
            get { return (double) GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        }

        private void OnCanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var canvasMousePos = e.GetPosition(TheCanvas);
            //canvasMousePos.X /= TheCanvas.ActualWidth;
            //canvasMousePos.Y /= TheCanvas.ActualHeight;
            var scrollerMousePos = e.GetPosition(SV_Container);

            if (e.Delta > 0)
            {
                ZoomFactor += 1;
            }
            else
            {
                var z = ZoomFactor;
                z -= 1;
                ZoomFactor = z < 1 ? 1 : z;
            }

            SV_Container.ScrollToVerticalOffset(canvasMousePos.Y * _scale - scrollerMousePos.Y);
            SV_Container.ScrollToHorizontalOffset(canvasMousePos.X * _scale - scrollerMousePos.X);
        }

        private void TheCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragActive && e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(SV_Container);
                SV_Container.ScrollToVerticalOffset(_vOffsetOrigin + _start.Y - pos.Y);
                SV_Container.ScrollToHorizontalOffset(_hOffsetOrigin + _start.X - pos.X);
            }
        }

        private void TheCanvas_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }

        private void TheCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _start = e.GetPosition(SV_Container);
            _dragActive = true;

            _vOffsetOrigin = SV_Container.VerticalOffset;
            _hOffsetOrigin = SV_Container.HorizontalOffset;
        }
    }
}
