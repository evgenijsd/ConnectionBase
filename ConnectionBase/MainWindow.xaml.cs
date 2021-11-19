using ConnectionBase.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnectionBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            ResulTables resulTables = new ResulTables();
            resulTables.Owner = this;
            this.Hide();
            resulTables.ShowDialog();
            this.Show();
        }

        private void ButtonConnection_Click(object sender, RoutedEventArgs e)
        {
            EditConnections editConnections = new EditConnections();
            editConnections.Owner = this;
            this.Hide();
            editConnections.ShowDialog();
            this.Show();
        }
    }
}
