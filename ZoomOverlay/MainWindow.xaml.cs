using System;
using System.Collections.Generic;
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
using ZoomOverlay.ViewModels;

namespace ZoomOverlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * references: https://stackoverflow.com/questions/16930074/wpf-image-pan-zoom-and-scroll-with-layers-on-a-canvas
         *             https://userinterfacemaker.wordpress.com/2015/09/08/zooming-for-canvas-cwpf/
         *             https://stackoverflow.com/questions/741956/pan-zoom-image
         *             
         * */

        public MainWindow()
        {
            InitializeComponent();

            var path = @"C:\temp\";
            var file = "sample.png";
            var fullpath = path + file;
            var bmp = new BitmapImage(new Uri(fullpath));

            var vm = new ZoomOverlayViewModel();
            vm.Image = bmp;

            DataContext = vm;
        }
    }
}
