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
    /// Логика взаимодействия для OrderDetailsPage.xaml
    /// </summary>
    public partial class OrderDetailsPage : Page
    {
        KassirWindow kassirWindow;
        public OrderDetailsPage(KassirWindow kassirWindow)
        {
            InitializeComponent();
            this.kassirWindow = kassirWindow;
        }

        private void OrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderDetails != null && OrderDetails.SelectedItem != null)
            {
                KassirWindow Kassa = new KassirWindow();
                DataRowView row = (DataRowView)OrderDetails.SelectedItem;

                int i = (int)row["ID_OrderDetails"];
                MessageBox.Show(i.ToString());
            }

        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDetails != null && OrderDetails.ItemsSource != null)
            {
                DataRowView row = (DataRowView)OrderDetails.SelectedItem;
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine("компьютерный магазин");
                //sb.AppendLine("кассовый чек № " + Convert.ToInt32(row["id_order"]));
                //sb.AppendLine(row["computername"].ToString() + "-" + row["priceorderrub"].ToString());
                //sb.AppendLine("итого к оплате: " + row["priceorderrub"].ToString());
                //sb.AppendLine("внесено: " + row["entermoneyrub"].ToString());
                //sb.AppendLine("сдача: " + row["changemoneyrub"].ToString());
                //file.writealltext("чек.txt", sb.ToString());
                //int id = Convert.ToInt32(com.selectcompfororders(row["computername"].ToString()));
                //computerstableadapter.updateamount(id);
                //messagebox.show("информация успешно записана в файл");
            }
        }
    }
}
