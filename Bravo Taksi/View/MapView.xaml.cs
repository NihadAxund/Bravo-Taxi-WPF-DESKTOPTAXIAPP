using Bravo_Taksi.ViewModel;
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
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Symbology;
using Bravo_Taksi.Models;

namespace Bravo_Taksi.View
{
    public partial class NavigateRouteView : Window
    {
        public static bool IsNagivateStart { get; set; } = true;
        private MapViewModel VM { get; set; }

        public NavigateRouteView()
        {
            InitializeComponent();
            Initialize();

        }
        private void MyMapView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsNagivateStart)
            {
                Point mousePoint = Mouse.GetPosition(this);

                MapViewModel.MyMapView.GraphicsOverlays.Clear();

                MapViewModel.CommandCreatedObject.PointTwo = MyMapView.ScreenToLocation(mousePoint);
                MapViewModel.CommandCreatedObject.Initialize();
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuToggleButton.IsChecked = true;
        }

        private void DrawerHost_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }
        private void Initialize()
        {
            VM = new MapViewModel(this);
            MyMapView.Map = new Esri.ArcGISRuntime.Mapping.Map(Esri.ArcGISRuntime.Mapping.BasemapStyle.ArcGISDarkGray);
            var car = new Uri($@"https://i.ibb.co/JBmgHbW/Bravo-car-Lightf.png");
            var converted = car.AbsoluteUri;
            PictureMarkerSymbol CarSymbol = new PictureMarkerSymbol(new Uri(converted));
            MenuToggleButton.IsChecked = true;
            var user = new Uri($@"https://i.ibb.co/MRG572N/Home.png");
            var converted2 = user.AbsoluteUri;
            PictureMarkerSymbol UserSymbol = new PictureMarkerSymbol(new Uri(converted2));

            MyMapView.LocationDisplay.CourseSymbol = CarSymbol;
            MyMapView.LocationDisplay.DefaultSymbol = UserSymbol;
            UserSymbol.Width = 40;
            UserSymbol.Height = 40;
            CarSymbol.Width = 35;
            CarSymbol.Height = 65;
            MyMapView.LocationDisplay.IsEnabled = true;
            DataContext = VM;
        }
    }

}
