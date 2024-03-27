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
    /// Логика взаимодействия для RolePage.xaml
    /// </summary>
    public partial class RolePage : Page
    {
        private AdminWindow admin;
        public RolePage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void RoleUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleUser != null && RoleUser.SelectedItem != null)
            {
                DataRowView row = (DataRowView)RoleUser.SelectedItem;

                admin.ID = (int)row["ID_RoleUser"];
                admin.Table = "Роли";
                admin.Text1.Text = "";
                admin.Text1.Text = row["NameRole"].ToString();
            }
        }
    }
}
