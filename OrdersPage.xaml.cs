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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private KassirWindow kassir;
        public OrdersPage(KassirWindow kassir)
        {
            InitializeComponent();
            this.kassir = kassir;
        }

        private void OrdersALL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersALL != null && OrdersALL.SelectedItem != null)
            {
                DataRowView row = (DataRowView)OrdersALL.SelectedItem;
                KassirWindow K = new KassirWindow();

                K.Text2.Text = row["OrderDate"].ToString();

                K.Text4.Text = row["EnterMoneyRUB"].ToString();

            }
        }
    }
}
