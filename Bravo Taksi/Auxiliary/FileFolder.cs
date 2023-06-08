using Bravo_Taksi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Bravo_Taksi.Auxiliary
{
    public class FileFolder
    {
        public static bool Check_Driver(ObservableCollection<Driver> Drivers, string Tel,string Num)
        {
            foreach (var item in Drivers) if (item.Phone == Tel|| item.CarNumber==Num) return true;
            return false;
        }
        //    private static string[] FileName = new string[3] { "Admin.bin", "User.bin", "Driver.bin" };
        //    public static ObservableCollection<Driver>  JsonRemoveDriver(string Phone, ObservableCollection<Driver> Drivers)
        //    {
        //        foreach (var item in Drivers)
        //           if (item.Phone == Phone) Drivers.Remove(item);
        //        return Drivers;
        //    }
        //    //public static void BinWrite(User user, int n)
        //    //{
        //    //    string Filename = FileName[n];
        //    //    using (FileStream fs = new FileStream(Filename, FileMode.Append, FileAccess.Write))
        //    //    {
        //    //        using (BinaryWriter br = new BinaryWriter(fs))
        //    //        {
        //    //            br.Write(user.Email); br.Write(user.Password);
        //    //        }
        //    //    }
        //    //}
        //public static bool BinDriverRead2(int n, string email, string phone)
        //{
        //    string Filename = "A";

        //    using (FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        using (BinaryReader br = new BinaryReader(fs))
        //        {
        //            for (int i = 0; i < fs.Length;)
        //            {
        //                string Name = br.ReadString();
        //                string Surname = br.ReadString();
        //                string DriverNumber = br.ReadString();
        //                if (DriverNumber == phone) return false;
        //                string Email = br.ReadString();
        //                if (Email == email) return false;
        //                string CarVendor = br.ReadString();
        //                string CarModel = br.ReadString();
        //                string CarNumber = br.ReadString();
        //                string CarColor = br.ReadString();
        //                i += Name.Length + Surname.Length + DriverNumber.Length + Email.Length + CarVendor.Length + CarModel.Length + CarNumber.Length
        //                    + CarColor.Length + 8;
        //            }
        //        }
        //    }
        //    return true;
        //}


        //    public static List<Driver> BinDriverRead(int n)
        //    {
        //        string Filename = FileName[n];
        //        List<Driver> drivers = new List<Driver>();
        //        using (FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Read))
        //        {
        //            using (BinaryReader br = new BinaryReader(fs))
        //            {
        //                for (int i = 0; i < fs.Length;)
        //                {
        //                    string Name = br.ReadString();
        //                    string Surname = br.ReadString();
        //                    string DriverNumber = br.ReadString();
        //                    string Email = br.ReadString();
        //                    string CarVendor = br.ReadString();
        //                    string CarModel = br.ReadString();
        //                    string CarNumber = br.ReadString();
        //                    string CarColor = br.ReadString();
        //                    i += Name.Length + Surname.Length + DriverNumber.Length + Email.Length + CarVendor.Length + CarModel.Length + CarNumber.Length
        //                        + CarColor.Length + 8;
        //                }
        //            }
        //        }
        //        return drivers;
        //    }
        //    public static void BinDriverWrite(Driver driver, int n)
        //    {
        //        string Filename = FileName[n];
        //        using (FileStream fs = new FileStream(Filename, FileMode.Append, FileAccess.Write))
        //        {
        //            using (BinaryWriter br = new BinaryWriter(fs))
        //            {
        //                br.Write(driver.Name);
        //                br.Write(driver.Surname);
        //                //br.Write(driver.DriverNumber);
        //                //br.Write(driver.Email);
        //                br.Write(driver.CarVendor);
        //                br.Write(driver.CarModel);
        //                br.Write(driver.CarNumber);
        //                br.Write(driver.CarColor);
        //            }
        //        }
        //    }

        //    public static void CopyDriver(string Email)
        //    {

        //        int index = 2;
        //        string CopyDriver = "copydriver.bin";
        //        using (FileStream fs = new FileStream(CopyDriver, FileMode.Create, FileAccess.Write))
        //        {
        //            using (BinaryWriter br = new BinaryWriter(fs))
        //            {
        //                using (FileStream fs2 = new FileStream(FileName[2], FileMode.Open, FileAccess.Read))
        //                {
        //                    using (BinaryReader br2 = new BinaryReader(fs2))
        //                    {
        //                        for (int i = 0; i < fs2.Length;)
        //                        {

        //                                string Names = br2.ReadString();
        //                                i += Names.Length + 1;
        //                                string surname = br2.ReadString();
        //                                i += surname.Length + 1;
        //                                string DriverP = br2.ReadString();
        //                                i += DriverP.Length + 1;
        //                                string text = br2.ReadString();
        //                                i += text.Length + 1;
        //                                if (text == Email)
        //                                {
        //                                    for (int x = 0; x < 4; x++)
        //                                    {
        //                                        string text2 = br2.ReadString();
        //                                        i += text2.Length + 1;
        //                                    }

        //                                }
        //                                else
        //                                {
        //                                    br.Write(Names);
        //                                    br.Write(surname);
        //                                    br.Write(DriverP);
        //                                    br.Write(text);

        //                                    for (int x = 0; x < 4; x++)
        //                                    {
        //                                        string text2 = br2.ReadString();
        //                                        i += text2.Length + 1;
        //                                        br.Write(text2);
        //                                    }
        //                                }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        using (FileStream fs2 = new FileStream(FileName[index], FileMode.Create, FileAccess.Write)) { }
        //        File_Save(CopyDriver, 2, 8);
        //        ;
        //    }

        //    public static void NewPassword(string Email, string Password)
        //    {

        //        int index = 1;
        //        using (FileStream fs = new FileStream("CopyBin.bin", FileMode.Create, FileAccess.Write))
        //        {
        //            using (BinaryWriter br = new BinaryWriter(fs))
        //            {
        //                using (FileStream fs2 = new FileStream(FileName[index], FileMode.Open, FileAccess.Read))
        //                {
        //                    using (BinaryReader br2 = new BinaryReader(fs2))
        //                    {
        //                        for (int i = 0; i < fs2.Length;)
        //                        {
        //                            string text = br2.ReadString();
        //                            i += text.Length + 1;
        //                            if (text == Email)
        //                            {
        //                                br.Write(Email);
        //                                br.Write(Password);
        //                                i += br2.ReadString().Length + 1;
        //                            }
        //                            else
        //                            {
        //                                string pass = br2.ReadString();
        //                                i += pass.Length + 1;
        //                                br.Write(text); br.Write(pass);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        using (FileStream fs2 = new FileStream(FileName[index], FileMode.Create, FileAccess.Write)) { }
        //        File_Save("CopyBin.bin");
        //    }

        //    private static void File_Save(string filename,int n =1,int forsize=2)
        //    {

        //        using (FileStream fs = new FileStream(FileName[n], FileMode.Append, FileAccess.Write))
        //        {
        //            using (BinaryWriter br = new BinaryWriter(fs))
        //            {
        //                using (FileStream fs2 = new FileStream(filename, FileMode.Open, FileAccess.Read))
        //                {
        //                    using (BinaryReader br2 = new BinaryReader(fs2))
        //                    {
        //                        for (int i = 0; i < fs2.Length;)
        //                        {
        //                            for (int j = 0; j < forsize; j++)
        //                            {
        //                                string text = br2.ReadString();
        //                                i += text.Length + 1;
        //                                br.Write(text);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    public static bool BinEmailRead(string Email, int n)
        //    {

        //        string Filename = FileName[n];
        //        using (FileStream fs = new FileStream(Filename, FileMode.OpenOrCreate, FileAccess.Read))
        //        {
        //            using (BinaryReader br = new BinaryReader(fs))
        //            {
        //                for (int i = 0; i < fs.Length;)
        //                {
        //                    string text = br.ReadString();
        //                    i += text.Length + 1;
        //                    if (text == Email) return true;
        //                }
        //            }
        //        }
        //        return false;
        //    }


        //    public static bool BinRead(string Name, string Password, int n)
        //    {

        //        string Filename = FileName[n];
        //        using (FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Read))
        //        {
        //            using (BinaryReader br = new BinaryReader(fs))
        //            {
        //                for (int i = 0; i < fs.Length;)
        //                {
        //                    string text = br.ReadString();
        //                    i += text.Length + 1;

        //                    if (text == Name)
        //                    {
        //                        string text2 = br.ReadString();

        //                        if (text2 == Password)
        //                        {
        //                            return true;
        //                        }
        //                        return false;
        //                    }
        //                }
        //            }
        //        }

        //        return false;
        //    }
    }
}
