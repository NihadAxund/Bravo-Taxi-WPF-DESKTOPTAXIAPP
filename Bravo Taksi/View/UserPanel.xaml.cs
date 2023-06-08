using Bravo_Taksi.Models;
using Bravo_Taksi.UserControls;
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

    public partial class UserPanel : Window
    {
        Users User { get; set; }
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern int MessageBoxTimeout(IntPtr hwnd, String text, String title, uint type, Int16 wLanguageId, Int32 milliseconds);
        private UserPanel()
        {

        }
        public UserPanel(Users user)
        {
            InitializeComponent();
            User = user;
 
            ListAdd(User.History);
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private  void ListAdd(List<History> his)
        {
            List<History> reverse = Enumerable.Reverse(his).ToList();
            MessageBoxTimeout((System.IntPtr)0, "", "", 0, 0, 1);
            foreach (History history in reverse)
            {
                UserInfo userinfo = new UserInfo(history);
                userinfo.Width = 700; userinfo.Height = 50;
                Listim.Items.Add(userinfo);
            }
            //for(int i = his.Count; i > 0;)
            //{
            //    i--;
            //    UserInfo userinfo = new UserInfo(his[i]);
            //    userinfo.Width = 700; userinfo.Height = 50;
            //    Listim.Items.Add(userinfo);
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();
        
    }
}
