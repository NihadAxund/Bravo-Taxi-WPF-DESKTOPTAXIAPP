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
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : UserControl
    {
        public UserInfo(History his)
        {
            InitializeComponent();
            if (his.PointOneText==" ") Start_loc.Content = "-- --";
            else  Start_loc.Content = his.PointOneText;
            Finish_loc.Content= his.PointTwoText;
            Price_lbl.Content = his.Price + " ₼";
            Driver_Name.Content = his.DriverName;
            Driver_Surname.Content = his.DriverSurname;
        }
    }
}
