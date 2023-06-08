using Bravo_Taksi.Models;
using Bravo_Taksi.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Bravo_Taksi.View
{
    public partial class Star : Window
    {
        private double _ratingValue { get; set; } = 0;
        private bool _isOkay { get; set; }
        private string _telephoneNumber { get; set; }
        private Driver _Driver { get; set; }
        private Users _User { get; set; }
        //public Star()
        //{
        //    InitializeComponent();
        //}
        public Star(Driver driver,Users user)
        {
            InitializeComponent();
            _ratingValue = 0;
            _Driver = driver;
            _User = user;
        }
        private List<Comment> Comm { get; set; }
        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {

            switch (ratingbar.Value)
            {
                case 1:
                    _ratingValue = -20;
                    break;
                case 2:
                    _ratingValue = -10;
                    break;
                case 3:
                    _ratingValue = 5;
                    break;
                case 4:
                    _ratingValue = 10;
                    break;
                case 5:
                    _ratingValue = 20;
                    break;
                default:
                    _isOkay = false;
                    break;
            }
            MapViewModel.Num = _ratingValue;
            if (Comment_tbox.Text.Length!=0)
            {
                Comment comment = new Comment(_Driver,_User);
                comment.Comment_text = Comment_tbox.Text;
                comment.Users.History.Clear();
                if (new FileInfo($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Comments.json").Length != 0)
                {
                    var b = JsonSerializer.Deserialize<List<Comment>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Comments.json"));
                    Comm = b;
                }
                else Comm = new List<Comment>();
                Comm.Add(comment);
                 
                var TextJso1n = JsonSerializer.Serialize(Comm, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Comments.json", TextJso1n);
            }
            Close();
        }
    }
}
