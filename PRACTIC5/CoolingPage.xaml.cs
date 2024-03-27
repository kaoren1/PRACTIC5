using System;
using System.Collections.Generic;
using System.Data;
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
