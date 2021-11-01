using ConnectionBase.ViewModels;
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

namespace ConnectionBase.View
{
    /// <summary>
    /// Логика взаимодействия для EditConnections.xaml
    /// </summary>
    public partial class EditConnections : Window
    {
        public EditConnections()
        {
            InitializeComponent();

            DataContext = new EditConnectiosViewModel();
        }
    }
}
