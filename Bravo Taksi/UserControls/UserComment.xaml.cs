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
    /// Interaction logic for UserComment.xaml
    /// </summary>
    public partial class UserComment : UserControl
    {
        public UserComment()
        {
            InitializeComponent();
        }
        public UserComment(Comment comment)
        {
            InitializeComponent();
            Email_lbl.Content = comment.Users.Email; CarNumber_lbl.Content = comment.Driver.CarNumber;
           Name_lbl.Content = comment.Driver.Name; Comment_lbl.Text = comment.Comment_text;
            Surname_lbl.Content = comment.Driver.Surname;
        }
    }
}
