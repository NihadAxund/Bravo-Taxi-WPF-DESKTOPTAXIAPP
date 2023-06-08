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
    public partial class DriverRaiting : UserControl
    {
        public DriverRaiting(Driver driver)
        {
            InitializeComponent();
            Name_lbl.Content = driver.Name; Surname_lbl.Content = driver.Surname;
            Car_Number.Content = driver.CarNumber;
            Function(driver.Rating);
        }
        private void Function (double Raiting){
            if (Raiting<=-20)
            {
                slider.Foreground = new SolidColorBrush(Colors.Red);
                slider.Value = -20;
            }
            else if (Raiting<=-10)
            {
                slider.Foreground = new SolidColorBrush(Colors.OrangeRed);
                slider.Value= -10;
            }
            else if (Raiting <= 0)
            {
                slider.Foreground = new SolidColorBrush(Colors.DarkOrange);
                slider.Value= 0;
            }
            else if (Raiting >=20)
            {
                slider.Foreground = new SolidColorBrush(Colors.Green);
                slider.Value = 20;
            }
            else if(Raiting >= 10)
            {
                slider.Foreground = new SolidColorBrush(Colors.Yellow);
                slider.Value= 10;
            }
            else if (Raiting >= 5)
            {
                slider.Foreground = new SolidColorBrush(Colors.Orange);
                slider.Value = 5;
            }
            else if (Raiting<5 && Raiting >=0)
            {
                slider.Foreground = new SolidColorBrush(Colors.DarkOrange);
                slider.Value = 0;
            }
        }
    }
}
