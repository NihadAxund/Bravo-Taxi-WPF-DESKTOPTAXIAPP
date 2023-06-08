using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Navigation;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;
using Esri.ArcGISRuntime.UI;
using Color = System.Drawing.Color;
using Esri.ArcGISRuntime.Portal;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.UI.Controls;
using Bravo_Taksi.Command;
using Bravo_Taksi.View;
using Esri.ArcGISRuntime.Tasks.Geocoding;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Text.Json;
using Bravo_Taksi.Models;
using Bravo_Taksi.UserControls;
using System.Net.Mail;
using System.Net;

namespace Bravo_Taksi.ViewModel
{
    public class MapViewModel : INotifyPropertyChanged
    {


        private RouteTracker _tracker;


        private RouteResult _routeResult;
        private Route _route;

        private IReadOnlyList<DirectionManeuver> _directionsList;



        private Graphic _routeAheadGraphic;
        private Graphic _routeTraveledGraphic;

        public MapPoint PointOne;

        public MapPoint PointTwo;

        public NavigateRouteView View { get; set; }
        public string FullName { get; set; }
        private NavigateRouteView RealView { get; set; }
        public string Car_Number { get; set; }

        private readonly Uri _routingUri = new Uri("https://route-api.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World");

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        static public MapView MyMapView { get; set; }

        private bool _StartNavigationButtonIsEnabled;
        public string LOC1 { get; set; } = "Start Location";
        public MapPoint PointThree;
        public bool TripDetailsButtonIsEnabled
        {
            get { return _StartNavigationButtonIsEnabled; }
            set { _StartNavigationButtonIsEnabled = value; OnPropertyRaised(); }
        }
        private bool _searchNavigationButtonIsEnabled = true;

        public bool SearchNavigationButtonIsEnabled
        {
            get { return _searchNavigationButtonIsEnabled; }
            set { _searchNavigationButtonIsEnabled = value; OnPropertyRaised(); }
        }
        private bool _RecenterButtonIsEnabled;

        public bool RecenterButtonIsEnabled
        {
            get { return _RecenterButtonIsEnabled; }
            set { _RecenterButtonIsEnabled = value; OnPropertyRaised(); }
        }

        private string _MessagesTextBlockText;

        public string MessagesTextBlockText
        {
            get { return _MessagesTextBlockText; }
            set { _MessagesTextBlockText = value; OnPropertyRaised(); }
        }

        private string pointOneText;

        public string PointOneText
        {
            get { return pointOneText; }
            set { pointOneText = value; OnPropertyRaised(); }
        }

        private string pointTwoText;

        public string PointTwoText
        {
            get { return pointTwoText; }
            set { pointTwoText = value; OnPropertyRaised(); }
        }

        private string _priceText = "";

        public string PriceText
        {
            get { return _priceText; }
            set { _priceText = value; OnPropertyRaised(); }
        }


        static public MapViewModel CommandCreatedObject { get; set; }

        public RelayCommand1 MapViewCommand { get; set; }
        public RelayCommand1 SearchBtnClickCommand { get; set; }
        public RelayCommand1 HistoryButton { get; set; }
        public RelayCommand1 WindowClosingCommand { get; set; }
        public RelayCommand1 TripDetailsButtonIsCommand { get; set; }
        public RelayCommand1 RecenterButtonCommand { get; set; }
        public RelayCommand1 ViewLoadCommand { get; set; }
        public RelayCommand1 Exit_btn { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; } = JsonSerializer.Deserialize<ObservableCollection<Driver>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json"));

        private LocatorTask _geocoder;
        public RelayCommand1 BaseUserPanel { get; set; }
        public void InitTaxies(Driver DoNotShowThisDriver = null)
        {

            Assembly currentAssembly = GetType().GetTypeInfo().Assembly;

            var uri = new Uri($@"https://i.ibb.co/6XvPYPk/Bravo-car.png");

            var converted = uri.AbsoluteUri;
            PictureMarkerSymbol CabSymbol = new PictureMarkerSymbol(new Uri(converted));

            if (MyMapView.GraphicsOverlays == null) return;
            CabSymbol.Width = 32;
            CabSymbol.Height = 55;

            foreach (var d in Drivers)
            {
                if (DoNotShowThisDriver is null)
                    MyMapView.GraphicsOverlays[1].Graphics.Add(new Graphic(
                        new MapPoint(d.Location.X, d.Location.Y, SpatialReferences.Wgs84),
                        CabSymbol));
                else if (!(DoNotShowThisDriver is null))
                {
                    if (DoNotShowThisDriver != d)
                        MyMapView.GraphicsOverlays[1].Graphics.Add(new Graphic(
                            new MapPoint(d.Location.X, d.Location.Y, SpatialReferences.Wgs84),
                            CabSymbol));
                }

            }
        }
        public double Distance { get; set; }
        private void SendTripDetails()
        {
            try
            {
                SmtpClient client = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "sendcsharpemail@gmail.com",
                        Password = "stnizrmeyrdmenot"
                    }
                };
                MailAddress from = new MailAddress("sendcsharpemail@gmail.com", "Bravo Taksi");
                MailAddress to = new MailAddress(CurrentUser.Email, "Someone");
                MailMessage Message = new MailMessage()
                {
                    From = from,
                    Subject = "Bravo Taxi Azerbaijan",
                    Body = $"Your Bravo Taxi Trip on {DateTime.Today.DayOfWeek}\nThanks for riding with us, {CurrentUser.Username}!\nTotal fare:{_priceText}\n" +
                    $"From:{RealView.LOC1.Text}\nTo:{RealView.Loc2.Text}\nDriver:{SelectedDriver.Name} {SelectedDriver.Surname}\n"
                };
                Message.To.Add(to);
                client.Send(Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GpsResponse()
        {
            PointTwo = new MapPoint(MyMapView.LocationDisplay.Location.Position.X, MyMapView.LocationDisplay.Location.Position.Y, SpatialReferences.Wgs84);
        }
        public static Users CurrentUser { get; set; }

        private string userUsername;

        public string UserUsername
        {
            get { return userUsername; }
            set { userUsername = value; OnPropertyRaised(); }
        }

        private string userEmail;

        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value; OnPropertyRaised(); }
        }

        public MapViewModel(NavigateRouteView vms)
        {
            RealView = vms;
            FullName = CurrentUser.Username;
            WindowClosingCommand = new RelayCommand1(s =>
            {
                System.Environment.Exit(0);

            });
            Exit_btn = new RelayCommand1(s =>
            {
                RealView.Close();
            });
            HistoryButton = new RelayCommand1(s =>
            {
                var temp2 = CurrentUser;
               
            });
            BaseUserPanel = new RelayCommand1(s =>
            {

                UserPanel UP = new UserPanel(CurrentUser);
                
                UP.Show();
            });
            MapViewCommand = new RelayCommand1(s =>
            {
                MyMapView = s as MapView;
                MyMapView.GraphicsOverlays.Add(new GraphicsOverlay());
                MyMapView.GraphicsOverlays.Add(new GraphicsOverlay());


                UserUsername = CurrentUser.Username;
                UserEmail = CurrentUser.Email;

                Initialize();

                MyMapView.LocationDisplay.IsEnabled = true;

                bool isok1 = true;
                if (MyMapView.LocationDisplay.Started)
                {
                    try
                    {
                        PointTwo = new MapPoint(MyMapView.LocationDisplay.Location.Position.X, MyMapView.LocationDisplay.Location.Position.Y, SpatialReferences.Wgs84);
                    }
                    catch{ isok1 = false; }
                    if (isok1) LOC1 = "";

                    
                }
                else
                {
                    MessageBox.Show("Please enable your location settings to show current location or enter manually");
                }

                InitTaxies();
                GetCurrentPointAddressName();
            });

            

            RecenterButtonCommand = new RelayCommand1(s =>
            {
                MyMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Navigation;
            });

            TripDetailsButtonIsCommand = new RelayCommand1(s =>
            {
                AcceptDriver AD = new AcceptDriver(SelectedDriver);
                AD.BD.Price = _priceText;
                AD.ShowDialog();
                if (AD.IsOk)
                {
                    AD.IsOk = false;
                    Play();
                }
                
            });

            ViewLoadCommand = new RelayCommand1(s =>
            {
                View = s as NavigateRouteView;
            });
            
            SearchBtnClickCommand = new RelayCommand1(s =>
            {
                if (!(MyMapView is null))
                    if (!string.IsNullOrWhiteSpace(PointTwoText))
                    {
                        TripDetailsButtonIsEnabled = false;
                        PriceText = "";
                        RealView.popup_panel.Visibility = Visibility.Hidden;
                        Loccheck(false);
                        MyMapView.GraphicsOverlays.Clear();

                        Temp(); 
                        isok = true;
                    }
            });

            CommandCreatedObject = this;

        }
        private void Loccheck (bool a){
            RealView.LOC1.IsEnabled = a;
            RealView.Loc2.IsEnabled = a;
        }
        public string CurrentPlaceName { get; set; } = "Any Problem";
        public static Double Num { get; set; } = 0;
        public async void GetCurrentPointAddressName()
        {
            Uri Link = new Uri("https://geocode-api.arcgis.com/arcgis/rest/services/World/GeocodeServer");
            try
            {
                _geocoder = await LocatorTask.CreateAsync(Link);

                IReadOnlyList<GeocodeResult> addresses = await _geocoder.ReverseGeocodeAsync(PointTwo);
                GeocodeResult address = addresses.First();
                PointOneText = address.Attributes["Address"].ToString();
                CurrentPlaceName = address.Attributes["Address"].ToString();
                if (string.IsNullOrWhiteSpace(PointOneText))
                {
                    PointOneText = "   ";
                    CurrentPlaceName = "   ";
                }
            }
            catch
            {
                MessageBox.Show("location off");
            }
        }
        public Driver SelectedDriver { get; set; }
        public List<Graphic> Taxies { get; set; } = new List<Graphic>();
        public async void Temp()
        {
            try
            {
                MyMapView.GraphicsOverlays.Clear();
                Uri Link = new Uri("https://geocode-api.arcgis.com/arcgis/rest/services/World/GeocodeServer");
                _geocoder = await LocatorTask.CreateAsync(Link);


                if (CurrentPlaceName != PointOneText)
                {
                    IReadOnlyList<SuggestResult> suggestionsOne = await _geocoder.SuggestAsync(PointOneText);
                    SuggestResult firstSuggestion = suggestionsOne.First();
                    var addressesOne = await _geocoder.GeocodeAsync(firstSuggestion.Label);
                    var mapPointOne = addressesOne.First().DisplayLocation;
                    PointTwo = mapPointOne;
                }
                else
                    GetCurrentPointAddressName();
                IReadOnlyList<SuggestResult> suggestionsTwo = await _geocoder.SuggestAsync(PointTwoText);

                SuggestResult SecondSuggestion = suggestionsTwo.First();

                var addressesTwo = await _geocoder.GeocodeAsync(SecondSuggestion.Label);

                var mapPointTwo = addressesTwo.First().DisplayLocation;


                PointThree = mapPointTwo;

                List<double> driversdistance = new List<double>();
                for (int i = 0; i < Drivers.Count; i++)
                {
                    RouteTask routeTaskk = await RouteTask.CreateAsync(new Uri("https://route-api.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World"));

                    RouteParameters routeParamss = await routeTaskk.CreateDefaultParametersAsync();

                    routeParamss.ReturnDirections = true;
                    routeParamss.ReturnStops = true;
                    routeParamss.ReturnRoutes = true;
                    routeParamss.OutputSpatialReference = SpatialReferences.Wgs84;

                    Stop stopp1 = new Stop(new MapPoint(Drivers[i].Location.X, Drivers[i].Location.Y, SpatialReferences.Wgs84));
                    Stop stopp2 = new Stop(PointTwo);

                    List<Stop> stopPointss = new List<Stop> { stopp1, stopp2 };
                    routeParamss.SetStops(stopPointss);

                    RouteResult result = await routeTaskk.SolveRouteAsync(routeParamss);
                    Route route = result.Routes[0];

                    driversdistance.Add(route.TotalLength / 1000);

                }
                var Min = driversdistance[0];
                SelectedDriver = Drivers[0];
                for (int i = 0; i < driversdistance.Count; i++)
                {
                    if (driversdistance[i] < Min)
                    {
                        Min = driversdistance[i];
                        SelectedDriver = Drivers[i];
                    }
                }
                PointOne = new MapPoint(SelectedDriver.Location.X, SelectedDriver.Location.Y, SpatialReferences.Wgs84);

                RouteTask routeTask = await RouteTask.CreateAsync(_routingUri);

                RouteParameters routeParams = await routeTask.CreateDefaultParametersAsync();

                routeParams.ReturnDirections = true;
                routeParams.ReturnStops = true;
                routeParams.ReturnRoutes = true;
                routeParams.OutputSpatialReference = SpatialReferences.Wgs84;

                Stop stops = new Stop(PointOne);
                Stop stops1 = new Stop(PointTwo);
                Stop stops2 = new Stop(PointThree);

                List<Stop> stopPoints = new List<Stop> { stops, stops1, stops2 };
                routeParams.SetStops(stopPoints);

                _routeResult = await routeTask.SolveRouteAsync(routeParams);
                _route = _routeResult.Routes[0];

                Distance = _route.TotalLength / 1000;


                MyMapView.GraphicsOverlays.Clear();
                MyMapView.GraphicsOverlays.Add(new GraphicsOverlay());
                MyMapView.GraphicsOverlays.Add(new GraphicsOverlay());


                var user = new Uri($@"https://i.ibb.co/VvXWwjJ/User-Location.png");
                var converted2 = user.AbsoluteUri;
                var Location = new Uri($@"https://i.ibb.co/XZq0V3n/9-95878-png-location-location-icon-png-black.png");
                var converted = Location.AbsoluteUri;
                PictureMarkerSymbol UserSymbol = new PictureMarkerSymbol(new Uri(converted2));
                PictureMarkerSymbol LocationSym = new PictureMarkerSymbol(new Uri(converted));

             


                UserSymbol.Width = 45; UserSymbol.Height = 70;
                LocationSym.Width = 45; LocationSym.Height = 70;
                MyMapView.GraphicsOverlays[0].Graphics.Add(new Graphic(PointTwo, UserSymbol));
                MyMapView.GraphicsOverlays[0].Graphics.Add(new Graphic(PointThree, LocationSym));

                _routeAheadGraphic = new Graphic(_route.RouteGeometry) { Symbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.FromArgb(71, 181, 255), 6) };

                _routeTraveledGraphic = new Graphic { Symbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.FromArgb(223, 246, 255), 3) };

                MyMapView.GraphicsOverlays[0].Graphics.Add(_routeAheadGraphic);
                MyMapView.GraphicsOverlays[0].Graphics.Add(_routeTraveledGraphic);

                await MyMapView.SetViewpointGeometryAsync(_route.RouteGeometry, 100);

                TripDetailsButtonIsEnabled = true;

                var temp5 = (Distance * double.Parse(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\PricePer.json")));

                PriceText = $"{(double)((int)temp5) + ((double)((int)((((temp5 - (int)(temp5)) * 10)) / 10)))} AZN";
                InitTaxies();

                AcceptDriver AD = new AcceptDriver(SelectedDriver);
                AD.BD.Price = _priceText;
                AD.ShowDialog();
                if (AD.IsOk)
                {
                    AD.IsOk = false;
                    RealView.InfoPanel.Car_model.Content = SelectedDriver.CarVendor + " " + SelectedDriver.CarModel;
                    RealView.InfoPanel.Car_Number.Content = SelectedDriver.CarNumber;
                    RealView.InfoPanel.Car_Color.Content = SelectedDriver?.CarColor;
                    RealView.InfoPanel.Driver_info.Content = SelectedDriver.Name + " " + SelectedDriver.Surname;
                    RealView.InfoPanel.Driver_Tel.Content = SelectedDriver.Phone;
                    RealView.InfoPanel.Price.Content = _priceText;
                    RealView.InfoPanel.Visibility = Visibility.Visible;
                    Loccheck(true);
                    if (RealView.LOC1.Text.Length == 0) RealView.InfoPanel.Start.Content = "Home";
                    else RealView.InfoPanel.Start.Content = RealView.LOC1.Text;
                    RealView.InfoPanel.Fin.Content = RealView.Loc2.Text;
                    RealView.popup_panel.Visibility = Visibility.Hidden;
                    Loccheck(false);
                    Play();
                }
                else
                {
                    RealView.popup_panel.Visibility = Visibility.Visible;
                    Loccheck(true);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MyMapView.GraphicsOverlays.Clear();
                MyMapView.GraphicsOverlays.Add(new GraphicsOverlay());
                MyMapView.GraphicsOverlays.Add(new GraphicsOverlay());
                RealView.popup_panel.Visibility = Visibility.Visible;
                Loccheck(true);
                InitTaxies();
            }
        }

        public async void Initialize()
        {
            try
            {
                MyMapView.Unloaded += SampleUnloaded;
                MyMapView.Map = new Esri.ArcGISRuntime.Mapping.Map(Esri.ArcGISRuntime.Mapping.BasemapStyle.ArcGISDarkGray);
                ArcGISPortal portal = await ArcGISPortal.CreateAsync();

                PortalItem mapItem = await PortalItem.CreateAsync(portal, "41281c51f9de45edaf1c8ed44bb10e30");

                Esri.ArcGISRuntime.Mapping.Map map = (new Esri.ArcGISRuntime.Mapping.Map(Esri.ArcGISRuntime.Mapping.BasemapStyle.ArcGISDarkGray));

                map.InitialViewpoint = new Viewpoint(40.409264, 49.867092, 100000);
                
                MyMapView.Map = map;

            }
            catch (Exception e)
            {
                if (e.Message != "Value cannot be null.\r\nParameter name: point")
                    MessageBox.Show(e.Message, "Error");
            }
        }

        private void Play()
        {
            try
            {

   
                MyMapView.GraphicsOverlays[1].Graphics.Clear();
                InitTaxies(SelectedDriver);

                NavigateRouteView.IsNagivateStart = false;

                TripDetailsButtonIsEnabled = false;
                SearchNavigationButtonIsEnabled = false;

                _directionsList = _route.DirectionManeuvers;

                _tracker = new RouteTracker(_routeResult, 0, true);

                _tracker.TrackingStatusChanged += TrackingStatusUpdated;

                MyMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Navigation;
                MyMapView.LocationDisplay.AutoPanModeChanged += AutoPanModeChanged;

               
                var simulationParameters = new SimulationParameters(DateTimeOffset.Now, 300);
                var simulatedDataSource = new SimulatedLocationDataSource();
                simulatedDataSource.SetLocationsWithPolyline(_route.RouteGeometry, simulationParameters);
                MyMapView.LocationDisplay.DataSource = new RouteTrackerDisplayLocationDataSource(simulatedDataSource, _tracker);
                MyMapView.LocationDisplay.IsEnabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"ERROR MESSAGE:{e.Message}\nYou cannot reach to this location", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TripDetailsButtonIsEnabled = true;
                SearchNavigationButtonIsEnabled = true;
            }
        }
        public int DestinationCounter { get; set; } = 0;
        public List<Users> Users { get; set; } = JsonSerializer.Deserialize<List<Users>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Users.json"));
        private bool isok = true;
        double kmcount = 0;
     
          double num1 = 50; double num2 = 40;
        private void TrackingStatusUpdated(object sender, RouteTrackerTrackingStatusChangedEventArgs e)
        {
            TrackingStatus status = e.TrackingStatus;
            if (isok)
            {
                kmcount = Convert.ToDouble(status.RouteProgress.RemainingDistance.DisplayText);
                if (kmcount < num1&& kmcount < num2)
                {
                    num1 = num1;
                }
                else
                {
                    num1 = 500; num2 = 450;
                    if ((kmcount > num1 && kmcount > num2))
                    {
                        num1 = 1; num2=1;      
                    }
                }
                isok = false;
            }
            System.Text.StringBuilder statusMessageBuilder = new System.Text.StringBuilder("Route Status:\n");
            double a = Convert.ToDouble(status.RouteProgress.RemainingDistance.DisplayText);
            if ((a != num1 && a!= num2) && (status.DestinationStatus == DestinationStatus.NotReached || status.DestinationStatus == DestinationStatus.Approaching))
            {

                statusMessageBuilder.AppendLine("Time remaining: " +
                                                status.RouteProgress.RemainingTime.ToString(@"hh\:mm\:ss"));
              

                if (status.CurrentManeuverIndex + 1 < _directionsList.Count)
                {
                    statusMessageBuilder.AppendLine("Next direction: " + _directionsList[status.CurrentManeuverIndex + 1].DirectionText);
                }

                _routeAheadGraphic.Geometry = status.RouteProgress.RemainingGeometry;
                _routeTraveledGraphic.Geometry = status.RouteProgress.TraversedGeometry;
                SearchNavigationButtonIsEnabled = false;

            }
            else if ((a == num1 || a == num2) || status.DestinationStatus == DestinationStatus.Reached)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = $@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\Resources\MyCarMusic.wav";
                statusMessageBuilder.AppendLine("Destination reached.");
                SearchNavigationButtonIsEnabled = true;
                DestinationCounter++;
               
                if (((a == num1 || a == num2)) || DestinationCounter >= 2 )
                {
                    SelectedDriver.Location = new Point(PointThree.X, PointThree.Y);
                  
                    double Price = 0;
                    if (!string.IsNullOrWhiteSpace(PriceText))
                    {
                        var temp = (Distance * double.Parse(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\PricePer.json")));
                        var temp2 = temp - (int)temp;
                        temp2 = temp2 * 100;
                        temp2 = (int)temp2;
                       
                        temp2 = temp2 / 100;

                        temp = (int)temp;

                        SelectedDriver.Balance += temp + temp2;
                        int nums = Convert.ToInt32(temp + temp2);
                        Price = nums;
                        SelectedDriver.CompanyBenefit = SelectedDriver.Balance * 15 / 100;

                        SelectedDriver.DriverBenefit = SelectedDriver.Balance - SelectedDriver.CompanyBenefit;
                    }
                    SelectedDriver.CountTravel += 1;

                    DestinationCounter = 0;
                    _tracker.TrackingStatusChanged -= TrackingStatusUpdated;

                    foreach (var item in Users)
                    {
                        if (item.Username == CurrentUser.Username)
                        {
                            item.History.Add(new History
                            {
                                DriverName = SelectedDriver.Name,
                                DriverSurname = SelectedDriver.Surname,
                                Price = Price,
                                PointOneText = PointOneText,
                                PointTwoText = PointTwoText
                            });
                            CurrentUser = item;
                            break;
                        }
                    }

                    var TextJso1n = JsonSerializer.Serialize(Users, new JsonSerializerOptions() { WriteIndented = true });
                    File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Users.json", TextJso1n);


                    View.Dispatcher.Invoke(() =>
                    {
                        MyMapView.GraphicsOverlays[1].Graphics.Clear();
                        InitTaxies();
                        MyMapView.SetViewpointRotationAsync(0);
                        _routeAheadGraphic.Geometry = null;
                        _routeTraveledGraphic.Geometry = status.RouteResult.Routes[0].RouteGeometry;
                        MyMapView.LocationDisplay.DataSource.StopAsync();
                        Star LP = new Star(SelectedDriver,CurrentUser);
                        LP.ShowDialog();
                        if (Num!=0) SelectedDriver.Rating += Num;                        
                        RealView.popup_panel.Visibility = Visibility.Visible;
                        Loccheck(true);
                        var TextJson = JsonSerializer.Serialize(Drivers, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json", TextJson);
                        SendTripDetails();

                    });
                }
                player.Play();

                _routeAheadGraphic.Geometry = null;
                _routeTraveledGraphic.Geometry = status.RouteResult.Routes[0].RouteGeometry;
               
                if (status.RemainingDestinationCount > 1)
                {
                    _tracker.SwitchToNextDestinationAsync();
                }
                else
                {
                    View.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        MyMapView.LocationDisplay.DataSource.StopAsync();
                    });
                }
            }

            View.Dispatcher.BeginInvoke((Action)delegate ()
            {
                if (status.RouteProgress.RemainingDistance.DisplayText == num1.ToString() || status.RouteProgress.RemainingDistance.DisplayText == num2.ToString()) { RealView.KM_LBL.Content = "0 \\km";  }
                else  RealView.KM_LBL.Content = status.RouteProgress.RemainingDistance.DisplayText + " \\KM";
                MessagesTextBlockText = statusMessageBuilder.ToString();
            });
        }



        private void AutoPanModeChanged(object sender, LocationDisplayAutoPanMode e)
        {
            RecenterButtonIsEnabled = e != LocationDisplayAutoPanMode.Navigation;
        }

        private void SampleUnloaded(object sender, RoutedEventArgs e)
        {
            if (_tracker != null)
            {
                _tracker.TrackingStatusChanged -= TrackingStatusUpdated;
                _tracker = null;
            }

            MyMapView.LocationDisplay?.DataSource?.StopAsync();
        }
    }

    public class RouteTrackerDisplayLocationDataSource : LocationDataSource
    {
        private LocationDataSource _inputDataSource;
        private RouteTracker _routeTracker;

        public RouteTrackerDisplayLocationDataSource(LocationDataSource dataSource, RouteTracker routeTracker)
        {
            _inputDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));

            _routeTracker = routeTracker ?? throw new ArgumentNullException(nameof(routeTracker));

            _inputDataSource.LocationChanged += InputLocationChanged;

            _routeTracker.TrackingStatusChanged += TrackingStatusChanged;
        }

        private void InputLocationChanged(object sender, Location e)
        {
            _routeTracker.TrackLocationAsync(e);
        }

        private void TrackingStatusChanged(object sender, RouteTrackerTrackingStatusChangedEventArgs e)
        {
            if (e.TrackingStatus.DisplayLocation != null)
            {
                UpdateLocation(e.TrackingStatus.DisplayLocation);
            }
        }

        protected override Task OnStartAsync() => _inputDataSource.StartAsync();

        protected override Task OnStopAsync() => _inputDataSource.StartAsync();
    }
}
