using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace PRACTIC5
{
    /// <summary>
    /// Логика взаимодействия для GPUPage.xaml
    /// </summary>
    public partial class GPUPage : Page
    {
        private AdminWindow admin;
        public GPUPage(AdminWindow admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void GPU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GPU != null && GPU.SelectedItem != null)
            {
                DataRowView row = (DataRowView)GPU.SelectedItem;

                admin.Text1.Text = "";
                admin.Text1.Text = row["GPUname"].ToString();
                admin.Text2.Text = "";
                admin.Text2.Text = row["GPUpower"].ToString();
            }
        }
    }
}
