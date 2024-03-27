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
    /// Логика взаимодействия для RAMPage.xaml
    /// </summary>
    public partial class RAMPage : Page
    {
        private AdminWindow admin;
        public RAMPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void RAM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RAM != null && RAM.SelectedItem != null)
            {
                DataRowView row = (DataRowView)RAM.SelectedItem;

                admin.Text1.Text = "";
                admin.Text1.Text = row["AmountRAM"].ToString();
                admin.Text2.Text = "";
                admin.Text2.Text = row["AmountSSD"].ToString();
                admin.Text2.Text = "";
                admin.Text2.Text = row["MemorySetPower"].ToString();
            }
        }
    }
}
