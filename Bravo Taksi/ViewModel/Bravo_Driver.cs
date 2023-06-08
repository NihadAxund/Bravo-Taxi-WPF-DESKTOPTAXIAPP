using Bravo_Taksi.Command;
using Bravo_Taksi.Models;
using Bravo_Taksi.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bravo_Taksi.ViewModel
{
    public class Bravo_Driver : ViewModelBase
    {
        public RelayCommand1 Accept { get; set; }

        public RelayCommand1 Reject { get; set; }
        private AcceptDriver DriverView { get; set; }
        private Driver drivers { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string CarNumber { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
        public Bravo_Driver(AcceptDriver aDV)
        {
            DriverView = aDV;
            Accept = new RelayCommand1(Add, True);
            Reject = new RelayCommand1(Remove, True);
        }
        public Bravo_Driver(AcceptDriver aDV,Driver D)
        {
            DriverView = aDV;
            Accept = new RelayCommand1(Add, True);
            Reject = new RelayCommand1(Remove, True);
            Name = D.Name; Surname = D.Surname; Phone = D.Phone;
            Vendor = D.CarVendor; Model = D.CarModel; Color = D.CarColor;
            CarNumber = D.CarNumber;
        }
        private bool True(object parametr) => true;
        private void Add(object parameter)
        {
            DriverView.IsOk = true;
            DriverView.Close();
        }
        private void Remove(object parameter)
        {
            DriverView.Close();
        }
    }
}
