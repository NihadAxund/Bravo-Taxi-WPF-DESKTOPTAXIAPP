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
    public partial class ForgetPassword : Window
    {
        private PasswordVM PVM { get; set; }
        public ForgetPassword()
        {
            InitializeComponent();
            PVM = new PasswordVM (this);
            DataContext = PVM;

        }
    }
}
