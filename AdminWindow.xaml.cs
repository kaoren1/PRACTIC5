using PRACTIC5.PCShopDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;
using static PRACTIC5.PCShopDataSet;
using TextBox = System.Windows.Controls.TextBox;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    

    /*Список проблем: некорретные проверки ВСЕ  с изменением вывода строки*/
    public partial class AdminWindow : Window
    {
        GPUTableAdapter gPUTableAdapter = new GPUTableAdapter();

        CPUSetTableAdapter cPUSetTableAdapter = new CPUSetTableAdapter();

        MemorySetTableAdapter memorySetTableAdapter = new MemorySetTableAdapter();

        ColoursTableAdapter coloursTableAdapter = new ColoursTableAdapter();

        PaymentsTableAdapter paymentsTableAdapter = new PaymentsTableAdapter();

        CoolingTableAdapter coolingTableAdapter = new CoolingTableAdapter();

        BPTableAdapter bPTableAdapter = new BPTableAdapter();

        WorkersTableAdapter workersTableAdapter = new WorkersTableAdapter();

        RoleUserTableAdapter roleUserTableAdapter = new RoleUserTableAdapter();

        ComputersTableAdapter computersTableAdapter = new ComputersTableAdapter();

        OrdersTableAdapter ordersTableAdapter = new OrdersTableAdapter();

        public int ID;
        public string Table;
        public AdminWindow()
        {
            InitializeComponent();
            this.Title = "Окно администратора";
            Switch.Items.Add("Видеокарта");
            Switch.Items.Add("Комплект процессора");
            Switch.Items.Add("Комплект памяти");
            Switch.Items.Add("Цвет");
            Switch.Items.Add("Платежные средства");
            Switch.Items.Add("Охлаждение");
            Switch.Items.Add("Блок питания");
            Switch.Items.Add("Сотрудники");
            Switch.Items.Add("Роли");   
        }

        private void CREATE_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Видеокарта")
            {
                if (Int32.TryParse(Text2.Text, out int GPUpower))
                {
                    GPUPage page = new GPUPage(this);
                    gPUTableAdapter.InsertGPU(Text1.Text, GPUpower);

                    page.GPU.ItemsSource = gPUTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {
                if (Int32.TryParse(Text4.Text, out int СPUpower))
                {
                    DataRowView selectedRow = (DataRowView)Text6.SelectedItem;

                    int init = Convert.ToInt32(selectedRow["ID_Cooling"]);
                    int value = Convert.ToInt32(coolingTableAdapter.SelectCooling(init));

                    if (СPUpower < value)
                    {
                        cPUSetTableAdapter.InsertCPUSet(Text1.Text, Text2.Text, Text3.Text, СPUpower, init);
                        CPUSetPage page = new CPUSetPage(this);
                        page.CPUSet.ItemsSource = cPUSetTableAdapter.SelectNORM();
                        Frame.Content = page;
                    }
                    else
                    {
                        MessageBox.Show("Мощности охлаждения для данного процессора будет недостаточно");
                    }
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {
                if (Int32.TryParse(Text1.Text, out int AmountRam) && Int32.TryParse(Text2.Text, out int AmountSSD) && Int32.TryParse(Text2.Text, out int Power))
                {
                    memorySetTableAdapter.InsertMemorySet(AmountRam, AmountSSD, Power);
                    RAMPage page = new RAMPage(this);
                    page.RAM.ItemsSource = memorySetTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                coloursTableAdapter.InsertColour(Text1.Text);
                ColoursPage page = new ColoursPage(this);
                page.Colours.ItemsSource = coloursTableAdapter.GetData();
                Frame.Content = page;
            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {
                paymentsTableAdapter.InsertPayments(Text1.Text);
                PaymentsPage page = new PaymentsPage(this);
                page.Payments.ItemsSource = paymentsTableAdapter.GetData();
                Frame.Content = page;

            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {
                if (Int32.TryParse(Text2.Text, out int Power) && Int32.TryParse(Text3.Text, out int TDP))
                {
                    coolingTableAdapter.InsertCooling(Text1.Text, Power, TDP);
                    CoolingPage page = new CoolingPage(this);
                    page.Cooling.ItemsSource = coolingTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {
                if (Int32.TryParse(Text2.Text, out int Power))
                {
                    bPTableAdapter.InsertBP(Text1.Text, Power);
                    BPPage page = new BPPage(this);
                    page.BP.ItemsSource = bPTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {
                string password = HashPassword(Text2.Text);
                DataRowView selectedRow = (DataRowView)Text6.SelectedItem;

                int init = Convert.ToInt32(selectedRow["ID_RoleUser"]);

                workersTableAdapter.InsertWorkers(Text1.Text, password, Text3.Text, Text4.Text, Text5.Text, init);
                WorkersPage page = new WorkersPage(this);
                page.Workers.ItemsSource = workersTableAdapter.GetData();
                Frame.Content = page;
            }
            else if ((string)Switch.SelectedItem == "Роли")
            {
                roleUserTableAdapter.InsertRoles(Text1.Text);
                RolePage page = new RolePage(this);
                page.RoleUser.ItemsSource = roleUserTableAdapter.GetData();
                Frame.Content = page;
            }
        }



        private void UPDATE_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Видеокарта")
            {
                if (CheckRow("Видеокарта", ID))
                {
                    if (Int32.TryParse(Text2.Text, out int GPUpower))
                    {
                        gPUTableAdapter.UpdateGPU(Text1.Text, GPUpower, ID);
                        GPUPage page = new GPUPage(this);
                        page.GPU.ItemsSource = gPUTableAdapter.GetData();
                        Frame.Content = page;
                    }
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {
                if (CheckRow("Комплект процессора", ID))
                {
                    DataRowView selectedRow = (DataRowView)Text6.SelectedItem;

                    int init = Convert.ToInt32(selectedRow["ID_Cooling"]);
                    int value = Convert.ToInt32(coolingTableAdapter.SelectCooling(init));

                    if (Int32.TryParse(Text4.Text, out int СPUpower))
                    {
                        if (СPUpower < value)
                        {
                            cPUSetTableAdapter.UpdateCPUSet(Text1.Text, Text2.Text, Text3.Text, СPUpower, init, ID);
                            CPUSetPage page = new CPUSetPage(this);
                            page.CPUSet.ItemsSource = cPUSetTableAdapter.SelectNORM();
                            Frame.Content = page;
                        }
                        else
                        {
                            MessageBox.Show("Мощности охлаждения для данного процессора будет недостаточно");
                        }
                    }
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {
                if (CheckRow("Комплект памяти", ID))
                {
                    if (Int32.TryParse(Text1.Text, out int AmountRam) && Int32.TryParse(Text2.Text, out int AmountSSD) && Int32.TryParse(Text3.Text, out int Power))
                    {
                        memorySetTableAdapter.UpdateMemorySet(AmountRam, AmountSSD, Power, ID);
                        RAMPage page = new RAMPage(this);
                        page.RAM.ItemsSource = memorySetTableAdapter.GetData();
                        Frame.Content = page;
                    }
            }   }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                if (CheckRow("Цвет", ID))
                {
                    coloursTableAdapter.UpdateColour(Text1.Text, ID);
                    ColoursPage page = new ColoursPage(this);
                    page.Colours.ItemsSource = coloursTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {
                if (CheckRow("Платежные средства", ID))
                {
                    paymentsTableAdapter.UpdatePayments(Text1.Text, ID);
                    PaymentsPage page = new PaymentsPage(this);
                    page.Payments.ItemsSource = paymentsTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {
                if (Int32.TryParse(Text2.Text, out int Power) && Int32.TryParse(Text3.Text, out int TDP))
                {
                    coolingTableAdapter.UpdateCooling(Text1.Text, Power, TDP, ID);
                    CoolingPage page = new CoolingPage(this);
                    page.Cooling.ItemsSource = coolingTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {
                if (CheckRow("Блок питания", ID))
                {
                    if (Int32.TryParse(Text2.Text, out int Power))
                    {
                        bPTableAdapter.UpdateBP(Text1.Text, Power, ID);
                        BPPage page = new BPPage(this);
                        page.BP.ItemsSource = bPTableAdapter.GetData();
                        Frame.Content = page;
                    }
                }
            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {
                if (CheckRow("Сотрудники", ID))
                {
                    DataRowView selectedRow = (DataRowView)Text6.SelectedItem;

                    int init = Convert.ToInt32(selectedRow["ID_RoleUser"]);
                    string password = HashPassword(Text2.Text);
                    workersTableAdapter.UpdateWorkers(Text1.Text, password, Text3.Text, Text4.Text, Text5.Text, init, ID);
                    WorkersPage page = new WorkersPage(this);
                    page.Workers.ItemsSource = workersTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Роли")
            {
                if (CheckRow("Роли", ID))
                {
                    roleUserTableAdapter.UpdateRoles(Text1.Text, ID);
                    RolePage page = new RolePage(this);
                    page.RoleUser.ItemsSource = roleUserTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Видеокарта")
            {
                if (CheckRow("Видеокарта", ID))
                {
                    gPUTableAdapter.DeleteGPU(ID);
                    GPUPage page = new GPUPage(this);
                    page.GPU.ItemsSource = gPUTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {
                if (CheckRow("Комплект процессора", ID))
                {
                    cPUSetTableAdapter.DeleteCPUSet(ID);
                    CPUSetPage page = new CPUSetPage(this);
                    page.CPUSet.ItemsSource = cPUSetTableAdapter.SelectNORM();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {
                if (CheckRow("Комплект памяти", ID))
                {
                    memorySetTableAdapter.DeleteMemorySet(ID);
                    RAMPage page = new RAMPage(this);
                    page.RAM.ItemsSource = memorySetTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                if (CheckRow("Цвет", ID))
                {
                    coloursTableAdapter.DeleteColour(ID);
                    ColoursPage page = new ColoursPage(this);
                    page.Colours.ItemsSource = coloursTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {
                if (CheckRow("Платежные средства", ID))
                {
                    paymentsTableAdapter.DeletePayments(ID);
                    PaymentsPage page = new PaymentsPage(this);
                    page.Payments.ItemsSource = paymentsTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {
                coolingTableAdapter.DeleteCooling(ID);
                CoolingPage page = new CoolingPage(this);
                page.Cooling.ItemsSource = coolingTableAdapter.GetData();
                Frame.Content = page;
            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {
                if (CheckRow("Блок питания", ID))
                {
                    bPTableAdapter.DeleteBP(ID);
                    BPPage page = new BPPage(this);
                    page.BP.ItemsSource = bPTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {
                if (CheckRow("Сотрудники", ID))
                {
                    workersTableAdapter.DeleteWorkers(ID);
                    WorkersPage page = new WorkersPage(this);
                    page.Workers.ItemsSource = workersTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
            else if ((string)Switch.SelectedItem == "Роли")
            {
                if (CheckRow("Роли", ID))
                {
                    roleUserTableAdapter.DeleteRoles(ID);
                    RolePage page = new RolePage(this);
                    page.RoleUser.ItemsSource = roleUserTableAdapter.GetData();
                    Frame.Content = page;
                }
            }
        }

        private void Switch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Видеокарта")
            {
                GPUPage page = new GPUPage(this);
                page.GPU.ItemsSource = gPUTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(2, 3, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Название видеокарты");

                var hint1 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text2);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text2, "Мощность в ваттах");

            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {
                CPUSetPage page = new CPUSetPage(this);
                page.CPUSet.ItemsSource = cPUSetTableAdapter.SelectNORM();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(4, 1, false);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Название процессора");

                var hint1 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text2);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text2, "Чипсет");

                var hin2 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text3);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text3, "Материнская плата");

                var hint3 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text4);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text4, "Потребление процессора в ваттах");

                Text6.ItemsSource = coolingTableAdapter.GetData();
                Text6.DisplayMemberPath = "TypeCooling";

            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {
                RAMPage page = new RAMPage(this);
                page.RAM.ItemsSource = memorySetTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(3, 2, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Кол-во ОЗУ в гигабайтах");

                var hint1 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text2);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text2, "Кол-во SSD в гигабайтах");

                var hint2 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text3);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text3, "Потребление в ваттах");

                
            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                ColoursPage page = new ColoursPage(this);
                page.Colours.ItemsSource = coloursTableAdapter.GetData();
                Frame.Content = page;
                Import.IsEnabled = true;

                BackUp(GridRight);
                Clear(1, 4, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Название цвета");
            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {
                PaymentsPage page = new PaymentsPage(this);
                page.Payments.ItemsSource = paymentsTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(1, 4, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Платежное средство");
            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {
                CoolingPage page = new CoolingPage(this);
                page.Cooling.ItemsSource = coolingTableAdapter.GetData();
                Frame.Content = page;
                Import.IsEnabled = true;

                BackUp(GridRight);
                Clear(3, 2, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Тип охлаждения");

                var hint1 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text2);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text2, "Потребление в ваттах");

                var hint2 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text3);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text3, "Рассеиваемая мощность");
            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {
                BPPage page = new BPPage(this);
                page.BP.ItemsSource = bPTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(2, 3, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Название БП");

                var hint1 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text2);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text2, "Мощность в ваттах");
            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {
                WorkersPage page = new WorkersPage(this);
                page.Workers.ItemsSource = workersTableAdapter.GetDataBy3();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(5, 0, false);
               

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Логин");

                var hint1 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text2);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text2, "Пароль");

                var hint2 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text3);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text3, "Имя");

                var hint3 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text4);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text4, "Фамилия");

                var hint4 = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text5);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text5, "Отчество");

                Text6.ItemsSource = roleUserTableAdapter.GetData();
                Text6.DisplayMemberPath = "NameRole";
            }
            else if ((string)Switch.SelectedItem == "Роли")
            {
                RolePage page = new RolePage(this);
                page.RoleUser.ItemsSource = roleUserTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear(1, 4, true);

                var hint = MaterialDesignThemes.Wpf.HintAssist.GetHint(Text1);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(Text1, "Название роли");
            }
        }
        private void Clear(int textBoxToClearCount, int textBoxToRemoveCount, bool removeComboBox)
        {
            Panel panel = GridRight;
            // Находим все TextBox в Panel
            List<TextBox> textBoxes = panel.Children.OfType<TextBox>().ToList();

            // Индекс первого TextBox
            int textBoxIndex = panel.Children.IndexOf(textBoxes.First());

            // Очистить указанное количество TextBox
            for (int i = 0; i < textBoxToClearCount && i < textBoxes.Count; i++)
            {
                textBoxes[i].Text = "";
            }

            // Удалить указанное количество TextBox
            for (int i = 0; i < textBoxToRemoveCount && i + textBoxToClearCount < textBoxes.Count; i++)
            {
                panel.Children.Remove(textBoxes[i + textBoxToClearCount]);
            }

            // Если нужно удалить ComboBox
            if (removeComboBox)
            {
                List<System.Windows.Controls.ComboBox> comboBoxes = panel.Children.OfType<System.Windows.Controls.ComboBox>().ToList();
                if (comboBoxes.Any())
                {
                    panel.Children.Remove(comboBoxes.First());
                }
            }
            else
            {
                List<System.Windows.Controls.ComboBox> comboBoxes = panel.Children.OfType<System.Windows.Controls.ComboBox>().ToList();
                if (comboBoxes.Any())
                {
                    int comboBoxIndex = panel.Children.IndexOf(comboBoxes.First());
                    panel.Children.RemoveAt(comboBoxIndex);
                    panel.Children.Insert(comboBoxIndex - 1, comboBoxes.First());
                    comboBoxes.First().SelectedIndex = -1;
                }
            }
        }
        private void BackUp(Panel panel)
        {
            // Проверяем, если у нас есть удаленные TextBox
            if (GridRight.Children.Contains(Text1) == false)
            {
                GridRight.Children.Insert(1, Text1);  // восстановляем Text1 на место
            }

            if (GridRight.Children.Contains(Text2) == false)
            {
                GridRight.Children.Insert(2, Text2);  // восстановляем Text2 на место
            }

            if (GridRight.Children.Contains(Text3) == false)
            {
                GridRight.Children.Insert(3, Text3);  // восстановляем Text3 на место
            }

            if (GridRight.Children.Contains(Text4) == false)
            {
                GridRight.Children.Insert(4, Text4);  // восстановляем Text4 на место
            }

            if (GridRight.Children.Contains(Text5) == false)
            {
                GridRight.Children.Insert(5, Text5);  // восстановляем Text5 на место
            }

            // Проверяем, если у нас был удален ComboBox
            if (GridRight.Children.Contains(Text6) == false)
            {
                GridRight.Children.Insert(6, Text6);  // восстанавливаем Text6 на место
            }
        }
        private bool CheckRow(string Table, int ID)
        {
            if(Table == "Видеокарта")
            {
                bool result = (Convert.ToInt32(computersTableAdapter.GetAmountGPU(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данное поле используется в сборке компьютера и будет доступно для удаления/изменения только после того, как данный компьютер закончится на складе");
                    return result;
                }
            }
            else if (Table == "Комплект процессора")
            {
                bool result = (Convert.ToInt32(computersTableAdapter.GetAmountCPU(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данное поле используется в сборке компьютера и будет доступно для удаления/изменения только после того, как данный компьютер закончится на складе");
                    return result;
                }
            }
            else if (Table == "Цвет")
            {
                bool result = (Convert.ToInt32(computersTableAdapter.GetAmountColour(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данное поле используется в сборке компьютера и будет доступно для удаления/изменения только после того, как данный компьютер закончится на складе");
                    return result;
                }
            }
            else if (Table == "Блок питания")
            {
                bool result = (Convert.ToInt32(computersTableAdapter.GetAmountBP(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данное поле используется в сборке компьютера и будет доступно для удаления/изменения только после того, как данный компьютер закончится на складе");
                    return result;
                }
            }
            else if (Table == "Комплект памяти")
            {
                bool result = (Convert.ToInt32(computersTableAdapter.GetMemorySet(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данное поле используется в сборке компьютера и будет доступно для удаления/изменения только после того, как данный компьютер закончится на складе");
                    return result;
                }
            }
            else if (Table == "Платежные средства")
            {
                bool result = (Convert.ToInt32(ordersTableAdapter.GetPayment(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данное платежное средство используется в хранимом чеке и оно недостуно для удаления/изменения пока не будет удален данный чек");
                    return result;
                }
            }
            else if (Table == "Сотрудники")
            {
                bool result = (Convert.ToInt32(ordersTableAdapter.GetWorker(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: данный работник используется в хранимом чеке и недоступен для удаления/изменения пока не будет удален данный чек");
                    return result;
                }
            }
            else if (Table == "Роли")
            {
                bool result = (Convert.ToInt32(workersTableAdapter.GetRole(ID)) != 0) ? false : true;
                if (result)
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Ошибка: работник с этой ролью используется в чеке и это поле недостуно для удаления/изменения пока не будет удален данный чек");
                    return result;
                }
            }
            return false;
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

        private void Text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string selectedSwitchItem = (string)Switch.SelectedItem;
            string inputText = e.Text;

            if (selectedSwitchItem == "Видеокарта" && textBox.Name == "Text2")
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsDigit(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Комплект процессора" && textBox.Name == "Text4")
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsDigit(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Комплект памяти" && (textBox.Name == "Text1" || textBox.Name == "Text2" || textBox.Name == "Text3"))
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsDigit(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Цвет" && textBox.Name == "Text1")
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsLetter(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Платежные средства" && textBox.Name == "Text1")
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsLetter(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Охлаждение" && (textBox.Name == "Text2" || textBox.Name == "Text3"))
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsDigit(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Блок питания" && textBox.Name == "Text2")
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsDigit(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Сотрудники" && (textBox.Name == "Text3" || textBox.Name == "Text4" || textBox.Name == "Text5"))
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsLetter(inputText[0]))
                {
                    e.Handled = true;
                }
            }
            else if (selectedSwitchItem == "Роли" && textBox.Name == "Text1")
            {
                if (!string.IsNullOrEmpty(inputText) && !char.IsLetter(inputText[0]))
                {
                    e.Handled = true;
                }
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Охлаждение")
            {
                List<CoolingModel> forImport = Converter.Deser<List<CoolingModel>>();
                foreach (var item in forImport)
                {
                    coolingTableAdapter.InsertCooling(item.TypeCooling, Convert.ToInt32(item.CoolingPower), Convert.ToInt32(item.CoolingTDP));
                }
                CoolingPage page = new CoolingPage(this);
                page.Cooling.ItemsSource = coolingTableAdapter.GetData();
                Frame.Content = page;
            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                List<ColourModel> forImport = Converter.Deser<List<ColourModel>>();
                foreach (var item in forImport)
                {
                    coloursTableAdapter.InsertColour(item.ColourName);
                }
                ColoursPage page = new ColoursPage(this);
                page.Colours.ItemsSource = coloursTableAdapter.GetData();
                Frame.Content = page;
            }
        }

        private void Text2_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Сотрудники")
            {
                string maskedText = "";
                foreach (char c in Text2.Text)
                {
                    maskedText += "•";
                }

                Text2.Text = maskedText;
            }
        }
    }
}
