using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using PRACTIC5.PCShopDataSetTableAdapters;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WorkersTableAdapter workersTable = new WorkersTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LIGHT_Click(object sender, RoutedEventArgs e)
        {

            var login = workersTable.GetData().Rows;

            for (int i = 0; i < login.Count; i++)
            {
                string Login1 = login[i][1].ToString();
                string Password1 = login[i][2].ToString();

                if (Login1 == Login.Text || (Password1 == HashPassword(Password.Password)))
                {
                    int Role = Convert.ToInt32(login[i][6]);

                    switch (Role)
                    {
                        case 1:
                            KassirWindow K = new KassirWindow();
                            K.Show();
                            break;
                        case 2:
                            AdminWindow A = new AdminWindow();
                            A.Show();
                            break ;
                        case 3:
                            SkladWindow S = new SkladWindow();
                            S.Show();
                            break ;
                    }
                }
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
