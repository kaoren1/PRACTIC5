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
    /// Логика взаимодействия для BPPage.xaml
    /// </summary>
    public partial class BPPage : Page
    {
        private AdminWindow admin;

        public BPPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void BP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BP != null && BP.SelectedItem != null)
            {
                DataRowView row = (DataRowView)BP.SelectedItem;

                admin.Text1.Text = "";
                admin.Text1.Text = row["BPName"].ToString();
                admin.Text2.Text = "";
                admin.Text2.Text = row["BPPower"].ToString();
            }
        }
    }
}
