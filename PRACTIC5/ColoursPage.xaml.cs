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
    /// Логика взаимодействия для ColoursPage.xaml
    /// </summary>
    public partial class ColoursPage : Page
    {
        private AdminWindow admin;
        public ColoursPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void Colours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Colours != null && Colours.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Colours.SelectedItem;

                admin.Text1.Text = "";
                admin.Text1.Text = row["ColourName"].ToString();

            }
        }
    }
}
