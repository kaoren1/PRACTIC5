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
using System.Windows.Shapes;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для SkladWindow.xaml
    /// </summary>
    public partial class SkladWindow : Window
    {
        ComputersTableAdapter computersTableAdapter = new ComputersTableAdapter();

        OrderDetailsTableAdapter orderDetailsTableAdapter = new OrderDetailsTableAdapter();

        GPUTableAdapter gPUTableAdapter = new GPUTableAdapter();

        CPUSetTableAdapter cPUSetTableAdapter = new CPUSetTableAdapter();

        MemorySetTableAdapter memorySetTableAdapter = new MemorySetTableAdapter();

        ColoursTableAdapter coloursTableAdapter = new ColoursTableAdapter();

        PaymentsTableAdapter paymentsTableAdapter = new PaymentsTableAdapter();

        BPTableAdapter bPTableAdapter = new BPTableAdapter();

        public SkladWindow()
        {
            InitializeComponent();
            this.Title = "Окно склада";
            Computers.ItemsSource = computersTableAdapter.SelectComputers();

            Text4.ItemsSource = gPUTableAdapter.GetData();
            Text4.DisplayMemberPath = "GPUname";

            Text5.ItemsSource = cPUSetTableAdapter.GetData();
            Text5.DisplayMemberPath = "CPUname";

            Text6.ItemsSource = coloursTableAdapter.GetData();
            Text6.DisplayMemberPath = "ColourName";

            Text7.ItemsSource = memorySetTableAdapter.GetData();
            Text7.DisplayMemberPath = "AmountRAM";

            Text8.ItemsSource = bPTableAdapter.GetData();
            Text8.DisplayMemberPath = "BPName";
        }

        private void Computers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Computers != null && Computers.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Computers.SelectedItem;

                Text1.Text = row["ComputerName"].ToString();

                Text2.Text = row["AmountWareHouse"].ToString();

                Text3.Text = row["PriceRUB"].ToString();
            }
        }

        private void CREATE_Click(object sender, RoutedEventArgs e)
        {
            if(Int32.TryParse(Text2.Text, out int AmountW) && Int32.TryParse(Text3.Text, out int PRICE))
            {
                DataRowView selectedGPU = (DataRowView)Text4.SelectedItem;
                int initG = Convert.ToInt32(selectedGPU["ID_GPU"]);

                DataRowView selectedCPU = (DataRowView)Text5.SelectedItem;
                int initCPU = Convert.ToInt32(selectedCPU["ID_CPUSet"]);

                DataRowView selectedColour = (DataRowView)Text6.SelectedItem;
                int initColour = Convert.ToInt32(selectedColour["ID_Colour"]);

                DataRowView selectedMemory = (DataRowView)Text7.SelectedItem;
                int initMemory = Convert.ToInt32(selectedMemory["ID_MemorySet"]);

                DataRowView selectedBP = (DataRowView)Text8.SelectedItem;
                int initBP = Convert.ToInt32(selectedBP["ID_BP"]);

                if (CheckPower(initG, initCPU, initMemory, initBP))
                {
                    computersTableAdapter.InsertComputers(Text1.Text, AmountW, PRICE, initG, initCPU, initMemory, initColour, initBP);
                    Computers.ItemsSource = computersTableAdapter.SelectComputers();
                }
            }
        }

        private void UPDATE_Click(object sender, RoutedEventArgs e)
        {
            if (Computers != null && Computers.SelectedItem != null && Int32.TryParse(Text2.Text, out int AmountW) && Int32.TryParse(Text3.Text, out int PRICE))
            {
                DataRowView row = (DataRowView)Computers.SelectedItem;

                int ID = Convert.ToInt32(row["ID_Computer"]);

                Text1.Text = row["ComputerName"].ToString();

                Text2.Text = row["AmountWareHouse"].ToString();

                Text3.Text = row["PriceRUB"].ToString();

                DataRowView selectedGPU = (DataRowView)Text4.SelectedItem;
                int initG = Convert.ToInt32(selectedGPU["ID_GPU"]);

                DataRowView selectedCPU = (DataRowView)Text5.SelectedItem;
                int initCPU = Convert.ToInt32(selectedCPU["ID_CPUSet"]);

                DataRowView selectedColour = (DataRowView)Text6.SelectedItem;
                int initColour = Convert.ToInt32(selectedColour["ID_Colour"]);

                DataRowView selectedMemory = (DataRowView)Text7.SelectedItem;
                int initMemory = Convert.ToInt32(selectedMemory["ID_MemorySet"]);

                DataRowView selectedBP = (DataRowView)Text8.SelectedItem;
                int initBP = Convert.ToInt32(selectedBP["ID_BP"]);

                if(CheckPower(initG, initCPU, initMemory, initBP) && CheckComputer(ID))
                {
                    computersTableAdapter.UpdateComputers(Text1.Text, AmountW, PRICE, initG, initCPU, initColour, initMemory, initBP, ID);
                    Computers.ItemsSource = computersTableAdapter.SelectComputers();
                }
            }
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRowOrder = (DataRowView)Computers.SelectedItem;
            int ID = Convert.ToInt32(selectedRowOrder["ID_Computer"]);
            if (CheckComputer(ID))
            {
                computersTableAdapter.DeleteComputers(ID);
                Computers.ItemsSource = computersTableAdapter.SelectComputers();
            }
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

        private bool CheckPower(int GPU_ID, int CPU_ID, int MemorySet_ID, int BP_ID)
        {
            int GPUPower = Convert.ToInt32(gPUTableAdapter.SelectGPUPower(GPU_ID));
            int CPUPower = Convert.ToInt32(cPUSetTableAdapter.SelectCPUPower(CPU_ID));
            int MemoryPower = Convert.ToInt32(memorySetTableAdapter.SelectMemorySetPower(MemorySet_ID));

            int Power = GPUPower + CPUPower + MemoryPower + 100;
            int BPPower = Convert.ToInt32(bPTableAdapter.SelectBPPower(BP_ID));
            
            if(BPPower < Power)
            {
                MessageBox.Show("Мощность блока питания недостаточна для этой сборки");
                return false;
            }
            return true;
        }
        private bool CheckComputer(int ID)
        {
            bool result = (Convert.ToInt32(orderDetailsTableAdapter.SelectComputers(ID)) != 0) ? false : true;
            if (result)
            {
                return result;
            }
            else
            {
                MessageBox.Show("Ошибка: данное поле используется в заказе и будет доступно для удаления/изменения только после того, как данный заказ будет удален");
                return result;
            }
        }
    }
}
