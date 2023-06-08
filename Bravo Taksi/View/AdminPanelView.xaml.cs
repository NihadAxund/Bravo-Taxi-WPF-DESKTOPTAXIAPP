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
    /// Interaction logic for AdminPanelProject.xaml
    /// </summary>
    public partial class AdminPanelView : Window
    {
        public ViewModel.AdminPanelViewModel APVM { get; set; }
        public AdminPanelView()
        {
            InitializeComponent();
            APVM = new AdminPanelViewModel(this);
            DataContext = APVM;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           AddDriveView addDriver = new AddDriveView(APVM);

            addDriver.Show();
       
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
