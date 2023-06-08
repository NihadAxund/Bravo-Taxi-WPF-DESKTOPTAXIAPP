
using Bravo_Taksi.ViewModel;
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
using System.Windows.Shapes;


namespace Bravo_Taksi.View
{
    public partial class Login : Window
    {
        public LoginViewModel loginview  { get; set; }
        public Login()
        {
            InitializeComponent();
            loginview = new LoginViewModel(this);
            DataContext = loginview;
        }
    }
}
