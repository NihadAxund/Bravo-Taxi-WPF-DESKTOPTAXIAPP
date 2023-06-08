using Bravo_Taksi.Auxiliary;
using Bravo_Taksi.Command;
using Bravo_Taksi.Models;
using Bravo_Taksi.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace Bravo_Taksi.ViewModel
{

    public class SignUpViewModel : ViewModelBase
    {
        private int _randomCode { get; set; }

        public RelayCommand1 SignUPComman { get; set; }
        public RelayCommand1 SingInComman { get; set; }
        public RelayCommand1 Send_command { get; set; }
        public RelayCommand1 Exit { get; set; }
        private SignUp signUp { get; set; }
        public List<Users> Users { get; set; } = new List<Users>();
        public SignUpViewModel(SignUp obj)
        {
            if (new FileInfo($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Users.json").Length != 0)
                Users = JsonSerializer.Deserialize<List<Users>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Users.json"));
            signUp = obj;
            Send_command = new RelayCommand1(Sends, OTPCanTrue);
            SignUPComman = new RelayCommand1(Show, CanTrue);
            SingInComman = new RelayCommand1(SignInMethod, True);
            Exit = new RelayCommand1(ExitMethod, True);
            Random _random = new Random();
            _randomCode = _random.Next(1000, 10000);

        }
        private bool Phone()
        {
            if (signUp.Phone_Txt.Text.Length==9 && Regex.IsMatch(signUp.Phone_Txt.Text, @"^(50|51|55|70|77|99).*$"))
            {
                signUp.Phone_Txt.Foreground = new SolidColorBrush(Colors.White);
                return true;
            }
            signUp.Phone_Txt.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }
        private bool Name()
        {
            if (signUp.Name_Txt.Text.Length >= 3)
            {
                signUp.Name_Txt.Foreground = new SolidColorBrush(Colors.White);
                return true;
            }
            signUp.Name_Txt.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }
        private bool SurName()
        {
            if (signUp.SName_Txt.Text.Length >= 3)
            {
                signUp.SName_Txt.Foreground = new SolidColorBrush(Colors.White);
                return true;
            }
            signUp.SName_Txt.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }
        private bool Password()
        {
            if (signUp.Password_Txt.Text.Length >= 6)
            {
                signUp.Password_Txt.Foreground = new SolidColorBrush(Colors.White);
                return true;
            }
            signUp.Password_Txt.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }
        private bool PasswordCopy()
        {
            if (Password() && signUp.PasswordC_Txt.Text == signUp.Password_Txt.Text)
            {
                signUp.PasswordC_Txt.Foreground = new SolidColorBrush(Colors.White);
                return true;
            }
            signUp.PasswordC_Txt.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }
        private void Num()
        {
            signUp.Email_Txt.IsReadOnly = true;
            signUp.Email_Txt.IsReadOnly = true;
        }
        private bool Email()
        {
            if (Regex.IsMatch(signUp.Email_Txt.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                signUp.Email_Txt.Foreground = new SolidColorBrush(Colors.White);
                return true;
            }
            signUp.Email_Txt.Foreground = new SolidColorBrush(Colors.Red);
            return false;
        }
        private bool Verify() => signUp.tbox_OTP.Text == _randomCode.ToString();
        private bool Check() =>  Name() && SurName() && PasswordCopy() && Email() && Phone() && Verify();
        private void LoginShow()
        {
            LoadingPanel LP = new LoadingPanel(1, 400, 500);
            LP.window = new Login();
            LP.Show(); signUp.Close();
        }
        private void SignInMethod(object parametr)
        {
            LoginShow();
        }
        private void ExitMethod(object parametr) => signUp.Close();
        private void Show(object parametr)
        {
           
            Users user = new Users(signUp.Name_Txt.Text, signUp.Password_Txt.Text, signUp.Email_Txt.Text);
            Users.Add(user);
            var TextJson = JsonSerializer.Serialize(Users, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Users.json", TextJson);
            LoginShow();
        }
        private bool EmailCheck(string email)
        {
            foreach (var item in Users)
                if (item.Email==email) return false;
            return true;
        }
        private void Sends(object parametr)
        {
            if (EmailCheck(signUp.Email_Txt.Text))
            {
                signUp.Email_Already.Visibility = Visibility.Hidden;
                Send();
                Num();
            }
            else signUp.Email_Already.Visibility = Visibility.Visible;
        }
        private void Send()
        {
            try
            {
                SmtpClient client = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "sendcsharpemail@gmail.com",
                        Password = "stnizrmeyrdmenot"
                    }
                };
                MailAddress from = new MailAddress("sendcsharpemail@gmail.com", "Bravo Taksi");
                MailAddress to = new MailAddress(signUp.Email_Txt.Text, "Someone");
                MailMessage Message = new MailMessage()
                {
                    From = from,
                    Subject = "Bravo Taksi otp code",
                    Body = "Your OTP code: " + _randomCode.ToString()
                };
                Message.To.Add(to);
                client.Send(Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool OTPCanTrue(object parametr) => Email();

        private bool CanTrue(object parametr) => Check();
        private bool True(object parametr) => true;
    }
}
