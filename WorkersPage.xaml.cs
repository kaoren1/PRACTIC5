using PRACTIC5.PCShopDataSetTableAdapters;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        private AdminWindow admin;
        public WorkersPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void Workers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Workers != null && Workers.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Workers.SelectedItem;

                admin.ID = (int)row["ID_Worker"];
                admin.Table = "Работники";
                admin.Text1.Text = "";
                admin.Text1.Text = row["LoginUser"].ToString();

                admin.Text2.Text = "";
                admin.Text2.Text = row["PasswordUser"].ToString();

                admin.Text3.Text = "";
                admin.Text3.Text = row["EmployeeName"].ToString();

                admin.Text4.Text = "";
                admin.Text4.Text = row["EmployeeSurname"].ToString();

                admin.Text5.Text = "";
                admin.Text5.Text = row["EmployeeMiddleName"].ToString();

            }
        }
    }
}
