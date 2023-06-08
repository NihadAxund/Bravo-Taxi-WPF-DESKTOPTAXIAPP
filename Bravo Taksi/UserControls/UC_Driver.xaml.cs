using Bravo_Taksi.Models;
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

namespace Bravo_Taksi.UserControls
{
    public partial class UC_Driver : UserControl
    {
        public string FirstName { get; set; } = nameof(Name);
        public string Surname { get; set; } = nameof(Surname);
        public string PhoneNumber { get; set; } = nameof(PhoneNumber);
        public string CarVendor { get; set; } = nameof(CarVendor);
        public string CarModel { get; set; } = nameof(CarModel);
        public string CarNumber { get; set; } = nameof(CarNumber);
        public string CarColor { get; set; } = nameof(CarColor);


        public UC_Driver(Driver driver)
        {
            InitializeComponent();
            FirstName = driver.Name;
            Surname = driver.Surname;
            PhoneNumber = driver.Phone;
            CarVendor = driver.CarVendor;
            CarModel = driver.CarModel;
            CarNumber = driver.CarNumber;
            CarColor = driver.CarColor;
            DataContext = this;

        }
    }
}
