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
    /// Логика взаимодействия для CPUSetPage.xaml
    /// </summary>
    public partial class CPUSetPage : Page
    {
        private AdminWindow admin;
        public CPUSetPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void CPUSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CPUSet != null && CPUSet.SelectedItem != null)
            {
                DataRowView row = (DataRowView)CPUSet.SelectedItem;

                admin.ID = (int)row["ID_CPUSet"];
                admin.Table = "Комплект процессора";
                admin.Text1.Text = "";
                admin.Text1.Text = row["CPUname"].ToString();

                admin.Text2.Text = "";
                admin.Text2.Text = row["Chipset"].ToString();

                admin.Text3.Text = "";
                admin.Text3.Text = row["MotherBoard"].ToString();

                admin.Text4.Text = "";
                admin.Text4.Text = row["CPUpower"].ToString();

            }
        }
    }
}
