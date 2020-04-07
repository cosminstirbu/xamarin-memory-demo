using System;
using System.Linq;
using CoreLocation;
using Foundation;
using MapKit;
using UIKit;

namespace MemoryDemo.iOS
{
    public partial class ItemNewViewController : UIViewController, IMKMapViewDelegate
    {
        private CLLocationManager _locationManager = new CLLocationManager();

        public ItemsViewModel ViewModel { get; set; }

        public ItemNewViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // The view controller leaks because of TouchUpInside (as expected) but if you comment it
            // it doesn't leak for LocationsUpdated and UpdatedHeading (when it actually should leak)

            // btnSaveItem.TouchUpInside += SaveButtonPressed;

            _locationManager.RequestWhenInUseAuthorization();
            _locationManager.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;

            _locationManager.LocationsUpdated += LocationsUpdated;
            _locationManager.UpdatedHeading += HeadingChanged;

            _locationManager.StartUpdatingLocation();
            _locationManager.StartUpdatingHeading();

            // If you set the delegate from C# then in causes a circular reference, but if you set it from the storyboard, it doesn't.
            // I guess that's expected since the delegate was set only in Obj-C runtime
            // MapView.Delegate = this;
        }

        void SaveButtonPressed(object sender, EventArgs e)
        {
            var item = new Item
            {
                Text = txtTitle.Text,
                Description = txtDesc.Text
            };
            ViewModel.AddItemCommand.Execute(item);
            NavigationController.PopToRootViewController(true);
        }

        [Export("mapView:regionDidChangeAnimated:")]
        public void RegionChanged(MKMapView mapView, bool animated)
        {
            txtDesc.Text = $"MapRegion : {mapView.VisibleMapRect}";
        }

        private void HeadingChanged(object sender, CLHeadingUpdatedEventArgs e)
        {
            txtTitle.Text = $"Heading : {e.NewHeading}";
        }

        private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            txtDesc.Text = $"Location : {e.Locations.First().Coordinate}";
        }
    }
}
