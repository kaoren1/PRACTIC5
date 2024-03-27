using PRACTIC5.PCShopDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для KassirWindow.xaml
    /// </summary>
    public partial class KassirWindow : Window
    {
        OrdersTableAdapter ordersTableAdapter = new OrdersTableAdapter();

        OrderDetailsTableAdapter orderDetailsTableAdapter = new OrderDetailsTableAdapter();

        PaymentsTableAdapter paymentsTableAdapter = new PaymentsTableAdapter();

        ComputersTableAdapter computersTableAdapter = new ComputersTableAdapter();

        WorkersTableAdapter workersTableAdapter = new WorkersTableAdapter();
        public KassirWindow()
        {
            InitializeComponent();
            OrdersALL.ItemsSource = ordersTableAdapter.SelectAllView();
            Text1.ItemsSource = paymentsTableAdapter.GetData();
            Text1.DisplayMemberPath = "PaymentName";

            Text6.ItemsSource = computersTableAdapter.GetForBoxOrders();
            Text6.DisplayMemberPath = "ComputerName";

            Text7.ItemsSource = workersTableAdapter.GetData();
            Text7.DisplayMemberPath = "EmployeeSurname";

            this.Title = "Окно кассира";
        }

        private void CREATE_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)Text1.SelectedItem;
            int init = Convert.ToInt32(selectedRow["ID_Payment"]);

            DataRowView selectedRowDetails = (DataRowView)Text6.SelectedItem;
            int initC = Convert.ToInt32(selectedRowDetails["ID_Computer"]);

            DataRowView selectedRowWorker = (DataRowView)Text7.SelectedItem;
            int initW = Convert.ToInt32(selectedRowWorker["ID_Worker"]);

            if (Int32.TryParse(Text4.Text, out int EnterMoney) && Int32.TryParse(Text3.Text, out int PriceOrder) && Int32.TryParse(Text5.Text, out int ChangeMoney))
            {
                ordersTableAdapter.InsertOrders(init, initW, Text2.Text, PriceOrder, EnterMoney, ChangeMoney);
                int ID = (int)ordersTableAdapter.ScalarID();
                orderDetailsTableAdapter.InsertOrderDetails(initC, ID);
            }
            OrdersALL.ItemsSource = ordersTableAdapter.SelectAllView();
        }

        private void UPDATE_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)Text1.SelectedItem;
            int init = Convert.ToInt32(selectedRow["ID_Payment"]);

            DataRowView selectedRowDetails = (DataRowView)Text6.SelectedItem;
            int initC = Convert.ToInt32(selectedRowDetails["ID_Computer"]);

            DataRowView selectedRowWorker = (DataRowView)Text7.SelectedItem;
            int initW = Convert.ToInt32(selectedRowWorker["ID_Worker"]);

            DataRowView selectedRowOrder = (DataRowView)OrdersALL.SelectedItem;
            int initO = Convert.ToInt32(selectedRowOrder["ID_Order"]);

            DataRowView selectedRowOrderDetails = (DataRowView)OrdersALL.SelectedItem;
            int initD = Convert.ToInt32(selectedRowOrderDetails["ID_OrderDetails"]);

            if (Int32.TryParse(Text4.Text, out int EnterMoney) && Int32.TryParse(Text3.Text, out int PriceOrder) && Int32.TryParse(Text5.Text, out int ChangeMoney))
            {
                ordersTableAdapter.UpdateOrders(init, initW, Text2.Text, PriceOrder, EnterMoney, ChangeMoney, initO);
                orderDetailsTableAdapter.UpdateOrderDetails(initC, initO, initD);
            }
            OrdersALL.ItemsSource = ordersTableAdapter.SelectAllView();
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRowOrder = (DataRowView)OrdersALL.SelectedItem;
            int initO = Convert.ToInt32(selectedRowOrder["ID_Order"]);

            DataRowView selectedRowOrderDetails = (DataRowView)OrdersALL.SelectedItem;
            int initD = Convert.ToInt32(selectedRowOrderDetails["ID_OrderDetails"]);

            orderDetailsTableAdapter.DeleteOrderDetails(initD);
            ordersTableAdapter.DeleteOrders(initO);
            OrdersALL.ItemsSource = ordersTableAdapter.SelectAllView();
        }
        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string inputText = e.Text;

            if (!string.IsNullOrEmpty(inputText) && !char.IsDigit(inputText[0]))
            {
                e.Handled = true;
            }
        }
        private void Text_PreviewTextInput1(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string inputText = e.Text;

            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && c != ':' && c != '.')
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void END_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersALL != null && OrdersALL.SelectedItem != null)
            {
                DataRowView row = (DataRowView)OrdersALL.SelectedItem;
                string COMP = computersTableAdapter.ScalarComp(Convert.ToInt32(row["ID_Computer"])).ToString();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Компьютерный магазин");
                sb.AppendLine("Кассовый чек № " + Convert.ToInt32(row["ID_Order"]));
                sb.AppendLine(COMP + " - " + row["PriceOrderRUB"].ToString());
                sb.AppendLine("Итого к оплате: " + row["PriceOrderRUB"].ToString());
                sb.AppendLine("Внесено: " + row["EnterMoneyRUB"].ToString());
                sb.AppendLine("Сдача: " + row["ChangeMoneyRUB"].ToString());
                File.WriteAllText("Чек.txt", sb.ToString());
                int ID = Convert.ToInt32(computersTableAdapter.SelectCompForOrders(row["ComputerName"].ToString()));
                computersTableAdapter.UpdateAmount(ID);
                MessageBox.Show("Информация успешно записана в файл");
            }
        }

        private void OrdersALL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersALL != null && OrdersALL.SelectedItem != null)
            {
                DataRowView row = (DataRowView)OrdersALL.SelectedItem;

                Text2.Text = row["OrderDate"].ToString();

                Text4.Text = row["EnterMoneyRUB"].ToString();
            }
        }

        private void Text6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataRowView selectedRow = (DataRowView)Text6.SelectedItem;
            if (selectedRow != null)
            {
                int init = Convert.ToInt32(selectedRow["PriceRUB"]);
                int i = Convert.ToInt32(Text4.Text);

                Text3.Text = init.ToString();
                Text5.Text = (i - init).ToString();
            }
        }
    }
}
