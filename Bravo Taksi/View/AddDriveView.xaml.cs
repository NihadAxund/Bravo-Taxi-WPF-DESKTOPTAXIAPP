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
    /// Interaction logic for AddDriveView.xaml
    /// </summary>
    public partial class AddDriveView : Window
    {
        private AddDriversVM ADVM {get;set;}
        public AdminPanelViewModel M { get; set; }
        public AddDriveView()
        {
            InitializeComponent();
            ADVM = new AddDriversVM(this);
            DataContext = ADVM;
        }
        public AddDriveView(AdminPanelViewModel vm)
        {
            InitializeComponent();
            ADVM = new AddDriversVM(this);
            M = vm;
            DataContext = ADVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>this.Close();
        

        //private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    this.Hide();
        //}
    }
}
