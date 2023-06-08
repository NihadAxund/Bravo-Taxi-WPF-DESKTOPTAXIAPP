using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bravo_Taksi.View
{
    public partial  class LoadingPanel : Window
    {
        public Window window { get; set; }
        public double num { get; set; }
        private DispatcherTimer timer = new DispatcherTimer();
        public LoadingPanel()
        {
            InitializeComponent();
            Sample();
            DataContext = this;
        }
        public LoadingPanel(double num1,double Height, double Width)
        {
            this.Height = Height; this.Width = Width;
            if (num1 <= 0) num1 = 0.5;
            num = num1;
            InitializeComponent();
            Sample();
            DataContext = this;
        }
        public void Sample()
        {
            timer.Interval = TimeSpan.FromSeconds(num);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
            
            window.ShowDialog();
        }
    }
}
