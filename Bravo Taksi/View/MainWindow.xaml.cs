using Bravo_Taksi.View;
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
using System.Windows.Threading;

namespace Bravo_Taksi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool AutoStart { get; set; } = false;
        private int num =100;
        private DispatcherTimer timer = new DispatcherTimer();
        public void Sample()
        {
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer_Tick;
            timer.Start();
            AutoStart = true;
        }
        private void CloseMethod()
        {
            timer.Stop();
            
            LoadingPanel Loading = new LoadingPanel(1,this.Height,this.Width);
            Loading.window = new Login();
            Loading.Show();
            this.Close();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if(!AutoStart)CloseMethod();
            pbStatus.Value += num;
            num += num-(num-1);
            if (pbStatus.Value>=100)AutoStart = false;
            else if (pbStatus.Value >= 50) TxtB.Foreground = new SolidColorBrush(Colors.Black);
        }
        public MainWindow()
        {
            InitializeComponent();
            Sample();
        }
    }
}
