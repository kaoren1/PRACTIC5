using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для CoolingPage.xaml
    /// </summary>

    internal class CoolingModel
    {
        public string TypeCooling { get; set; }
        public string CoolingPower { get; set; }
        public string CoolingTDP { get; set; }
    }
    internal class ConverterCool
    {
        public static T Deser<T>()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string json = File.ReadAllText(ofd.FileName);
                T odj = JsonConvert.DeserializeObject<T>(json);
                return odj;
            }
            else
            {
                return default(T);
            }
        }
    }
    public partial class CoolingPage : Page
    {
        private AdminWindow admin;
        public CoolingPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void Cooling_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cooling != null && Cooling.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Cooling.SelectedItem;

                admin.ID = (int)row["ID_Cooling"];
                admin.Table = "Охлаждение";
                admin.Text1.Text = "";
                admin.Text1.Text = row["TypeCooling"].ToString();
                admin.Text2.Text = "";
                admin.Text2.Text = row["CoolingPower"].ToString();
                admin.Text3.Text = "";
                admin.Text3.Text = row["CoolingTDP"].ToString();
            }
        }
    }
}
