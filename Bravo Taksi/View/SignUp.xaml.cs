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

namespace Bravo_Taksi.View
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        //public string PhoneNumber { get; set; } = "31312321321";
        //public string BtnText = "Nihad";
        public SignUpViewModel SUVM { get; set; }
        public SignUp()
        {
            
            InitializeComponent();
            SUVM = new SignUpViewModel(this);
            DataContext = SUVM;
        }
    }
}
