using Bravo_Taksi.Auxiliary;
using Bravo_Taksi.Command;
using Bravo_Taksi.Models;
using Bravo_Taksi.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Bravo_Taksi.ViewModel
{
    public class AddDriversVM : ViewModelBase
    {
        public RelayCommand1 Btn_Command { get; set; }
        private AddDriveView ADV { get; set; }
        private Driver drivers { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; } = new ObservableCollection<Driver>();

        public AddDriversVM(AddDriveView aDV)
        {
            ADV = aDV;
            if (new FileInfo($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json").Length != 0)
                Drivers = JsonSerializer.Deserialize<ObservableCollection<Driver>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json"));
            Btn_Command = new RelayCommand1(Add, True);
        }
        private bool Driver_Name() => ADV.Drivers_name.Text.Length >= 3;
        private bool Driver_Surname() => ADV.Driver_surname.Text.Length >= 3;
        private bool Driver_Email()
        {
            if (Regex.IsMatch(ADV.Driver_MailAdress.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                return true;
            return false;
        }
        private bool Driver_Phone()
        {
            if (ADV.Driver_PN.Text.Length == 9 && Regex.IsMatch(ADV.Driver_PN.Text, @"^(50|51|55|70|77|99).*$"))
                return true;
            return false;
        }
        private bool Car_Model() => ADV.Car_Model.Text.Length >= 3;
        private bool Car_Color() => ADV.Car_color.Text.Length >= 3;
        private bool Car_Number() => ADV.Car_number.Text.Length >= 3;
        private bool Car_Vendor() => ADV.Car_Vendor.Text.Length >= 3;
        private bool Check()
         => Driver_Name() && Driver_Surname() && Driver_Phone() && Driver_Email() && Car_Color() && Car_Model() && Car_Number() && Car_Vendor();

        private bool True(object parametr) => Check();
        private void Add(object parameter)
        {
            if (FileFolder.Check_Driver(Drivers, ADV.Driver_PN.Text,ADV.Car_number.Text))
            {
                MessageBox.Show("This Driver already exists");
                return;
            }
            Random rnd = new Random();
            MessageBox.Show(AdminPanelViewModel.bakuBus.BUS.Count.ToString());
            int index = rnd.Next(0, AdminPanelViewModel.bakuBus.BUS.Count - 1);
            double num = Convert.ToDouble(AdminPanelViewModel.bakuBus.BUS[index].Attributes.LONGITUDE);
            double num2 = Convert.ToDouble(AdminPanelViewModel.bakuBus.BUS[index].Attributes.LATITUDE);
            Point pn = new Point(num, num2);
            Driver driver = new Driver(ADV.Drivers_name.Text, ADV.Driver_surname.Text, ADV.Driver_PN.Text, ADV.Car_Model.Text, ADV.Car_Vendor.Text, ADV.Car_number.Text, ADV.Car_color.Text, pn);
            Drivers.Add(driver);
            var TextJson = JsonSerializer.Serialize(Drivers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json", TextJson);
            ADV.M.AddDriver();
            ADV.Close();
        }

    }
}
