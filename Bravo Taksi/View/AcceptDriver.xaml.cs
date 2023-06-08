using Bravo_Taksi.Models;
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
    /// Interaction logic for AcceptDriver.xaml
    /// </summary>
    public partial class AcceptDriver : Window
    {
        public Bravo_Driver BD { get; set; }
        public bool IsOk = false;
        public AcceptDriver()
        {
            InitializeComponent();
            BD = new Bravo_Driver(this);
            DataContext = BD;
        }
        public AcceptDriver(Driver Driver)
        {
            InitializeComponent();
            BD = new Bravo_Driver(this,Driver);
            DataContext = BD;
        }
    }
}
