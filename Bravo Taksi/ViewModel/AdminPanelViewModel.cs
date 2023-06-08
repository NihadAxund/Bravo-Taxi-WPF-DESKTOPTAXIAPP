using Bravo_Taksi.Auxiliary;
using Bravo_Taksi.Command;
using Bravo_Taksi.Models;
using Bravo_Taksi.UserControls;
using Bravo_Taksi.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace Bravo_Taksi.ViewModel
{
    public class Attributes
    {
        public string BUS_ID { get; set; }
        public string PLATE { get; set; }
        public string DRIVER_NAME { get; set; }
        public string PREV_STOP { get; set; }
        public string CURRENT_STOP { get; set; }
        public string SPEED { get; set; }
        public string BUS_MODEL { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string ROUTE_NAME { get; set; }
        public int LAST_UPDATE_TIME { get; set; }
        public string DISPLAY_ROUTE_CODE { get; set; }
    }

    public class Bus
    {
        [JsonPropertyName("@attributes")]
        public Attributes Attributes { get; set; }
    }
    public class BakuBus
    {
        public ObservableCollection<Bus> BUS { get; set; }
    }
    public class AdminPanelViewModel
    {
        public static BakuBus bakuBus = new BakuBus();
        public AdminPanelView APV { get; set; }
        public RelayCommand1 Del { get; set; }
        public RelayCommand1 Update_Binding { get; set; }
        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        private ObservableCollection<Driver> Drivers { get; set; }
        
        public static async void Method()
        {
            HttpClient client = new HttpClient();
            var jsonString = await client.GetStringAsync("https://www.bakubus.az/az/ajax/apiNew1");
            bakuBus = JsonSerializer.Deserialize<BakuBus>(jsonString);
        }
        private void AddComment()
        {
            var text = new FileInfo($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Comments.json");
            if (text.Length == 0) return;
            APV.List_Comment.Items.Clear();
            var comment = JsonSerializer.Deserialize<List<Comment>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Comments.json"));
         //   MessageBox.Show(comment.Count.ToString());
            foreach (var item in comment)
            {
                UserComment us = new UserComment(item);
                us.Width = 800;
                APV.List_Comment.Items.Add(us);
            }
        }
        public void AddDriver()
        {

            var info = new FileInfo($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json");
            if (info.Length == 0) return;
            APV.DriverListView.Items.Clear();
            APV.List_Rainting.Items.Clear();
            Drivers = JsonSerializer.Deserialize<ObservableCollection<Driver>>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json"));
            foreach (var driver in Drivers)
            {
                UC_Driver control = new UC_Driver(driver);
                DriverRaiting DR = new DriverRaiting(driver);
                APV.List_Rainting.Items.Add(DR);
                APV.DriverListView.Items.Add(control);
            }
        }
        public void CanAdd(object parametr)
        {
            double num = Convert.ToDouble( APV.Price_add.Text);
            var TextJson = JsonSerializer.Serialize(num, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\PricePer.json", TextJson);
            Price_TextBox();
        }
        public void CanDel(object parametr)
        {
            List<string> arr = new List<string>();

            try
            {
                foreach (var item in APV.DriverListView.SelectedItems)
                {
                        UC_Driver driver  = item as UC_Driver;
                       arr.Add(driver.PhoneNumber);
                }
                if (arr.Count!=0)
                {

                    ObservableCollection<Driver> Drivers2 = new ObservableCollection<Driver>();
                    foreach (var item in Drivers)
                    {
                        bool isok = true;
                        foreach (var item2 in arr)if (item.Phone == item2)isok = false;
                        if (isok)Drivers2.Add(item);
                    }
                    Drivers = Drivers2;
                }
                
                var TextJson = JsonSerializer.Serialize(Drivers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\Drivers.json", TextJson);
                AddDriver();
            }
            catch { }
        }
        private void Price_TextBox()
        {
            var text = new FileInfo($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\PricePer.json");
            if (text.Length == 0) return;
            double num = JsonSerializer.Deserialize<double>(File.ReadAllText($@"C:\Users\{Environment.UserName}\source\repos\Bravo-Taksison\Bravo Taksi\FileFolder\PricePer.json"));
            APV.Price_TectBox.Text = num.ToString("0.00");
        }
        public bool CanDel2(object parametr)
        {
            return APV.DriverListView.SelectedItems!=null;
        }
        public bool AddBoolen(object parametr)
        {
            if (APV.Price_add.Text.Length == 0) return false;
            else
            {
                try
                {

                    var num = Convert.ToDouble(APV.Price_add.Text);
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
        public AdminPanelViewModel(AdminPanelView aPV)
        {
            Method();
            APV = aPV;
            Del = new RelayCommand1(CanDel, CanDel2);
            Update_Binding = new RelayCommand1(CanAdd, AddBoolen);
            AddDriver();
            Calculation();
            AddComment();
            Price_TextBox();
        }
        private void Calculation()
        {
            double Price1 = 0; double Price2 = 0;
            foreach (var item in Drivers)
            {
               Price1 += item.Balance;
               Price2 += item.CompanyBenefit;
            }
            this.Price1 = Price1.ToString()+ " ₼";
            this.Price2 =(Price1 - Price2).ToString() + " ₼";
            this.Price3 = Price2.ToString() + " ₼";
        }
    }
}
