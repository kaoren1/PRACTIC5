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
    /// Логика взаимодействия для PaymentsPage.xaml
    /// </summary>
    public partial class PaymentsPage : Page
    {
        private AdminWindow admin;
        public PaymentsPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void Payments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Payments != null && Payments.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Payments.SelectedItem;

                admin.Text1.Text = "";
                admin.Text1.Text = row["PaymentName"].ToString();
            }
        }
    }
}
