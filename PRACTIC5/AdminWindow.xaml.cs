using PRACTIC5.PCShopDataSet1TableAdapters;
using System;
using System.Collections.Generic;
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
using TextBox = System.Windows.Controls.TextBox;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public class NumericOnlyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is TextCompositionEventArgs e)
            {
                e.Handled = !int.TryParse(e.Text, out _);
            }
        }
    }
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
                    gPUTableAdapter.InsertGPU(Text1.Text, GPUpower);
                }
            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {

            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {
                if (Int32.TryParse(Text1.Text, out int AmountRam) && Int32.TryParse(Text2.Text, out int AmountSSD) && Int32.TryParse(Text2.Text, out int Power))
                {
                    memorySetTableAdapter.InsertMemorySet(AmountRam, AmountSSD, Power);
                }
            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                coloursTableAdapter.InsertColour(Text1.Text);
            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {
                paymentsTableAdapter.InsertPayments(Text1.Text);
            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {
                if (Int32.TryParse(Text2.Text, out int Power) && Int32.TryParse(Text3.Text, out int TDP))
                {
                    coolingTableAdapter.InsertCooling(Text1.Text, Power, TDP);
                }
            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {
                if (Int32.TryParse(Text2.Text, out int Power))
                {
                    bPTableAdapter.InsertBP(Text1.Text, Power);
                }
            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {

            }
            else if ((string)Switch.SelectedItem == "Роли")
            {
                roleUserTableAdapter.InsertRoles(Text1.Text);
            }
        }

        private void UPDATE_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Видеокарта")
            {

            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {

            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {

            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {

            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {

            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {

            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {

            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {

            }
            else if ((string)Switch.SelectedItem == "Роли")
            {

            }
        }

        private void DELETE_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Switch.SelectedItem == "Видеокарта")
            {

            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {

            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {

            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {

            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {

            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {

            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {

            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {

            }
            else if ((string)Switch.SelectedItem == "Роли")
            {

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
                Clear3();

                Text1.Text = "Название видеокарты";
                Text2.Text = "Мощность в ваттах";


                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Название видеокарты";
                };

                Text2.GotFocus += (_, __) =>
                {
                    Text2.Text = "";
                };
                Text2.LostFocus += (_, __) =>
                {
                    Text2.Text = "Мощность в ваттах";
                };
            }
            else if ((string)Switch.SelectedItem == "Комплект процессора")
            {
                CPUSetPage page = new CPUSetPage();
                page.CPUSet.ItemsSource = cPUSetTableAdapter.SelectCooling();
                Frame.Content = page;

                BackUp(GridRight);
                ClearAll();
            }
            else if ((string)Switch.SelectedItem == "Комплект памяти")
            {
                RAMPage page = new RAMPage(this);
                page.RAM.ItemsSource = memorySetTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear2();

                Text1.Text = "Кол-во ОЗУ";
                Text2.Text = "Кол-во SSD";
                Text3.Text = "Потребление";


                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Кол-во ОЗУ";
                };

                Text2.GotFocus += (_, __) =>
                {
                    Text2.Text = "";
                };
                Text2.LostFocus += (_, __) =>
                {
                    Text2.Text = "Кол-во SSD";
                };
                Text3.GotFocus += (_, __) =>
                {
                    Text3.Text = "";
                };
                Text3.LostFocus += (_, __) =>
                {
                    Text3.Text = "Потребление";
                };
            }
            else if ((string)Switch.SelectedItem == "Цвет")
            {
                ColoursPage page = new ColoursPage(this);
                page.Colours.ItemsSource = coloursTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear4();

                Text1.Text = "Название цвета";

                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Название цвета";
                };
            }
            else if ((string)Switch.SelectedItem == "Платежные средства")
            {
                PaymentsPage page = new PaymentsPage(this);
                page.Payments.ItemsSource = paymentsTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear4();

                Text1.Text = "Платежное средство";

                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Платежное средство";
                };
            }
            else if ((string)Switch.SelectedItem == "Охлаждение")
            {
                CoolingPage page = new CoolingPage(this);
                page.Cooling.ItemsSource = coolingTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear2();

                Text1.Text = "Тип охлаждения";
                Text2.Text = "Потребление";
                Text3.Text = "Рассеиваемая мощность";


                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Тип охлаждения";
                };

                Text2.GotFocus += (_, __) =>
                {
                    Text2.Text = "";
                };
                Text2.LostFocus += (_, __) =>
                {
                    Text2.Text = "Потребление";
                };
                Text3.GotFocus += (_, __) =>
                {
                    Text3.Text = "";
                };
                Text3.LostFocus += (_, __) =>
                {
                    Text3.Text = "Рассеиваемая мощность";
                };
            }
            else if ((string)Switch.SelectedItem == "Блок питания")
            {
                BPPage page = new BPPage(this);
                page.BP.ItemsSource = bPTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear3();

                Text1.Text = "Название БП";
                Text2.Text = "Мощность";


                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Название БП";
                };

                Text2.GotFocus += (_, __) =>
                {
                    Text2.Text = "";
                };
                Text2.LostFocus += (_, __) =>
                {
                    Text2.Text = "Мощность";
                };
            }
            else if ((string)Switch.SelectedItem == "Сотрудники")
            {
                WorkersPage page = new WorkersPage();
                page.Workers.ItemsSource = workersTableAdapter.SelectRoles();
                Frame.Content = page;


            }
            else if ((string)Switch.SelectedItem == "Роли")
            {
                RolePage page = new RolePage(this);
                page.RoleUser.ItemsSource = roleUserTableAdapter.GetData();
                Frame.Content = page;

                BackUp(GridRight);
                Clear4();

                Text1.Text = "Название роли";

                Text1.GotFocus += (_, __) =>
                {
                    Text1.Text = "";
                };
                Text1.LostFocus += (_, __) =>
                {
                    Text1.Text = "Название роли";
                };
            }
        }
        private void ClearAndRemoveControls(Panel panel, int textBoxToClearCount, int textBoxToRemoveCount, bool removeComboBox)
        {
            // Находим все TextBox и ComboBox в Panel
            List<TextBox> textBoxes = panel.Children.OfType<TextBox>().ToList();
            List<ComboBox> comboBoxes = panel.Children.OfType<ComboBox>().ToList();

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
                // Удалить ComboBox
                if (comboBoxes.Any())
                {
                    panel.Children.Remove(comboBoxes.First());
                }
            }
            else
            {
                // Переместить ComboBox на строку выше и очистить его
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
        private void ClearAll()
        {
            var textBoxes = GridRight.Children.OfType<TextBox>().ToList();
            foreach (var textBox in textBoxes)
            {
                textBox.Text = "";
            }
        }
        private void Clear1()
        {
            var textBoxes = GridRight.Children.OfType<TextBox>().ToList();
            for (int i = 0; i < 5 && i < textBoxes.Count; i++)
            {
                if (i < 4)
                {
                    textBoxes[i].Text = "";
                }
                else
                {
                    GridRight.Children.Remove(textBoxes[i]);
                }
            }
        }
        private void Clear2()
        {
            var textBoxes = GridRight.Children.OfType<TextBox>().ToList();
            for (int i = 0; i < 5 && i < textBoxes.Count; i++)
            {
                if (i < 3)
                {
                    textBoxes[i].Text = "";
                }
                else
                {
                    var textBox = textBoxes[textBoxes.Count - 1 - i + 3];
                    GridRight.Children.Remove(textBox);
                }
            }
        }
        private void Clear3()
        {
            var textBoxes = GridRight.Children.OfType<TextBox>().ToList();
            for (int i = 0; i < 5 && i < textBoxes.Count; i++)
            {
                if (i < 2)
                {
                    textBoxes[i].Text = "";
                }
                else
                {
                    var textBox = textBoxes[textBoxes.Count - 1 - i + 2];
                    GridRight.Children.Remove(textBox);
                }
            }
        }
        private void Clear4()
        {
            var textBoxes = GridRight.Children.OfType<TextBox>().ToList();
            for (int i = 0; i < 5 && i < textBoxes.Count; i++)
            {
                if (i == 0)
                {
                    textBoxes[i].Text = "";
                }
                else
                {
                    var textBox = textBoxes[textBoxes.Count - 1 - i + 1];
                    GridRight.Children.Remove(textBox);
                }
            }
        }
    }
}
